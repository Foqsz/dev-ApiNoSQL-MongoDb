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

    public async Task<List<Product>> GetAsync() => 
        await _productCollection.Find(x => true).ToListAsync();

    public async Task<Product> GetAsyncById(string id) =>
        await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Product product) =>
        await _productCollection.InsertOneAsync(product);

    public async Task UpdateAsync(string id, Product product) =>
        await _productCollection.ReplaceOneAsync(x => x.Id == id, product);

    public async Task RemoveAsync(string id) =>
        await _productCollection.DeleteOneAsync(x => x.Id == id);
}
