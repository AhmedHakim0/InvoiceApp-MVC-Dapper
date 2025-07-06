

namespace InvoiceWebApp.Helpers;

public class GoogleCaptchValidator
{
    private readonly string _secretKey;

    public GoogleCaptchValidator(IOptions<GoogleReCaptchaSettings> options)
    {
        _secretKey = options.Value.SecretKey;
    }

    // Validates the reCAPTCHA token by sending it to Google's reCAPTCHA API.
    public async Task<bool> IsCaptchaValid(string token)
    {
        var httpClient = new HttpClient();
        var response = await httpClient.PostAsync(
            $"https://www.google.com/recaptcha/api/siteverify?secret={_secretKey}&response={token}",
            null);

        var jsonString = await response.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonElement>(jsonString);

        return json.GetProperty("success").GetBoolean();
    }
}
