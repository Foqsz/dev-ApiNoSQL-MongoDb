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
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Product>>> GetProduct()
    {
        var products = await _productService.GetAsyncProduct();
        return StatusCode(StatusCodes.Status200OK, products);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetProductById(string id)
    {
        if (!ObjectId.TryParse(id, out _))
        {
            return StatusCode(StatusCodes.Status404NotFound, "Id não localizado no MongoDB.");
        }

        var productId = await _productService.GetAsyncProductById(id);

        if (productId == null)
        {
            return StatusCode(StatusCodes.Status404NotFound, "Produto não encontrado.");
        }

        return StatusCode(StatusCodes.Status200OK, productId);
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
        await _productService.PostProductService(product);
        return StatusCode(StatusCodes.Status200OK, product);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> PutProduct(string id, Product product)
    {
        if (product is null || !ObjectId.TryParse(id, out _))
        {
            return StatusCode(StatusCodes.Status404NotFound, "Dados inválidos para alteração.");
        }

        await _productService.UpdateProductService(id, product);
        return StatusCode(StatusCodes.Status200OK, product);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> RemoveProduct(string id)
    {
        if (!ObjectId.TryParse(id, out _))
        {
            return StatusCode(StatusCodes.Status404NotFound, "Id não localizado no MongoDb");
        }

        await _productService.DeleteProductService(id);
        return StatusCode(StatusCodes.Status200OK, id);
    }
}
