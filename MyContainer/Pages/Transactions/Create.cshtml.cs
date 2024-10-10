using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyContainer.Data;
using MyContainer.Enums;

namespace MyContainer.Pages.Transactions;

public class Create : PageModel
{
    private readonly ApplicationDbContext _context;

    public Create(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public int CustomerId { get; set; }

    [BindProperty]
    public decimal CreditAmount { get; set; }

    [BindProperty]
    public decimal DebitAmount { get; set; }

    public User Customer { get; set; } = default!;
    public decimal RemainingBalance { get; set; }

    public async Task<IActionResult> OnGetAsync(int customerId)
    {
        Customer = await _context.Users.FindAsync(customerId);
        if (Customer == null)
        {
            return NotFound();
        }

        // Calculate remaining balance based on transactions
        RemainingBalance = _context.Transactions
            .Where(t => t.UserId == customerId)
            .Sum(t =>  t.CreditAmount-t.DebitAmount);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (CreditAmount > 0 || DebitAmount > 0)
        {
            var creditTransaction = new Transaction
            {
                UserId = CustomerId,
                CreditAmount = CreditAmount,
                DebitAmount = DebitAmount,
                TransactionType = TransactionType.Manual,
                CreatedAt = DateTime.Now
            };
            _context.Transactions.Add(creditTransaction);
        }
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}