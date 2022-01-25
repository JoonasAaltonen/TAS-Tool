using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using TasTool.InputRecording;
using TasTool.Interfaces;

namespace TasTool.Track
{
    public class TrackParser : ITrackParser
    {
        public List<CommandData> ParseTrack(string filePath, out string debugMessage, out bool success)
        {
            List<CommandData> parsedTrackData = new List<CommandData>();
            try
            {
                using (StreamReader streamReader = File.OpenText(filePath))
                using (CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    parsedTrackData = csvReader.GetRecords<CommandData>().ToList();
                }

                if (IsParsingSuccessful(parsedTrackData))
                {
                    debugMessage = "Track data successfully parsed";
                    success = true;
                    return parsedTrackData;
                }
                debugMessage = "Track command file parsing failed or file does not include any commands.";
                success = false;
                return parsedTrackData;

            }
            catch (Exception e)
            {
                debugMessage = string.Format("Exception in parsing track command data file: \n{0}", e.Message);
                success = false;
                return parsedTrackData;
            }
            
        }

        private bool IsParsingSuccessful(IEnumerable<CommandData> data)
        {
            if (data.Any(x => x.KeyCode != null))
            {
                return true;
            }

            return false;
        }


    }
}