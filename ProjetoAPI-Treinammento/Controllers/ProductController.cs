using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ProjetoAPI_Treinammento.Models;
using ProjetoAPI_Treinammento.Service;
using ProjetoAPI_Treinammento.Service.Interface;

namespace ProjetoAPI_Treinammento.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProduct()
    {
        var products = await _productService.GetAsyncProduct();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(string id)
    {
        if (!ObjectId.TryParse(id, out _))
        {
            return NotFound("Id não localizado no MongoDB.");
        }

        var productId = await _productService.GetAsyncProductById(id);

        if (productId == null)
        {
            return NotFound("Produto não encontrado.");
        }

        return Ok(productId);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
        await _productService.PostProductService(product);
        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> PutProduct(string id, Product product)
    {
        if (product is null || !ObjectId.TryParse(id, out _))
        {
            return NotFound("Dados inválidos para alteração.");
        }

        await _productService.UpdateProductService(id, product);

        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Product>> RemoveProduct(string id)
    {
        if (!ObjectId.TryParse(id, out _))
        {
            return NotFound("Id não localizado no MongoDb");
        }

        await _productService.DeleteProductService(id);

        return Ok(id);
    }
}
