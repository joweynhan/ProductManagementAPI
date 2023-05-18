namespace ProductManagementAPI.Model
{
    public class Product
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public double Price { get; set; }

    }
}
