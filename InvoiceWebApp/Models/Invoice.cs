namespace InvoiceWebApp.Models;

public class Invoice

{
    public int Id { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<InvoiceItem> Items { get; set; } = new();
}
