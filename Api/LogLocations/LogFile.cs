using Business.Dto;
using Business.Interfaces;
using System;

namespace Api.LogLocations
{
    public class LogFile : ILogStoreLocation, IReadableLogLocation
    {
        public void Create(LogRequest request)
        {
            throw new NotImplementedException();
        }

        public LogResponseDtoArray All()
        {
            throw new NotImplementedException();
        }

        public LogResponseDto Get(int key)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int key)
        {
            throw new NotImplementedException();
        }

        public LogResponseDto Get()
        {
            throw new NotImplementedException();
        }
    }
}
