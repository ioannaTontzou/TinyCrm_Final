using System;
using System.Collections.Generic;
using System.Text;
using Serilog;


namespace TinyCrm.Core.Services
{
    class LoggerService : ILoggerService
    {
        public void LogError(string errorcode, string text)
        {
            var Log = new LoggerConfiguration()
              .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
              .CreateLogger();

            Log.Error(errorcode);
            Log.Information(text);


        }

        public void LogInformation(string text)
        {
            var Log = new LoggerConfiguration()
              .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
              .CreateLogger();

            Log.Information(text);
        }
    }
}
