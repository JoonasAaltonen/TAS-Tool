using System.CodeDom;

namespace TasUi.KeyboardHandlers
{
    public class MappedKey
    {
        public byte Key { get; set; }
        public string KeyName { get; set; }
        public bool IsPressed { get; set; }

        public MappedKey(byte key, string keyName, bool isPressed)
        {
            Key = key;
            KeyName = keyName;
            IsPressed = isPressed;
        }
    }
}