
using InvoiceWebApp.Services;

namespace InvoiceWebApp.Controllers;

[Authorize(AuthenticationSchemes = "MyCookieAuth")]
public class InvoicesController : Controller
{
    private readonly InvoiceService _invoiceService;

    public InvoicesController(InvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }


    // Show list of all invoices with optional search and pagination
    [HttpGet]
    public async Task<IActionResult> Index(int page = 1, int? searchId = null)
    {
        int pageSize = 5;

        var (invoices, totalPages) = await _invoiceService.GetInvoicesAsync(page, pageSize, searchId);

        var vm = new InvoiceListViewModel
        {
            Invoices = invoices,
            CurrentPage = page,
            TotalPages = totalPages
        };

        return View(vm);
    }


    // Show form to create a new invoice
    [HttpGet]
    public IActionResult Create()
    {
        var viewModel = new InvoiceFormViewModel
        {
            Items = new List<InvoiceItem> { new InvoiceItem() }
        };
        return View(viewModel);
    }


    // Handle form submission to create a new invoice
    [HttpPost]
    public async Task<IActionResult> Create(InvoiceFormViewModel model)
    {
        if (!ModelState.IsValid || model.Items == null || !model.Items.Any())
        {
            ModelState.AddModelError("", "Please add at least one item.");
            return View(model);
        }

        var invoice = new Invoice
        {
            InvoiceDate = model.InvoiceDate,
            Items = model.Items,
            TotalAmount = model.TotalAmount
        };

        await _invoiceService.AddInvoiceAsync(invoice);
        return RedirectToAction("Index");
    }


    // Show details of a specific invoice
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var invoice = await _invoiceService.GetInvoiceByIdAsync(id);

        if (invoice == null)
            return NotFound();

        return View(invoice);
    }

    // Show confirmation page to delete an invoice
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
        if (invoice == null) return NotFound();

        return View(invoice);
    }


    // Handle deletion of an invoice
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _invoiceService.DeleteInvoiceAsync(id);
        return RedirectToAction("Index");
    }


    // Show form to edit an existing invoice
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
        if (invoice == null)
            return NotFound();

        var viewModel = new InvoiceFormViewModel
        {
            InvoiceDate = invoice.InvoiceDate,
            Items = invoice.Items
        };

        ViewBag.InvoiceId = id;
        return View("Create", viewModel); 
    }

    // Handle form submission to update an existing invoice
    [HttpPost]
    public async Task<IActionResult> Edit(int id, InvoiceFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.InvoiceId = id;
            return View("Create", model);
        }

        var invoice = new Invoice
        {
            Id = id,
            InvoiceDate = model.InvoiceDate,
            Items = model.Items,
            TotalAmount = model.TotalAmount
        };

        await _invoiceService.UpdateInvoiceAsync(invoice);
        TempData["SuccessMessage"] = "Invoice updated successfully.";
        return RedirectToAction("Index");
    }

   

}
