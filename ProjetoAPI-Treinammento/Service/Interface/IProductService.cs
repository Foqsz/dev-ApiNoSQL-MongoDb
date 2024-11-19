using ProjetoAPI_Treinammento.Models;

namespace ProjetoAPI_Treinammento.Service.Interface;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAsyncProduct();
    Task<Product> GetAsyncProductById(string id);
    Task<Product> PostProductService(Product product);
    Task<Product> UpdateProductService(string id, Product product);
    Task DeleteProductService(string id);
}
