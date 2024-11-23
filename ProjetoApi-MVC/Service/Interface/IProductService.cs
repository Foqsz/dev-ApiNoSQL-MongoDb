using ProjetoApi_MVC.Models;

namespace ProjetoApi_MVC.Service.Interface;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetAsyncProduct(); 
    Task<ProductViewModel> GetAsyncProductById(string id);
    Task<ProductViewModel> PostProductService(ProductViewModel product);
    Task<ProductViewModel> UpdateProductService(string id, ProductViewModel product);
    Task<bool> DeleteProductService(string id);
}
