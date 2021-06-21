# TAS-Tool
Tool Assisted Speedrun tool for any game that can receive "[virtual key presses](https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-keybd_event)" from User32.dll

## About the code
In current state, the solution still includes the first version of the tool (Windows forms project named "Botti") which is no longer in use.  

The UI has a lot of unused text boxes and the other tab for showing active key playback. The logic for these will be implemented eventually once all main features work, so don't worry about useless textboxes for now.

Turns out that the virtual key commands have different byte values in [System.Windows.Forms](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.keys?view=net-5.0) and [System.Windows.Input](https://docs.microsoft.com/en-us/dotnet/api/system.windows.input.key?view=net-5.0), and it seems that most games use the Windows Forms inputs. Due to this, at least one Windows.Forms project needs to be referenced in the project.  

After the first winforms POC my intention was to have separate UI and "API" as the projects suggest. However, the keyboard simulation needs to be done in the WPF project which then doesn't allow proper decoupling of the two projects. There's likely a lot of room for refactoring, for example Autofac has not yet been taken in use in the UI project.

All sorts of comments and criticism is welcome, I just reserve the right to be aware of but not arsed to fix certain things.

## Configuration and usage
TAS UI project has an App.config file (TasUi.dll.config after compiling) which is the main (only) configuration file for the program. There are 3 things that the end user can adjust:  
 * Track input JSON files location. Set your own folder path after the parameter value. There are example input files included, they will be in C:\YourSolutionFolder\TAS-Tool\Source\TasUi\bin\Release\netcoreapp3.1\Track runs
     * ``<add key="trackJsonLocation" value="C:\TAS-Tool\Track runs" />``
 * Games that you try to run. You can add new ones as long as the parameters are correct:  
 `name` parameter is optional, this is shown in the dropdown menu when opening the tool,  
 to find `lpClassName` and `windowCaption` for your favourite game, use a tool like WinSpy++ ([standalone download](https://www.catch22.net/software/winspy)) or Spy++ (Visual Studio tool included in some C++ utilities).  
 Examples for getting the values for Assetto Corsa Competizione with WinSpy++:
     * WindowCaption (Notice the name had 2 spaces in the end, these are likely necessary): ![image](https://user-images.githubusercontent.com/15697256/122680031-11756b80-d1f6-11eb-8663-8ccfa1a841f6.png)
     * lpClassName: </br> ![image](https://user-images.githubusercontent.com/15697256/122680127-74ff9900-d1f6-11eb-9148-9ec5cdc94d76.png)  
     Result will be as in the config file:  
     `` <TasGame name="ACC" lpClassName="UnrealWindow" windowCaption="AC2  " /> ``
 * Keyboard handler type can be selected. Only the `enabled` parameter can be changed, use boolean values (Case sensitive) and remember to disable the other value if changing. Changing this will change the used byte values for the virtual key presses.
     * ``<KeyboardHandlerConfigElement name="WinFormsKeyboardHandler" enabled="True"/>``

### Creating inputs
To create your own input sequences, you can use one of the json files (named "Construction site" due to track with that name in Trials 2) in the track runs folder as an example.  

#### Input JSON elements:
* `Timing` : The amount of milliseconds which is waited between reading next key commands. I haven't tested extreme values, but you should be able to use any positive integer value. Smaller number == sequence completed faster, but more accurate inputs. In most games, optimal value might be the rendering speed (e.g. 33 FPS in some old games) or the physics engine running rate (333Hz in Assetto Corsa for example)
* `Length` : The length of the input sequence (`OnOffPattern`), all sequences' lengths need to match this value or you'll likely receive an "index out of bounds" error when the run is getting to end.
* `KeyName` : This value is mapped to an enum which is sent to the game. The values need to match with the hardcoded Microsoft values. Check appropriate key names for the selected keyboard handler. [Winforms](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.keys?view=net-5.0), [WPF](https://docs.microsoft.com/en-us/dotnet/api/system.windows.input.key?view=net-5.0)
* `OnOffPattern` : The sequence of is the key pressed or not. All values from the same index will be read for each cycle, meaning if you have value `1` in 2 different keys in the first index of the array, both keys will be pressed at the same time. Only `1` and `0` are valid values.

Full input JSON file:
```
{
  "Timing": 500, 
  "Length": 20,
  "Inputs": [
    {
      "Key": {
        "KeyName": "W",
        "OnOffPattern": [ 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 0 ]
      }
    },
    {
      "Key": {
        "KeyName": "A",
        "OnOffPattern": [ 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0 ]
      }
    },
    {
      "Key": {
        "KeyName": "D",
        "OnOffPattern": [ 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ]
      }
    },
    {
      "Key": {
        "KeyName": "S",
        "OnOffPattern": [ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ]
      }
    },
    {
      "Key": {
        "KeyName": "R",
        "OnOffPattern": [ 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ]
      }
    }
  ]
}
```

You can add new keys and input sequences for them, by adding new `Key` objects to the JSON. Just remember to add the comma in after the previous element :p 
```
{
  "Key": {
    "KeyName": "Space",
    "OnOffPattern": [ 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ]
  }
}
```
