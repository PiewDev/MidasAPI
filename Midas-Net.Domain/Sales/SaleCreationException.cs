using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Midas.Net.Domain;
namespace Midas.Net.Domain.Sales
{
    public class SaleCreationException : InternalException
    {
        public List<long> MissingIds { get; }
        public List<long> InsufficientStockIds { get; }
        public SaleCreationException(List<long> missingIds, List<long> insufficientStockIds)
        : base(GenerateErrorMessage(missingIds, insufficientStockIds))
        {
            MissingIds = missingIds;
            InsufficientStockIds = insufficientStockIds;
        }

        private static string GenerateErrorMessage(List<long> missingIds, List<long> insufficientStockIds)
        {
            var errorMessage = new StringBuilder();

            if (missingIds.Any())
            {
                errorMessage.AppendLine($"The following sale IDs do not exist: {string.Join(", ", missingIds)}");
            }

            if (insufficientStockIds.Any())
            {
                errorMessage.AppendLine($"Insufficient stock for the following products: {string.Join(", ", insufficientStockIds)}");
            }

            return errorMessage.ToString();
        }

       
    }
}
