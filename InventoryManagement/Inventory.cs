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
        private readonly IProductRepository _productRepository;

        public Inventory(IProductRepository _productRepository)
        {
            this._productRepository = _productRepository;
        }

        public void AddProduct(string name, double price, int quantity)
        {
            if (price < 0) throw new ArgumentException("Price cannot be negative");
            if (quantity < 0) throw new ArgumentException("Quantity cannot be negative");

            _productRepository.Add(new Product(name, price, quantity));
        }
        public bool DeleteProduct(string name)
        {
            return _productRepository.Delete(name);
        }

        public bool EditProduct(string productName, Product newProduct)
        {
            return _productRepository.Update(productName, newProduct);
        }
        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }

        public Product? FindProduct(string productName)
        {
            return _productRepository.Find(productName);

        }





    }
}
