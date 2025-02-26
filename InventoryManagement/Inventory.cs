using Simple_Inventory_Management_System.ProductManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Inventory_Management_System.InventoryManagement
{
    public class Inventory: IInventory
    {
        public List<Product> Products { get; }

        public Inventory()
        {
            Products = new List<Product>() { new Product("Apple", 1.0, 10), new Product("Banana", 0.5, 20) };
        }

        public void AddProduct(string name, double price, int quantity)
        {
            Products.Add(new Product(name, price, quantity));
        }
        public bool DeleteProduct(string name)
        {
            var product = Products.FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));

            if (product == null)
            {
                return false;
            }

            Products.Remove(product);
            return true;
        }





    }
}
