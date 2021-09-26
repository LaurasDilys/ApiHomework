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

        private readonly ILogStoreLocation location;
        private readonly LogStoreLocationOptions _options;

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

            return readableLocation.All();
        }

        public bool Exists(string key)
        {
            var readableLocation = location as IReadableLogLocation;

            if (!readableLocation.Exists(key))
                return false;

            return true;
        }

        public LogDto Get(string key)
        {
            var readableLocation = location as IReadableLogLocation;

            return readableLocation.Get(key);
        }
    }
}
