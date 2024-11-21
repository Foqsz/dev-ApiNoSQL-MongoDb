using Newtonsoft.Json;
using ProjetoApi_MVC.Models;
using ProjetoApi_MVC.Service.Interface;
using System.Text.Json;

namespace ProjetoApi_MVC.Service;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string apiEndPoint = "/api/Product";
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

        var url = apiEndPoint;

        var response = await client.GetAsync(url);

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
        var client = _httpClientFactory.CreateClient("ProductAPI");

        // Montando a URL com o ID como parte do caminho da URL
        var url = $"{apiEndPoint}/{id}";

        var response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            // Deserializando a resposta em um objeto ProductViewModel
            return JsonConvert.DeserializeObject<ProductViewModel>(content);
        }

        throw new HttpRequestException($"Erro ao buscar produto Id {id}: {response.StatusCode}");
    } 

    public async Task<ProductViewModel> PostProductService(ProductViewModel product)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductViewModel> UpdateProductService(string id, ProductViewModel product)
    {
        var client = _httpClientFactory.CreateClient("ProductAPI");
          
        var url = $"{apiEndPoint}?id={id}";

        ProductViewModel productUpdated = new ProductViewModel();

        using (var response = await client.PutAsJsonAsync(url, product))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                productUpdated = await System.Text.Json.JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return productUpdated;
    }


    public async Task DeleteProductService(string id)
    {
        throw new NotImplementedException();
    }
}
