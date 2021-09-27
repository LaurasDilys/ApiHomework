using Business.Dto;
using Business.Interfaces;
using System;
using System.Text.Json;

namespace Api.LogLocations
{
    public class LogToConsole : ILogStoreLocation
    {
        public void Create(LogDtoArray request)
        {
            foreach (var item in request.Events)
            {
                Console.WriteLine($"Timestamp: {item.Timestamp}");
                Console.WriteLine($"Level: {item.Level}");
                Console.WriteLine($"MessageTemplate: {item.MessageTemplate}");
                Console.WriteLine($"RenderedMessage: {item.RenderedMessage}");
                Console.WriteLine($"Properties: {JsonSerializer.Serialize(item.Properties)}");
                Console.WriteLine();
            }
        }
    }
}
