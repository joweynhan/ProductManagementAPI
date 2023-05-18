using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Model;
using ProductManagementAPI.Models;


namespace ProductManagementAPI.Data
{
    public class ProductDBContext : IdentityDbContext<ApplicationUser>
    {
        public IConfiguration _appConfig { get; }
        public ILogger _logger { get; }

        public ProductDBContext(IConfiguration appConfig, ILogger<ProductDBContext> logger)
        {
            _appConfig = appConfig;
            _logger = logger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"Server = COLLABPH1001998; Database = ProductDB; Trusted_Connection = True; MultipleActiveResultSets = true; TrustServerCertificate = True";

            // log over here 
            _logger.LogInformation("Db Connection string: " + connectionString);

            optionsBuilder.UseSqlServer(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            base.OnConfiguring(optionsBuilder);
        }
       public DbSet<Product> Products => Set<Product>();
    }
}
