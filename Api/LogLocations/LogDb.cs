using Business.Dto;
using Business.Interfaces;
using System;

namespace Api.LogLocations
{
    public class LogDb : ILogStoreLocation, IReadableLogLocation
    {
        public void Create(LogRequest request)
        {
            throw new NotImplementedException();
        }

        public LogRequest All()
        {
            throw new NotImplementedException();
        }

        public LogDto Get(string key)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string key)
        {
            throw new NotImplementedException();
        }
    }
}
