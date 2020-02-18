using System;
using System.Collections.Generic;
using System.Text;
using Serilog.Core;

namespace TinyCrm.Core.Services
{
    interface ILoggerService
    {
        void LogError(string errorcode, string text);
        void LogInformation(string text);
    }
}
