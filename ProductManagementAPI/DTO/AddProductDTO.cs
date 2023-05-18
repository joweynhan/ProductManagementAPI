namespace ProductManagementAPI.DTO
{
    public class AddProductDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; } = default!;
        public double Price { get; set; }
    }
}
