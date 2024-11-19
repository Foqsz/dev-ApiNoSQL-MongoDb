using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjetoAPI_Treinammento.Data;
using ProjetoAPI_Treinammento.Models;
using ProjetoAPI_Treinammento.Repository.Interface;
using ProjetoAPI_Treinammento.Service.Interface;

namespace ProjetoAPI_Treinammento.Service;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetAsyncProduct()
    {
        var productList = await _productRepository.GetAsync();
        return productList;
    }

    public async Task<Product> GetAsyncProductById(string id)
    {
        var productById = await _productRepository.GetAsyncById(id);
        return productById;
    }

    public async Task<Product> PostProductService(Product product)
    {
        var createProduct = await _productRepository.CreateAsync(product);
        return createProduct;
    }

    public async Task<Product> UpdateProductService(string id, Product product)
    {
        var updateProduct = await _productRepository.UpdateAsync(id, product);
        return updateProduct;
    }

    public async Task DeleteProductService(string id)
    {
        await _productRepository.DeleteAsync(id);
    }
}
