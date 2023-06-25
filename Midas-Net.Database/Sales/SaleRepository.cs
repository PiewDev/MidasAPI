using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Midas.Net.Database.Products;
using Midas.Net.Domain;
using Midas.Net.Domain.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Midas.Net.Database.Sales
{
    public class SaleRepository : ISaleRepository
    {
        private readonly CommerceDbContext _dbContext;
        private readonly IMapper _mapper;

        public SaleRepository(CommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<Sale>> GetSalesByDateAsync(DateTime date)
        {
            var dbEntities = await _dbContext.Set<DbSale>()
              .Where(s => s.Date.Date == date.Date)
                .ToListAsync();
            return _mapper.Map<List<Sale>>(dbEntities);
        }


    }
}
