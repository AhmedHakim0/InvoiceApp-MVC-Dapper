

namespace InvoiceWebApp.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly  DapperContext _context;

    public InvoiceRepository(DapperContext context)
    {
        _context = context;
    }

    // Adds a new invoice along with its items to the database
    public async Task AddInvoiceAsync(Invoice invoice)
    {
        using var connection = _context.CreateConnection();

        var table = new DataTable();
        table.Columns.Add("ProductName", typeof(string));
        table.Columns.Add("Quantity", typeof(int));
        table.Columns.Add("UnitPrice", typeof(decimal));

        foreach (var item in invoice.Items)
        {
            table.Rows.Add(item.ProductName, item.Quantity, item.UnitPrice);
        }

        var parameters = new DynamicParameters();
        parameters.Add("@TotalAmount", invoice.TotalAmount);
        parameters.Add("@InvoiceDate", invoice.InvoiceDate);
        parameters.Add("@Items", table.AsTableValuedParameter("InvoiceItemType"));

        await connection.ExecuteAsync("sp_AddInvoiceWithItems", parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
    {
        using var connection = _context.CreateConnection();

        var query = "SELECT * FROM vw_InvoiceSummary";
        var invoices = await connection.QueryAsync<Invoice>(query);
        return invoices;
    }

    public async Task<Invoice> GetInvoiceByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();

    
        var invoiceQuery = "SELECT * FROM Invoices WHERE Id = @Id";
        var invoice = await connection.QuerySingleOrDefaultAsync<Invoice>(invoiceQuery, new { Id = id });

        if (invoice == null)
            return null;


        var itemsQuery = "SELECT ProductName, Quantity, UnitPrice FROM InvoiceItems WHERE InvoiceId = @Id";
        var items = await connection.QueryAsync<InvoiceItem>(itemsQuery, new { Id = id });

        invoice.Items = items.ToList();
        return invoice;
    }

 
    public async Task DeleteInvoiceAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var query = "DELETE FROM Invoices WHERE Id = @Id";
        await connection.ExecuteAsync(query, new { Id = id });
    }

    
    public async Task UpdateInvoiceAsync(Invoice invoice)
    {
        using var connection = _context.CreateConnection();

        var deleteQuery = "DELETE FROM InvoiceItems WHERE InvoiceId = @Id";
        await connection.ExecuteAsync(deleteQuery, new { invoice.Id });

        var updateInvoiceQuery = "UPDATE Invoices SET InvoiceDate = @InvoiceDate, TotalAmount = @TotalAmount WHERE Id = @Id";
        await connection.ExecuteAsync(updateInvoiceQuery, invoice);

        var insertQuery = @"INSERT INTO InvoiceItems (InvoiceId, ProductName, Quantity, UnitPrice)
                        VALUES (@InvoiceId, @ProductName, @Quantity, @UnitPrice)";

        foreach (var item in invoice.Items)
        {
            await connection.ExecuteAsync(insertQuery, new
            {
                InvoiceId = invoice.Id,
                item.ProductName,
                item.Quantity,
                item.UnitPrice
            });
        }
    }

    // Retrieves a paginated list of invoices
    public async Task<IEnumerable<Invoice>> GetPagedInvoicesAsync(int page, int pageSize)
    {
        using var connection = _context.CreateConnection();

        int offset = (page - 1) * pageSize;

        string sql = @"
        SELECT * FROM Invoices
        ORDER BY InvoiceDate DESC
        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

        return await connection.QueryAsync<Invoice>(sql, new { Offset = offset, PageSize = pageSize });
    }

    public async Task<int> GetTotalInvoiceCountAsync()
    {
        using var connection = _context.CreateConnection();
        string sql = "SELECT COUNT(*) FROM Invoices";
        return await connection.ExecuteScalarAsync<int>(sql);
    }


}
