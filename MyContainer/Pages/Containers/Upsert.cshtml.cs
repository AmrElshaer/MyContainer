using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyContainer.Data;

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
        if (Container.Id == 0)
        {
            context.Containers.Add(Container);
        }
        else
        {
            context.Attach(Container).State = EntityState.Modified;
        }

        await context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}