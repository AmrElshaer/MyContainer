using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyContainer.Data;

namespace MyContainer.Pages.Users;

public class Create(ApplicationDbContext context) : PageModel
{
    private readonly ApplicationDbContext _context = context;

    [BindProperty] public User User { get; set; }
    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Users.Add(User);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}