using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyContainer.Data;
using MyContainer.Enums;

namespace MyContainer.Pages.Containers;

public class Upsert(ApplicationDbContext context) : PageModel
{
    [BindProperty] public Container Container { get; set; } = default!;
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
        // Validate Remaining amount
        if (Container.Remaining < 0)
        {
            return Page();
        }

        // Handle creation or update of the container
        if (Container.Id == 0)
        {
            await CreateContainerAsync();
        }
        else
        {
            await UpdateContainerAsync();
        }

        // Save changes to the database
        await context.SaveChangesAsync();
        return RedirectToPage("Index");
    }

    private async Task CreateContainerAsync()
    {
     
            var transaction = CreateTransaction(Container.UserId, Container.TotalPrice, Container.Arrive);
            Container.Transaction=transaction;
       
        await context.Containers.AddAsync(Container);
    }

    private async Task UpdateContainerAsync()
    {
        var oldContainer = await context.Containers.Include(c => c.Transaction)
            .FirstOrDefaultAsync(c => c.Id == Container.Id);

        ArgumentNullException.ThrowIfNull(oldContainer);
        if (oldContainer.Transaction!= null)
        {
            oldContainer.Transaction.UserId=Container.UserId;
            oldContainer.Transaction.CreditAmount=Container.TotalPrice;
            oldContainer.Transaction.DebitAmount=Container.Arrive;
        }
        UpdateContainerProperties(oldContainer);
    }

    private Transaction CreateTransaction(int userId, decimal creditAmount, decimal debitAmount)
    {
        return new Transaction
        {
            UserId = userId,
            CreatedAt = DateTime.Now,
            TransactionType = TransactionType.Container,
            CreditAmount = creditAmount,
            DebitAmount = debitAmount,
        };
    }

    private void UpdateTransactionUserId(IEnumerable<Transaction> transactions, int newUserId)
    {
        foreach (var transaction in transactions)
        {
            transaction.UserId = newUserId;
        }
    }

    private void UpdateContainerProperties(Container oldContainer)
    {
        oldContainer.UserId = Container.UserId;
        oldContainer.ContainerType = Container.ContainerType;
        oldContainer.ContainerPrice = Container.ContainerPrice;
        oldContainer.Number = Container.Number;
        oldContainer.LoadingPrice = Container.LoadingPrice;
        oldContainer.LoadingTime = Container.LoadingTime;
        oldContainer.NumberOfWoodenBoards = Container.NumberOfWoodenBoards;
        oldContainer.PriceOfWoodenBoard = Container.PriceOfWoodenBoard;
        oldContainer.NumberOfBoxes = Container.NumberOfBoxes;
        oldContainer.BoxPrice = Container.BoxPrice;
        oldContainer.NumberOfStretch = Container.NumberOfStretch;
        oldContainer.PriceOfStretch = Container.PriceOfStretch;
        oldContainer.TotalPrice = Container.TotalPrice;
        oldContainer.Remaining = Container.Remaining;
        oldContainer.Arrive = Container.Arrive;
    }
}