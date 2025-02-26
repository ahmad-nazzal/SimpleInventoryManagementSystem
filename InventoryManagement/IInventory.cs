using Simple_Inventory_Management_System.ProductManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Inventory_Management_System.InventoryManagement
{
    public interface IInventory
    {
        public void AddProduct(string name, double price, int quantity);
        public bool DeleteProduct(string name);
        public bool EditProduct(string productName, Product newProduct);
        public void FindProduct(string productName);
        public List<Product> GetAllProducts();
    }
}
