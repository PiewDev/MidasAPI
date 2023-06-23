using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Domain.Log
{
    public class HttpToLog
    {
        public string Guid { get; set; }
        public string Log { get; set; }
        public string Origin { get; set; }
        public string Uri { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string UserOrigin { get; set; }
        public string Type { get; set; }

        public string LogText { get; set; }
        public virtual string ToString()
        {
            return LogText;
        }

    }
}
