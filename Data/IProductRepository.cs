using Simple_Inventory_Management_System.ProductManagement;

namespace Simple_Inventory_Management_System.InventoryManagement
{
    public interface IProductRepository
    {
        void Add(Product product);
        List<Product> GetAll();
        bool Delete(string name);
        bool Update(string name, Product updatedProduct);
        Product? Find(string name);
    }
}