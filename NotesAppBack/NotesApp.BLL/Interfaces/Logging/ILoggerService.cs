using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.BLL.Interfaces.Logging
{
    public interface ILoggerService
    {
        void LogInformation(string msg);
        void LogError(object request, string errorMsg);
    }
}
