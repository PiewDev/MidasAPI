using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Domain.Log
{
    public interface ILogService
    {
        Task<string> SaveLog(HttpToLog log);
    }
}
