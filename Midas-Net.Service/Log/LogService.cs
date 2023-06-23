using Midas.Net.Domain.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<string> SaveLog(HttpToLog log)
        {
            try
            {
                return await _logRepository.SaveLog(log);
            }
            catch (System.Exception e)
            {

                throw;
            }


        }

        public async Task<bool> SaveLog(string log)
        {
            try
            {
                return await _logRepository.SaveLog(log);
            }
            catch (System.Exception e)
            {

                throw;
            }

        }
    }
}
