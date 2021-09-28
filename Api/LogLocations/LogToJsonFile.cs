using Business.Dto;
using Business.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace Api.LogLocations
{
    public class LogToJsonFile : ILogStoreLocation, IReadableLogLocation
    {
        private string Path { get; set; }

        public LogToJsonFile()
        {
            Path = "Data.json";
            if (!File.Exists(Path))
            {
                var newFile = File.Create(Path);
                newFile.Close();
            }
        }

        public void Create(LogDtoArray request)
        {
            WriteToFile(request);
        }

        public LogResponseDtoArray All()
        {
            var allData = FileDataToDataSet();
            return new LogResponseDtoArray
            {
                Events = Convert(allData),
            };
        }

        public LogResponseDto Get(int key)
        {
            var allData = FileDataToDataSet();
            return GetById(key, allData);
        }

        public bool Exists(int key)
        {
            var allData = FileDataToDataSet();
            return allData.Exists(a => a.ID == key);
        }


        private void WriteToFile(LogDtoArray request)
        {
            int lastId = GetLastId();
            string jsonString = string.Empty;
            foreach (LogDto input in request.Events)
            {
                var inputData = (new DataSet() { ID = ++lastId, Log = input });
                jsonString = jsonString + JsonSerializer.Serialize(inputData, new JsonSerializerOptions() { WriteIndented = true }) + ";\n";
            }
            File.AppendAllText(Path, jsonString);
        }

        public int GetLastId()
        {
            return DataExists() ? FileDataToDataSet().Last().ID : 0;
        }

        public List<DataSet> FileDataToDataSet()
        {
            string data = File.ReadAllText(Path);
            var records = data.Split(";");
            List<DataSet> dataSets = new List<DataSet>();
            foreach (string record in records)
            {
                if (record != string.Empty && record != "\n")
                {
                    dataSets.Add(JsonSerializer.Deserialize<DataSet>(record));
                }
            }
            return dataSets;
        }

        private LogResponseDto[] Convert(List<DataSet> inp)
        {
            List<LogResponseDto> outputLog = new List<LogResponseDto>();
            foreach (DataSet value in inp)
            {
                outputLog.Add(value.LogToResponseDto());
            }
            return outputLog.ToArray();
        }

        private LogResponseDto GetById(int id, List<DataSet> inp)
        {
            return inp.Find(i => i.ID == id).LogToResponseDto();
        }

        private bool DataExists()
        {
            string data = File.ReadAllText(Path);
            return data != "";
        }

        public class DataSet
        {
            public int ID { get; set; }
            public LogDto Log { get; set; }

            public LogResponseDto LogToResponseDto()
            {
                return new LogResponseDto
                {
                    Timestamp = Log.Timestamp,
                    Level = Log.Level,
                    MessageTemplate = Log.MessageTemplate,
                    RenderedMessage = Log.RenderedMessage,
                    Properties = JsonSerializer.Serialize(Log.Properties),
                };
            }
        }
    }
}
