namespace ProductManagementAPI.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = default!;
        public double Price { get; set; }
    }
}