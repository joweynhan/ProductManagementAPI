using System.ComponentModel.DataAnnotations;

namespace ProductManagementAPI.DTO
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public ProductDTO()
        {
        }

        public ProductDTO(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}
