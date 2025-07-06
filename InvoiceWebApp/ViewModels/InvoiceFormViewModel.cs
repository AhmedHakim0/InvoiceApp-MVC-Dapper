
namespace InvoiceWebApp.ViewModels;

public class InvoiceFormViewModel
{
    [Required]
    public DateTime InvoiceDate { get; set; } = DateTime.Now;

    public List<InvoiceItem> Items { get; set; } = new();

    public decimal TotalAmount => Items?.Sum(i => i.Total) ?? 0;

}
