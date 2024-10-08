using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyContainer.Data;

namespace MyContainer.Pages.Users;

public class Edit(ApplicationDbContext context) : PageModel
{
    [BindProperty] public new required User User { get; set; } 

    public async Task<IActionResult> OnGetAsync(int id)
    {
        User = await context.Users.FindAsync(id);
        if (User == null)
        {
            return NotFound();
        }
        return Page();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        context.Attach(User).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}