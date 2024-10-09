using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyContainer.Data;

namespace MyContainer.Pages.Transactions;

public class Index(ApplicationDbContext context) : PageModel
{
    public IList<Transaction> Transactions { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Transactions = await context.Transactions
            .AsNoTracking()
            .Include(t => t.User)
             .ToListAsync();
    }
}