using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderNow.Domain.Entities;
using OrderNow.Domain.Services;
using OrderNow.Web.Models.ViewModel;

namespace OrderNow.Web.Controllers;
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    public CategoryController(ICategoryService categoryService,IMapper mapper)
    {
       _categoryService = categoryService;
        _mapper = mapper;
    }
    public IActionResult NewCategory()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> NewCategory(CategoryVm categoryVm)
    {
        if (ModelState.IsValid)
        {
            var category = _mapper.Map<Category>(categoryVm);
           await _categoryService.AdAsync(category);
            return RedirectToAction("Add","Product");
        }
        return View(categoryVm);
        
    }
}
