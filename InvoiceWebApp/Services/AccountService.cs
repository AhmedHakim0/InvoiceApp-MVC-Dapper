
namespace InvoiceWebApp.Services
{
    public class AccountService
    {
        private readonly IUserRepository _userRepo;
        private readonly GoogleCaptchValidator _captchaValidator;
        private readonly EmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(
            IUserRepository userRepo,
            GoogleCaptchValidator captchaValidator,
            EmailSender emailSender,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepo = userRepo;
            _captchaValidator = captchaValidator;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
        }

        //Attempts to log in a user after verifying the reCAPTCHA and credentials.
        public async Task<(bool Success, string ErrorMessage)> LoginAsync(LoginViewModel model, string captchaToken)
        {
            // Validate CAPTCHA before proceeding
            var isCaptchaValid = await _captchaValidator.IsCaptchaValid(captchaToken);
            if (!isCaptchaValid)
                return (false, "Captcha verification failed.");

            // Check username and password
            var user = _userRepo.GetUserByUsernameAndPassword(model.Username, model.Password);
            if (user == null)
                return (false, "Invalid username or password");

            // Build authentication claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var principal = new ClaimsPrincipal(identity);

            // Sign the user in using cookie authentication
            await _httpContextAccessor.HttpContext.SignInAsync("MyCookieAuth", principal);

            return (true, string.Empty);
        }

        public (bool Success, string ErrorMessage) RegisterUser(RegisterViewModel model, out string verificationUrl)
        {
            verificationUrl = string.Empty;

            if (_userRepo.UsernameExists(model.Username))
                return (false, "Username is already taken");

            // Create user object from registration data
            var user = new User
            {
                FullName = model.FullName,
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                Phone = model.Phone,
                IsVerified = false
            };

            _userRepo.AddUser(user);

            // Generate a fake verification token (not stored currently)
            string token = Guid.NewGuid().ToString(); 
            verificationUrl = $"/Account/Verify?email={model.Email}&token={token}";

            // Send verification email
            string body = $"Click here to verify your email: <a href='{verificationUrl}'>Verify Account</a>";
            _emailSender.SendEmail(model.Email, "Account Verification", body);

            return (true, string.Empty);
        }

        public void VerifyAccount(string email)
        {
            _userRepo.VerifyUserByEmail(email);
        }

        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync("MyCookieAuth");
        }
    }
}
