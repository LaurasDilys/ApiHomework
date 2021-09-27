using Api.LogLocations;
using Business.Dto;
using Business.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

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

        public LogResponseDtoArray All()
        {
            var readableLocation = location as IReadableLogLocation;

            return readableLocation.All();
        }

        public bool Exists(int key)
        {
            var readableLocation = location as IReadableLogLocation;

            if (!readableLocation.Exists(key))
                return false;

            return true;
        }
        public LogResponseDto Get(int key)
        {
            var readableLocation = location as IReadableLogLocation;

            return readableLocation.Get(key);
        }
       
    }
}
