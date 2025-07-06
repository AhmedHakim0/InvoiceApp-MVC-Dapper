
namespace InvoiceWebApp.Controllers;
public class AccountController : Controller
{

   
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }


    // This action is used to redirect users to the landing page or the index page based on authentication status
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }


    // This action is used to display the registration page for new users
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // This action is used to display the account verification page after registration
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var captchaToken = Request.Form["g-recaptcha-response"];
        var (success, error) = await _accountService.LoginAsync(model, captchaToken);

        if (!success)
        {
            ModelState.AddModelError("", error);
            return View(model);
        }

        return RedirectToAction("Index", "Invoices");
    }

    // This action is used to register a new user account
    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = _accountService.RegisterUser(model, out string verifyUrl);

        if (!result.Success)
        {
            ModelState.AddModelError("Username", result.ErrorMessage);
            return View(model);
        }

        TempData["SuccessMessage"] = "Account created. Please verify your email later.";
        return RedirectToAction("Login");
    }


    // This action is used to verify the user's account after registration
    [HttpGet]
    public IActionResult Verify(string email, string token)
    {
        _accountService.VerifyAccount(email);
        ViewBag.Message = "Account Verified Successfully!";
        return View();
    }


    // This action is used to log out the user from the application
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _accountService.LogoutAsync();
        return RedirectToAction("Login");
    }

}
