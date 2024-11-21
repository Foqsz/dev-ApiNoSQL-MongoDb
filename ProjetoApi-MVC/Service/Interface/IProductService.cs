﻿using ProjetoApi_MVC.Models;

namespace ProjetoApi_MVC.Service.Interface;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetAsyncProduct();
    Task<ProductViewModel> PostProductService(ProductViewModel product);
    Task<ProductViewModel> UpdateProductService(string id, ProductViewModel product);
    Task DeleteProductService(string id);
}
