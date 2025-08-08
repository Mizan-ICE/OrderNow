using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderNow.Domain.Entities;
using OrderNow.Domain.Services;
using OrderNow.Web.Models.ViewModel;

namespace OrderNow.Web.Controllers;
public class CustomerController : Controller
{
    private ICustomerService _customerService;
    private IMapper _mapper;
    public CustomerController(ICustomerService customerService,
        IMapper mapper)
    {
        _customerService = customerService;
        _mapper = mapper;
    }
    public async Task<IActionResult> CustomerList()
    {
        var customer = await _customerService.GetAllAsync();
        var viewcustomer = _mapper.Map<List<CustomerVM>>(customer);
        return View(viewcustomer);
    }
    public IActionResult Add()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Add(CustomerVM model)
    {
        if (ModelState.IsValid)
        {
            var customer = _mapper.Map<Customer>(model);
            await _customerService.AddAsync(customer);
            return RedirectToAction("CustomerList");
        }
        return View(model);
    }
    public async Task<IActionResult> Update(int id)
    {
        var customer = await _customerService.GetByIdAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        var model = _mapper.Map<CustomerVM>(customer);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(CustomerVM model)
    {
        if (ModelState.IsValid)
        {
            var customer=_mapper.Map<Customer>(model);
          await  _customerService.UpdateAsync(customer);
            return RedirectToAction("CustomerList");
        }
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var customer =_customerService.GetByIdAsync(id);
        if(customer == null)
        {
            return NotFound();
        }
        await _customerService.DeleteAsync(id);
        return RedirectToAction("CustomerList");
    }
}
