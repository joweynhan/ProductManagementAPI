using Microsoft.EntityFrameworkCore;

namespace ProductManagementAPI.Data
{
    public static class AutoMigration // no need to update-database everytime making a stored procedure
    {
        public static void Automigrate(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<ProductDBContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
        }
    }
}
