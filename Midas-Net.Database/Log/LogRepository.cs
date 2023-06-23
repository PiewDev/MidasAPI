using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Midas.Net.Domain.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Database.Log
{
    public class LogRepository : ILogRepository
    {
        private readonly IMapper _mapper;

        protected readonly CommerceDbContext _dbContext;

        public LogRepository(IMapper mapper, CommerceDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext; 
        }

        public async Task<string> SaveLog(HttpToLog log)
        {
            var dbLog = _mapper.Map<DbLog>(log);
            _dbContext.Logs.Add(dbLog);
            await _dbContext.SaveChangesAsync();
            return dbLog.Guid;

        }

        public async Task<bool> SaveLog(string log)
        {
            var newLog = new DbLog();
            newLog.Guid = Guid.NewGuid().ToString();
            newLog.LogText = log;
            newLog.Datetime = DateTime.Now;
            newLog.Origin = "default";
            _dbContext.Logs.Add(newLog);

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
