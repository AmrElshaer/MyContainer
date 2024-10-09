using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyContainer.Data;

namespace MyContainer.Pages.Containers;

public class Index(ApplicationDbContext context): PageModel
{
    public IList<Container> Containers { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Containers = await context.Containers.AsNoTracking()
            .Include(c => c.User)
            .OrderByDescending(c=>c.CreatedAt).ToListAsync();
    }
}