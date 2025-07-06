
namespace InvoiceWebApp.Repositories;

public interface IInvoiceRepository
{
    Task AddInvoiceAsync(Invoice invoice);
    Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
    Task<Invoice> GetInvoiceByIdAsync(int id);
    Task DeleteInvoiceAsync(int id);
    Task UpdateInvoiceAsync(Invoice invoice);
    Task<int> GetTotalInvoiceCountAsync();
    Task<IEnumerable<Invoice>> GetPagedInvoicesAsync(int page, int pageSize);
}
