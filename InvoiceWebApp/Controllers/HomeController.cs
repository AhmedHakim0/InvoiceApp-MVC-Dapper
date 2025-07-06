
namespace InvoiceWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public HomeController(ILogger<HomeController>logger,IStringLocalizer<SharedResource> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        // This action is used to redirect users to the landing page or the index page based on authentication status
        public IActionResult Landing()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Invoices");
            }
            ViewBag.Test = _localizer["Welcome"];
            return View();
        }



        // This action is used to display the index page of the application
        public IActionResult Index()
        {
           
            return View();
        }


        // This action is used to display the privacy policy page of the application
        public IActionResult Privacy()
        {
            return View();
        }


        // This action is used to display the error page of the application
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // This action is used to set the language preference for the user
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }



    }
}
