using Microsoft.AspNetCore.Mvc;
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

            if (produtos is null)
            {
                return View("Error");
            }

            return View(produtos);  
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
            if (ModelState.IsValid)
            {  
                var existingProduct = await _productService.GetAsyncProductById(id);

                if (existingProduct != null)
                { 
                    await _productService.UpdateProductService(id, product);  
                    return RedirectToAction("Index");  
                }
            }

            return View(product);
        }


    }
}
