using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyContainer.Data;

namespace MyContainer.Pages.Users;

public class Index(ApplicationDbContext context) : PageModel
{
    public required IList<User> Users { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Users = await context.Users.ToListAsync();
        return Page();
    }
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) return RedirectToPage();
        context.Users.Remove(user);
        await context.SaveChangesAsync();

        return RedirectToPage();
    }
}