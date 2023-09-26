using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieManager.Data.Contexts;

namespace MovieManager.Data.Extensions
{
    public static class MigrationExtension
    {
        public static void MigrateInitializer(this IApplicationBuilder app)
        {
            try
            {
                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    var serviceDb = serviceScope.ServiceProvider.GetService<AppDbContext>();

                    serviceDb.Database.Migrate();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
