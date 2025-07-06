
namespace InvoiceWebApp.Models;

public class InvoiceItem
{
    [Required]
    [RegularExpression(@".*[A-Za-z].*", ErrorMessage = "Product name must contain at least one letter.")]
    public string ProductName { get; set; } = string.Empty;

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public decimal Total => Quantity * UnitPrice;
}
