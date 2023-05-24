using AutoMapper;
using ProductManagementAPI.Data;
using ProductManagementAPI.DTO;
using ProductManagementAPI.Model;
using ProductManagementAPI.Models;
using ProductManagementAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagementAPI.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductDBRepository _repo;
        private readonly IMapper _mapper;
        private readonly ProductDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductController(IProductDBRepository repo, IMapper mapper,
            ProductDBContext context, UserManager<ApplicationUser> userManager)
        {
            _repo = repo;
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("Products")] // attritube
        public IActionResult GetAll() // action method 
        {
            var products = _repo.GetAllProductsAsync(); // method in the reposiitory
            return Ok(products);
        }


        [Authorize]
        [HttpPost("AddProduct")]
        public IActionResult AddProduct(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                // map to the Product entity
                var newProduct = new Product
                {
                    Name = productDTO.Name,
                    Price = productDTO.Price
                };

                var addedProduct = _repo.AddProduct(newProduct);
                return Ok(addedProduct);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpGet("GetProduct/{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _repo.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [Authorize]
        [HttpDelete("DeleteProduct/{id}")]
        public IActionResult Delete(int id)
        {
            var productToDelete = _repo.GetProductById(id);
            if (productToDelete == null)
            {
                return NotFound("Product is not found!");
            }

            var deletedProduct = _repo.DeleteProduct(id);
            return Ok(_mapper.Map<ProductDTO>(deletedProduct));
        }

        [Authorize]
        [HttpPut("UpdateProduct/{productId}")]
        public IActionResult UpdateProduct(int productId, Product updatedProduct)
        {
            var existingProduct = _repo.UpdateProduct(productId, updatedProduct);
            if (existingProduct != null)
            {
                return Ok(existingProduct);
            }

            else
            {
                var errorMessage = $"Product with ID {productId} not found.";
                return NotFound(errorMessage);
            }
        }
    }
}
