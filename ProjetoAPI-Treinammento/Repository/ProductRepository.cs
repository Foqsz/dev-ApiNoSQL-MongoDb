using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjetoAPI_Treinammento.Data;
using ProjetoAPI_Treinammento.Models;
using ProjetoAPI_Treinammento.Repository.Interface;
using ProjetoAPI_Treinammento.Service;

namespace ProjetoAPI_Treinammento.Repository;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _productCollection;

    public ProductRepository(IOptions<ProductDbSettings> productRepository)
    {
        var mongoClient = new MongoClient(productRepository.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(productRepository.Value.DataBaseName);

        _productCollection = mongoDatabase.GetCollection<Product>(productRepository.Value.ProductCollectionName);
    }

    public async Task<IEnumerable<Product>> GetAsync()
    {
        var product = await _productCollection.Find(x => true).ToListAsync();
        return product;
    }

    public async Task<Product> GetAsyncById(string id)
    {
        var productbyId = await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        return productbyId;
    }

    public async Task<Product> CreateAsync(Product product)
    {
        var productExist = await _productCollection.Find(p => p.Nome == product.Nome).FirstOrDefaultAsync();

        if (productExist != null)
        {
            throw new Exception("Já existe um produto com esse nome, tente outro.");
        }

        await _productCollection.InsertOneAsync(product);
        return product;
    }
    public async Task<Product> UpdateAsync(string id, Product product)
    {
        var productExist = await _productCollection.Find(p => p.Nome == product.Nome).FirstOrDefaultAsync();

        if (productExist != null)
        {
            throw new Exception("Não é possivel atualizar para esse nome pois ele já existe.");
        }

        await _productCollection.ReplaceOneAsync(x => x.Id == id, product);
        return product;
    }

    public async Task DeleteAsync(string id)
    {
        await _productCollection.DeleteOneAsync(x => x.Id == id);
    }
}
