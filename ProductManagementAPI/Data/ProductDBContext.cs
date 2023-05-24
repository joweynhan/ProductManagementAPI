using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Model;
using ProductManagementAPI.Models;


namespace ProductManagementAPI.Data
{
    public class ProductDBContext : IdentityDbContext<ApplicationUser> // to handle user authentication and authorization in addition to managing the database
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
            // establish a connection to the database
            string connectionString = @"Server = COLLABPH1001998; Database = ProductDB; Trusted_Connection = True; MultipleActiveResultSets = true; TrustServerCertificate = True";

            // log over here 
            _logger.LogInformation("Db Connection string: " + connectionString);

            optionsBuilder.UseSqlServer(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            base.OnConfiguring(optionsBuilder);
        }

        // represents the database table for the "Product" model
        public DbSet<Product> Products => Set<Product>();
    }
}

//for managing the connection and interaction with the database for the Product Management API