using AdsAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace AdsAPI.Data
{
    public static class DataInitializer
    {
        public static void InitializeDatabase(IHost app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<AdsDbContext>();

                context.Database.Migrate();

                if (!context.Ads.Any())
                {
                    context.Ads.AddRange(
                        new Ad
                        {
                            Title = "Begagnad cykel",
                            Description = "26-tums herrcykel i fint skick.",
                            Price = 1200
                        },
                        new Ad
                        {
                            Title = "Soffa säljes",
                            Description = "Grå 3-sits soffa från IKEA.",
                            Price = 1500
                        }
                    );

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Fel vid initiering av databasen", ex);
            }
        }
    }
}
