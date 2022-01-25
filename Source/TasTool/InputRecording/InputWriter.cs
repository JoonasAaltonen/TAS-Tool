using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using TasTool.Interfaces;
using TasTool.Track;

namespace TasTool.InputRecording
{
    public class InputWriter : IInputWriter
    {
        private ITasConfig config;

        public InputWriter(ITasConfig config)
        {
            this.config = config;
        }

        public void WriteDataToFile(List<CommandData> inputList)
        {
            string timestamp = DateTime.Now.ToString("yyyy-M-d hh:mm:ss");
            string filePath = string.Concat(config.InputRecordingsLocation, @"\Inputs ", timestamp, ".csv");
            EnsureDirectoryExists(config.InputRecordingsLocation);

            using (StreamWriter streamWriter = File.CreateText(filePath))
            using (CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(inputList);
            }

        }

        private void EnsureDirectoryExists(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Directory.Exists)
            {
                System.IO.Directory.CreateDirectory(fileInfo.DirectoryName);
            }
        }
    }
}