using ProductManagementAPI.Data;
using ProductManagementAPI.Model;
using ProductManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagementAPI.Repository // repository for handling CRUD operations by using the stored procedure
{
    public class ProductDBRepository : IProductDBRepository
    {
        private readonly ProductDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductDBRepository(ProductDBContext context, UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<Product> GetAllProductsAsync()
        {
            var products = _context.Products
                .FromSqlRaw("EXECUTE GetAllProducts")
                .AsNoTracking()
                .ToList();
            return products;
        }

        public Product GetProductById(int id)
        {
            var productIdParam = new SqlParameter("@ProductId", id);

            var products = _context.Products
                .FromSqlRaw("EXECUTE GetProductById @ProductId", productIdParam)
                .AsEnumerable()
                .FirstOrDefault();

            return products;
        }

        public Product AddProduct(Product newProduct)
        {
            var nameParam = new SqlParameter("@Name", newProduct.Name);
            var priceParam = new SqlParameter("@Price", newProduct.Price);

            _context.Database.ExecuteSqlRaw("EXECUTE AddProduct @Name, @Price",
                nameParam, priceParam);

            return newProduct;
        }

       
        public Product UpdateProduct(int productId, Product updatedProduct)
        {
            var existingProduct = _context.Products.Find(productId);
            if (existingProduct != null)
            {
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;

                var productIdParam = new SqlParameter("@ProductId", productId);
                var nameParam = new SqlParameter("@Name", updatedProduct.Name);
                var priceParam = new SqlParameter("@Price", updatedProduct.Price);
                
                _context.Database.ExecuteSqlRaw("EXECUTE UpdateProduct @ProductId, @Name, @Price", productIdParam, nameParam, priceParam);

                _context.SaveChanges();
            }
            else
            {
                return null; 
            }
            return existingProduct;
        }

        public Product DeleteProduct(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                var productIdParam = new SqlParameter("@ProductId", productId);
                _context.Database.ExecuteSqlRaw("EXECUTE DeleteProduct @ProductId", productIdParam);
                _context.SaveChanges();
            }
            return product;
        }
    }
}
