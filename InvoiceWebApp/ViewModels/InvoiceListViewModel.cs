
namespace InvoiceWebApp.ViewModels;

public class InvoiceListViewModel
{
    public IEnumerable<Invoice> Invoices { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
