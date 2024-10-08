using Microsoft.EntityFrameworkCore;
using MyContainer.Data;

namespace MyContainer.Extensions;

public static class InitializerExtensions
{
    public static IApplicationBuilder InitialiseDatabase(this IApplicationBuilder app)
    {
        MigrateDatabaseAsync<ApplicationDbContext>(app.ApplicationServices).GetAwaiter().GetResult();
        return app;
    }

    private static async Task MigrateDatabaseAsync<TContext>(IServiceProvider serviceProvider)
        where TContext : DbContext
    {
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        await context.Database.MigrateAsync();
    }

    
}

