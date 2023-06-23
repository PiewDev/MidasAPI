using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Domain.Log
{
    public interface ILogRepository
    {
        public Task<string> SaveLog(HttpToLog log);
        public Task<bool> SaveLog(string log);

    }
}
