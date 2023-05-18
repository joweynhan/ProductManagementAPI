using ProductManagementAPI.Model;

namespace ProductManagementAPI.Repository
{
    public interface IProductDBRepository
    {
        IEnumerable<Product> GetAllProductsAsync();
        Product GetProductById(int id);
        Product AddProduct(Product newProduct);
        Product UpdateProduct(int productId, Product updatedProduct);
        Product DeleteProduct(int productId);

        // IEnumerable<string> GetApplicationUserIds();
    }
}
