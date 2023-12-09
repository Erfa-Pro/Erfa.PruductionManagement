using Erfa.PruductionManagement.Domain.Entities;

namespace Erfa.PruductionManagement.Application.Contracts.Persistance
{
    public interface ICatalogRepository : IAsyncRepository<Product>
    {
        Task<Product> GetByProductNumber(string ProductNumber);
        Task<List<Product>> FindListOfProductsByProductNumbers(HashSet<string> productNumberds);

    }
}
