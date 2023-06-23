using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Database.Log
{
    public class DbLog
    {
        public int Id { get; set; }

        public string? Guid { get; set; }

        public string? LogText { get; set; }

        public DateTime? Datetime { get; set; }

        public string? Origin { get; set; }

        public string? Uri { get; set; }

        public string? Header { get; set; }

        public string? Type { get; set; }

        public string? UserOrigin { get; set; }

        public string? StatusCode { get; set; }

        public string? Body { get; set; }
    }
}
