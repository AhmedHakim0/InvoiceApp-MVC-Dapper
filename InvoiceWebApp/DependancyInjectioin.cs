
namespace InvoiceWebApp;

public static class DependancyInjectioin
{
    // This method is used to register services and dependencies in the application.
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
       services.AddControllersWithViews();
       services.Configure<GoogleReCaptchaSettings>(configuration.GetSection("GoogleReCaptcha"));
       services.Configure<SmtpSettings>(configuration.GetSection("Smtp"));
       services.AddAuthentication("MyCookieAuth")
                      .AddCookie("MyCookieAuth", options =>
                      {
                          options.LoginPath = "/Account/Login";
                          options.AccessDeniedPath = "/Account/Login";
                      });


        services.AddSingleton<DapperContext>();
        services.AddScoped<GoogleCaptchValidator>();
        services.AddScoped<EmailSender>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<InvoiceService>();
        services.AddScoped<AccountService>();
        services.AddHttpContextAccessor(); // مهم جدًا لتسجيل الدخول والخروج
        services.AddAppLocalization();



        return services;
    }

    // This method is used to configure localization for the application.
    public static IServiceCollection AddAppLocalization(this IServiceCollection services)
    {

        var supportedCultures = new[] { "en", "ar" };

        services.AddLocalization(options => options.ResourcesPath = "Resources");

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("ar") };

            options.DefaultRequestCulture = new RequestCulture("en");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });

        return services;
    }

}
