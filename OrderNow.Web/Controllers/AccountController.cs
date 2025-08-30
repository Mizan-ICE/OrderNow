using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using OrderNow.Domain.Identity;
using OrderNow.Infrastructure;
using OrderNow.Web.Models;

namespace OrderNow.Web.Controllers;
public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserStore<ApplicationUser> _userStore;
    private readonly IUserEmailStore<ApplicationUser> _emailStore;
    private readonly ILogger<RegisterModel> _logger;
    private readonly OrderNowDbContext _context;
    //private readonly IEmailUtility _emailUtility;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        IUserStore<ApplicationUser> userStore,
        SignInManager<ApplicationUser> signInManager,
        ILogger<RegisterModel> logger,
        OrderNowDbContext context
        /*IEmailUtility emailUtility*/)
    {
        _userManager = userManager;
        _userStore = userStore;
        _emailStore = GetEmailStore();
        _signInManager = signInManager;
        _logger = logger;
        _context = context;
        //_emailUtility = emailUtility;
    }
    public async Task<IActionResult>Users()
    {
        var users = await _context.Users.ToListAsync();
        return View( users );
    }
    public async Task<IActionResult> Login(string returnUrl = null)
    {
        var model = new LoginModel
        {
            ReturnUrl = returnUrl ?? Url.Content("~/")
        };

        if (!string.IsNullOrEmpty(model.ErrorMessage))
        {
            ModelState.AddModelError(string.Empty, model.ErrorMessage);
        }

        // Clear external cookies
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {

        model.ReturnUrl ??= Url.Content("~/");

        model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        if (!ModelState.IsValid) return View(model);

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
            var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);
            if (passwordCheck)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    return LocalRedirect(model.ReturnUrl);
                }
                
            }
            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(model);
        }

        TempData["Error"] = "Wrong credentials. Please, try again!";
        return View(model);
    }
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAsync(string returnUrl = null)
    {
        var model = new RegisterModel();

        model.ReturnUrl = returnUrl;
        model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel registerVM)
    {
        registerVM.ReturnUrl ??= Url.Content("~/");
        registerVM.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        if (!ModelState.IsValid) return View(registerVM);

        var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
        if (user != null)
        {
            TempData["Error"] = "This email address is already in use";
            return View(registerVM);
        }

        var newUser = new ApplicationUser()
        {
            FullName = registerVM.FullName,
            Email = registerVM.EmailAddress,
            UserName = registerVM.EmailAddress
        };
        IdentityResult newUserResponse;
        try
        {
            newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Exception: {ex.Message}");
            return View(registerVM);
        }

        if (newUserResponse.Succeeded)
            if (_userManager.Options.SignIn.RequireConfirmedAccount)
        {
            return RedirectToPage("RegisterConfirmation", new { email = registerVM.EmailAddress, returnUrl = registerVM.ReturnUrl });
        }
        else
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(registerVM.ReturnUrl);
        }


        return View(registerVM);
    }


    [Authorize]
    public async Task<IActionResult> LogoutAsync()
    {
        await _signInManager.SignOutAsync();


        return RedirectToAction("ProductList", "Product");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
    private IUserEmailStore<ApplicationUser> GetEmailStore()
    {
        if (!_userManager.SupportsUserEmail)
        {
            throw new NotSupportedException("The default UI requires a user store with email support.");
        }
        return (IUserEmailStore<ApplicationUser>)_userStore;
    }
    
}
