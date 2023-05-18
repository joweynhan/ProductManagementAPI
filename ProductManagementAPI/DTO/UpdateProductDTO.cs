namespace ProductManagementAPI.DTO
{
    public class UpdateProductDTO
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public double Price { get; set; }
    }
}
