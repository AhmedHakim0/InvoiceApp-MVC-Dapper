namespace InvoiceWebApp.Services;

public class InvoiceService
{
    private readonly IInvoiceRepository _invoiceRepo;

    public InvoiceService(IInvoiceRepository invoiceRepo)
    {
        _invoiceRepo = invoiceRepo;
    }

    public async Task<(IEnumerable<Invoice> Invoices, int TotalPages)> GetInvoicesAsync(int page, int pageSize, int? searchId)
    {
        if (searchId != null)
        {
            var invoice = await _invoiceRepo.GetInvoiceByIdAsync(searchId.Value);
            var list = invoice != null ? new List<Invoice> { invoice } : new List<Invoice>();
            return (list, 1);
        }

        var totalCount = await _invoiceRepo.GetTotalInvoiceCountAsync();
        var invoices = await _invoiceRepo.GetPagedInvoicesAsync(page, pageSize);
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        return (invoices, totalPages);

    }

    public async Task AddInvoiceAsync(Invoice invoice)
    {
        await _invoiceRepo.AddInvoiceAsync(invoice);
    }

    public async Task<Invoice?> GetInvoiceByIdAsync(int id)
    {
        return await _invoiceRepo.GetInvoiceByIdAsync(id);
    }

    public async Task UpdateInvoiceAsync(Invoice invoice)
    {
        await _invoiceRepo.UpdateInvoiceAsync(invoice);
    }

    public async Task DeleteInvoiceAsync(int id)
    {
        await _invoiceRepo.DeleteInvoiceAsync(id);
    }


}
