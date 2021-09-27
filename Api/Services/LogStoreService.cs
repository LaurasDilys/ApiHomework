using Api.LogLocations;
using Api.Options;
using Business.Dto;
using Business.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Text.Json;

namespace Api.Services
{
    public class LogStoreService : ILogStoreService
    {
        private readonly MailOptions _mailSettings;
        private readonly LogStoreLocationOptions _logOptions;
        private readonly Dictionary<string, ILogStoreLocation> locations;
        private readonly ILogStoreLocation location;

        public LogStoreService(
            IOptions<LogStoreLocationOptions> logOptions,
            IOptions<MailOptions> mailSettings)
        {
            _mailSettings = mailSettings.Value;
            _logOptions = logOptions.Value;

            locations = new Dictionary<string, ILogStoreLocation>
            {
                { "LogToConsole", new LogToConsole() },
                { "LogToEmail", new LogToEmail(_mailSettings) },
                { "LogToFile", new LogToFile() },
                { "LogToDb", new LogToDb() }
            };

            location = locations[_logOptions.LogDestination];
        }

        public void Create(LogDtoArray request)
        {
            location.Create(request);
        }

        public bool LocationIsReadable()
        {
            var readableLocation = location as IReadableLogLocation;

            if (readableLocation == null)
                return false;

            return true;
        }

        public LogDtoArray All()
        {
            var readableLocation = location as IReadableLogLocation;

            return DeserializeAll(readableLocation.All());
        }

        public bool Exists(int key)
        {
            var readableLocation = location as IReadableLogLocation;

            if (!readableLocation.Exists(key))
                return false;

            return true;
        }

        public LogDto Get(int key)
        {
            var readableLocation = location as IReadableLogLocation;

            return DeserializeOne(readableLocation.Get(key));
        }

        private LogDtoArray DeserializeAll(LogResponseDtoArray logs)
        {
            int count = logs.Events.Length;
            var array = new LogDto[count];

            for (int i = 0; i < count; i++)
            {
                var log = logs.Events[i];
                array[i] = DeserializeOne(log);
            }

            return new LogDtoArray() { Events = array };
        }

        private LogDto DeserializeOne(LogResponseDto log)
        {
            return new LogDto
            {
                Timestamp = log.Timestamp,
                Level = log.Level,
                MessageTemplate = log.MessageTemplate,
                RenderedMessage = log.RenderedMessage,
                Properties = JsonSerializer.Deserialize<object>(log.Properties)
            };
        }
    }
}
