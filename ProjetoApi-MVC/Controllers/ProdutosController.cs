using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetoApi_MVC.Models;
using ProjetoApi_MVC.Service.Interface;

namespace ProjetoApi_MVC.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProductService _productService;

        public ProdutosController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {
            var produtos = await _productService.GetAsyncProduct();

            return produtos is null ? View("Error") : View(produtos);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            var product = await _productService.GetAsyncProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(string id, ProductViewModel product)
        {
            if (!ModelState.IsValid) return View(product);
            var existingProduct = await _productService.GetAsyncProductById(id);

            if (existingProduct == null) return View(product);
            await _productService.UpdateProductService(id, product);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.Id = new SelectList(await _productService.GetAsyncProduct(), "id", "nome");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductViewModel product)
        {
            if (!ModelState.IsValid) return View(product);
            var newProduct = await _productService.PostProductService(product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await _productService.GetAsyncProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);  
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(ProductViewModel product)
        {
            if (!ModelState.IsValid) return View();
            var deleteProduct = await _productService.DeleteProductService(product.Id);
            return RedirectToAction("Index");
        }
    }
}
