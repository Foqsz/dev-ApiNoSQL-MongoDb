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
        try
        {
            var products = await _productService.GetAsyncProduct();
            return StatusCode(StatusCodes.Status200OK, products);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter produtos: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetProductById(string id)
    {
        try
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
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter produto: {ex.Message}");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> PostProduct([FromBody] Product product)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdProduct = await _productService.PostProductService(product);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar produto: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> PutProduct(string id, Product product)
    {
        try
        {
            if (product is null || !ObjectId.TryParse(id, out _))
            {
                return StatusCode(StatusCodes.Status404NotFound, "Dados inválidos para alteração.");
            }

            await _productService.UpdateProductService(id, product);
            return StatusCode(StatusCodes.Status200OK, product);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar produto: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> RemoveProduct(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return StatusCode(StatusCodes.Status404NotFound, "Id não localizado no MongoDb");
            }

            await _productService.DeleteProductService(id);
            return StatusCode(StatusCodes.Status200OK, id);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar produto: {ex.Message}");
        }
    }
}
