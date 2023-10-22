using Microsoft.EntityFrameworkCore;

namespace Ticketer.WebApp.Extensions;

public static class DbExtensions
{
    public static async Task EnsureDbMigrationsAsync<T>(this IApplicationBuilder app) where T : DbContext
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<T>();
        await context.Database.EnsureCreatedAsync();
        await context.Database.MigrateAsync();
    }
}