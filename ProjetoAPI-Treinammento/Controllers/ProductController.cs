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

    /// <summary>
    /// Retrieves all products.
    /// </summary>
    /// <returns>A list of products.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Product>>>GetProduct()
    {
        try
        {
            var products = await _productService.GetAsyncProduct();
            await Task.Delay(3000);
            return StatusCode(StatusCodes.Status200OK, products);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter produtos: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product.</param>
    /// <returns>The product with the specified ID.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetProductById([FromRoute] string id)
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

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="product">The product to create.</param>
    /// <returns>The created product.</returns>
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

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="id">The ID of the product to update.</param>
    /// <param name="product">The updated product data.</param>
    /// <returns>The updated product.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> PutProduct([FromRoute] string id, [FromBody] Product product)
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

    /// <summary>
    /// Deletes a product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to delete.</param>
    /// <returns>The ID of the deleted product.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> RemoveProduct([FromRoute] string id)
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
