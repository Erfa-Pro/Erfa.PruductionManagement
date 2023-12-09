using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Erfa.ProductionManagement.Persistance.Repositories
{

    public class CatalogRepository : BaseRepository<Product>, ICatalogRepository
    {
        public CatalogRepository(ErfaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Product>> FindListOfProductsByProductNumbers(HashSet<string> productNumbers)
        {
            var Products = await _dbContext.Products
                                         .Where(e => productNumbers.Contains(e.ProductNumber))
                                         .ToListAsync();
            return Products;
        }

        public async Task<Product> GetByProductNumber(string ProductNumber)
        {
            return await _dbContext.Products
                 .Where(i => string
                    .Equals(i.ProductNumber, ProductNumber)
                  )
                 .FirstOrDefaultAsync();
        }
    }
}
