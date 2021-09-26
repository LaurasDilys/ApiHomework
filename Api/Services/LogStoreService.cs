using Api.LogLocations;
using Business.Dto;
using Business.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Text.Json;

namespace Api.Services
{
    public class LogStoreService : ILogStoreService
    {
        private readonly Dictionary<string, ILogStoreLocation> locations = new Dictionary<string, ILogStoreLocation>
        {
            { "LogToConsole", new LogConsole() },
            { "LogToEmail", new LogEmail() },
            { "LogToFile", new LogFile() },
            { "LogToDb", new LogDb() }
        };
        
        private readonly LogStoreLocationOptions _options;
        private readonly ILogStoreLocation location;

        public LogStoreService(IOptions<LogStoreLocationOptions> options)
        {
            _options = options.Value;
            location = locations[_options.LogDestination];
        }

        public void Create(LogRequest request)
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

        public LogRequest All()
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

        private LogRequest DeserializeAll(LogResponseDtoArray logs)
        {
            int count = logs.Events.Length;
            var array = new LogDto[count];

            for (int i = 0; i < count; i++)
            {
                var log = logs.Events[i];
                array[i] = DeserializeOne(log);
            }

            return new LogRequest() { Events = array };
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
