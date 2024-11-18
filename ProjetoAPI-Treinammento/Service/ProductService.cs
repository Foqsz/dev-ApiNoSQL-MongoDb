using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjetoAPI_Treinammento.Data;
using ProjetoAPI_Treinammento.Models;

namespace ProjetoAPI_Treinammento.Service;

public class ProductService
{
    private readonly IMongoCollection<Product> _productCollection;

    public ProductService(IOptions<ProductDbSettings> productService)
    {
        var mongoClient = new MongoClient(productService.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(productService.Value.DataBaseName);

        _productCollection = mongoDatabase.GetCollection<Product>(productService.Value.ProductCollectionName);
    } 
}
