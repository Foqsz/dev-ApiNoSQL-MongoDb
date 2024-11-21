using Newtonsoft.Json;
using ProjetoApi_MVC.Models;
using ProjetoApi_MVC.Service.Interface;
using System.Text.Json;

namespace ProjetoApi_MVC.Service;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string apiEndPoint = "/api/Product/";
    private readonly JsonSerializerOptions _options;
    private ProductViewModel _productViewModel;
    private IEnumerable<ProductViewModel> _products;

    public ProductService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<ProductViewModel>> GetAsyncProduct()
    {
        var client = _httpClientFactory.CreateClient("ProductAPI");

        // Fazendo a requisição para o endpoint "/api/Product"
        var response = await client.GetAsync("Product");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            // Deserializando a resposta em uma lista de produtos
            return JsonConvert.DeserializeObject<IEnumerable<ProductViewModel>>(content);
        }

        throw new HttpRequestException($"Erro ao buscar produtos: {response.StatusCode}");
    }

    public async Task<ProductViewModel> GetAsyncProductById(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductViewModel> PostProductService(ProductViewModel product)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductViewModel> UpdateProductService(string id, ProductViewModel product)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteProductService(string id)
    {
        throw new NotImplementedException();
    }
}
