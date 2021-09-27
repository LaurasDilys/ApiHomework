using Business.Dto;
using Business.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Data.Model;
using Data;

namespace Api.LogLocations
{
    public class LogDb : ILogStoreLocation, IReadableLogLocation
    {
        public void Create(LogRequest request)
        {
            using (var db = new LogContext())
            {
                foreach (var ev in request.Events)
                {
                    Log log = new Log
                    {
                        Timestamp = ev.Timestamp,
                        Level = ev.Level,
                        MessageTemplate = ev.MessageTemplate,
                        RenderedMessage = ev.RenderedMessage,
                        Properties = JsonSerializer.Serialize(ev.Properties),
                    };
                    db.Add(log);
                }
                db.SaveChanges();
            }
        }

        public LogResponseDtoArray All()
        {
            using (var db = new LogContext())
            {
                var events = db.Logs.Select(log => new LogResponseDto
                {
                    Timestamp = log.Timestamp,
                    Level = log.Level,
                    MessageTemplate = log.MessageTemplate,
                    RenderedMessage = log.RenderedMessage,
                    Properties = log.Properties
                }).ToArray();
                return new LogResponseDtoArray { Events = events };
            }
        }

        public LogResponseDto Get(int key)
        {
            using (var db = new LogContext())
            {
                var log = db.Logs.Where(l => l.Id == key).First();
                
                return new LogResponseDto
                {
                    Timestamp = log.Timestamp,
                    Level = log.Level,
                    MessageTemplate = log.MessageTemplate,
                    RenderedMessage = log.RenderedMessage,
                    Properties = log.Properties
                };
            }
        }

        public bool Exists(int key)
        {
            using (var db = new LogContext())
            {
                return db.Logs.Where(l => l.Id == key).Any();

            }
        }

       
    }
}
