using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyContainer.Data;
using MyContainer.Enums;

namespace MyContainer.Pages.Containers;

public class Upsert(ApplicationDbContext context) : PageModel
{
    [BindProperty]
    public Container Container { get; set; } = default!;
    public SelectList Customers { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        Customers = new SelectList(context.Users, "Id", "Name");

        if (id == null)
        {
            // Create
            Container = new Container();
        }
        else
        {
            // Edit
            Container = await context.Containers.FindAsync(id);
            if (Container == null)
            {
                return NotFound();
            }
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Container.Remaining < 0)
        {
            return Page();
            
        }
       
        if (Container.Id == 0)
        {
            context.Containers.Add(Container);
        }
        else
        {
            context.Attach(Container).State = EntityState.Modified;
        }

        if (Container.Remaining>0)
        {
            var creditTransaction=new Transaction
            {
                UserId=Container.UserId,
                CreatedAt = DateTime.Now,
                TransactionType = TransactionType.Credit,
                Amount = Container.Remaining,
            };
            await context.Transactions.AddAsync(creditTransaction);
        }
       
        

        await context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}