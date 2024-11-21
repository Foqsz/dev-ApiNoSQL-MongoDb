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

    }
}
