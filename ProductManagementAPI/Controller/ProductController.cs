using ProductManagementAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.DTO;
using System.Security.Claims;
using System.Data;
using System.Threading.Tasks;

namespace EMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        ProductDBContext _context;

        public ProductController(ProductDBContext ProductDB)
        {
            _context = ProductDB;
        }

        // Get All Product
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var result = await _context.Products.FromSqlRaw("EXEC SelectAllProduct").ToListAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductDTO addDTO)
        {
            //// Get the current UserId from the claims
            //var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if (UserId is null)
            //{
            //    return Unauthorized();
            //}

            var parameters = new[]
            {
                new SqlParameter("@UserId", addDTO. UserId),
                new SqlParameter("@Name", addDTO.Name),
                new SqlParameter("@Price", addDTO.Price)
            };

            var result = await _context.Database.ExecuteSqlRawAsync("EXEC AddProduct @UserId, @Name, @Price", parameters);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDTO updateDTO)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            product.Name = updateDTO.Name;
            product.Price = updateDTO.Price;

            var parameters = new[]
            {
                new SqlParameter("@UserId", updateDTO.UserId),
                new SqlParameter("@Id", id),
                new SqlParameter("@Name", updateDTO.Name),
                new SqlParameter("@Price", updateDTO.Price)
            };

            try
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC UpdateProduct @UserId, @Id, @Name, @Price", parameters);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update the product.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var parameters = new[]
            {
                 new SqlParameter("@Id", id)
            };

            try
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC DeleteProduct @Id", parameters);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete the product.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(); // Return 404 Not Found if the product is not found
            }

            return Ok(product); // Return the product if found
        }

    }
}
