using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderNow.Domain.Entities;
using OrderNow.Domain.Services;
using OrderNow.Web.Models.ViewModel;

namespace OrderNow.Web.Controllers;
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    public ProductController(
        IProductService productService,
        ICategoryService categoryService,
        IMapper mapper
        )
    {
        _productService = productService;  
        _categoryService = categoryService;
        _mapper = mapper;
    }
    public async Task<IActionResult> ProductList()
    {
      var product=await _productService.GetAllProduct();
       var viewproduct=_mapper.Map<IEnumerable< ProductVM >>(product);
        return View( viewproduct );
    }
    public async Task<IActionResult> Add()
    {
        var viewModel = new ProductVM
        {
            Categories =await _categoryService.AllCategoriesAsync()
                          
        };

        return View(viewModel);
    }
    [HttpPost]
    public async Task<IActionResult> Add(ProductVM viewModel)
    {
        if (ModelState.IsValid)
        {
            var product = _mapper.Map<Product>(viewModel);

           await _productService.AddProductAsync(product);
            TempData["ProductCreated"] = "New Product has been added.";
            return RedirectToAction("ProductList");
        }

        // If validation fails, re-populate categories
        viewModel.Categories =await _categoryService.AllCategoriesAsync();

        return View(viewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        var viewproduct=_mapper.Map<ProductVM>(product);    
        if(product == null)
        {
            return NotFound();
        }
        return View(viewproduct);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        var viewModel = _mapper.Map<ProductVM>(product);
        viewModel.Categories = await _categoryService.AllCategoriesAsync();
        return View(viewModel);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(ProductVM viewModel)
    {
        if (ModelState.IsValid)
        {
            var product = _mapper.Map<Product>(viewModel);
            await _productService.UpdateProductAsync(product);
            TempData["ProductUpdated"] = "Product has been updated.";
            return RedirectToAction("ProductList");
        }
        // If validation fails, re-populate categories
        viewModel.Categories = await _categoryService.AllCategoriesAsync();
        return View(viewModel);
    }
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if(product ==null)
        {
            return NotFound();
        }
        await _productService.DeleteProductAsync(id);
        return RedirectToAction("ProductList");
    }
}


