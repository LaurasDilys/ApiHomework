using System;
using Data.Model;
using Microsoft.EntityFrameworkCore;
namespace Data
{

    public class LogContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }

        public string DbPath { get; private set; }

        public LogContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}logger.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
