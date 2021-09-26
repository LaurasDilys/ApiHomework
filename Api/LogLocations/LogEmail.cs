using Business.Dto;
using Business.Interfaces;
using System;
using System.Text.Json;

namespace Api.LogLocations
{
    public class LogEmail : ILogStoreLocation
    {
        public void Create(LogRequest request)
        {
            // Simuliuojamas Email išsiuntimas

            Console.WriteLine($"--Email sent on {DateTime.Now}--\n");
            Console.WriteLine("Recently added logs:\n");

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
