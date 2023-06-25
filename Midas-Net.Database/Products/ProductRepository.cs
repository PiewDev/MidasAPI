using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Midas.Net.Domain;
using Midas.Net.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Database.Products
{
  
    public class ProductRepository : IProductRepository
    {
        private readonly CommerceDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductRepository(CommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<decimal> GetAveragePriceByProductType(long productTypeId)
        {
            return await _dbContext.Products
                .Where(p => p.TypeId == productTypeId)
                .AverageAsync(p => p.Price);
        }
        public async Task<List<Product>> GetByIdAsync(List<long> idsEntity)
        {
            var products = await _dbContext.Products.Where(x => idsEntity.Contains(x.ProductId)).ToListAsync();

            return _mapper.Map<List<Product>>(products);
        }
        public async Task<List<Product>> GetByIdWithTypesAsync(List<long> idsEntity)
        {
            var products = await _dbContext.Products
                .Include(p => p.Type)
                .Where(x => idsEntity.Contains(x.ProductId)).ToListAsync();

            return _mapper.Map<List<Product>>(products);
        }

    }

}
