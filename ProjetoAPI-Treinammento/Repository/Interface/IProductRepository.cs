using ProjetoAPI_Treinammento.Models;

namespace ProjetoAPI_Treinammento.Repository.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAsync();
        Task<Product> GetAsyncById(string id);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(string id, Product product);
        Task DeleteAsync(string id);
    }
}
