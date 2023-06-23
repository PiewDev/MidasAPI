using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Midas.Net.Domain.Products;
using Midas.Net.Domain.Products.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Database.Products
{
    public class ProductByTypeRepository: IProductByTypeRepository
    {
        private readonly CommerceDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProductByTypeRepository(CommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<Product>> GetProductsByTypeAsync(long typeId)
        {
            var products = await _dbContext.Products
                .Where(p => p.TypeId == typeId && !p.IsDeleted)
                .ToListAsync();

            return _mapper.Map<List<Product>>(products);
        }
    }
}
