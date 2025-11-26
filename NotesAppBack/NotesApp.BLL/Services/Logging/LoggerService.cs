using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesApp.BLL.Interfaces.Logging;
using Serilog;

namespace NotesApp.BLL.Services.Logging
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger _logger;

        public LoggerService(ILogger logger)
        {
            _logger = logger;
        }

        public void LogError(object request, string errorMsg)
        {
            string requestType = request.GetType().ToString();
            string requestClass = requestType.Substring(requestType.LastIndexOf('.') + 1);
            _logger.Error($"{requestClass} handled with the error: {errorMsg}");
        }

        public void LogInformation(string msg)
        {
            _logger.Information($"{msg}");
        }
    }
}
