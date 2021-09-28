using Business.Dto;
using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Api.LogLocations
{
    public class LogToTxtFile : ILogStoreLocation, IReadableLogLocation
    {
        private string path { get; set; }

        public LogToTxtFile()
        {
            path = @"Data.txt";
            if (!File.Exists(path))
            {
                var newFile = File.Create(path);
                newFile.Close();
            }
        }

        public void Create(LogDtoArray request)
        {
            foreach (var log in request.Events)
                AddLog(From(log));
        }

        public LogResponseDtoArray All()
        {
            var logs = new List<LogResponseDto>();
            for (int i = 1; i < NewId(); i++)
                logs.Add(From(i));

            return new LogResponseDtoArray
            {
                Events = logs.ToArray()
            };
        }

        public LogResponseDto Get(int key)
        {
            return From(key);
        }

        public bool Exists(int key)
        {
            return IdLines()
                .Select(x => int.Parse(x))
                .Any(y => y == key);
        }

        private void AddLog(List<string> text)
        {
            File.AppendAllLines(path, text);
        }

        private string[] AllLines()
        {
            return File.ReadAllLines(path);
        }

        private string[] IdLines()
        {
            return AllLines()
                .Where(s => s.All(c => Char.IsDigit(c))).ToArray();
        }

        private int NewId()
        {
            var ids = IdLines();

            return ids.Any() ?
                ids.Max(x => int.Parse(x)) + 1 : 1;
        }

        private List<string> From(LogDto log)
        {
            return new List<string>
            {
                NewId().ToString(),
                $"Timestamp:{log.Timestamp}",
                $"Level:{log.Level}",
                $"MessageTemplate:{log.MessageTemplate}",
                $"RenderedMessage:{log.RenderedMessage}",
                $"Properties:{JsonSerializer.Serialize(log.Properties)}"
            };
        }

        private LogResponseDto From(int key)
        {
            var lines = AllLines();

            int idIndex = Array.IndexOf(lines, key.ToString());
            int nextIdIndex = Array.IndexOf(lines, (key + 1).ToString());
            int stop = nextIdIndex == -1 ? lines.Length : nextIdIndex;

            List<string> values = new List<string>();
            for (int i = idIndex + 1; i < stop; i++)
                values.Add(lines[i].Substring(lines[i].IndexOf(':') + 1));

            return new LogResponseDto
            {
                Timestamp = DateTime.Parse(values[0]),
                Level = values[1],
                MessageTemplate = values[2],
                RenderedMessage = values[3],
                Properties = values[4]
            };
        }
    }
}
