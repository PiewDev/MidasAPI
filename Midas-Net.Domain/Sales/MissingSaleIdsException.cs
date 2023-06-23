using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Midas.Net.Domain;
namespace Midas.Net.Domain.Sales
{
    public class MissingSaleIdsException : InternalException
    {
        public List<long> MissingIds { get; }

        public MissingSaleIdsException(List<long> missingIds)
        {
            MissingIds = missingIds;
        }
    }
}
