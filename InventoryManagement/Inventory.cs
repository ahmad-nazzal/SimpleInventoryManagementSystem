﻿using Simple_Inventory_Management_System.ProductManagement;
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
            if (price < 0) throw new ArgumentException("Price cannot be negative");
            if (quantity < 0) throw new ArgumentException("Quantity cannot be negative");

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

        public bool EditProduct(string productName, Product newProduct)
        {
            var existingProduct = Products.FirstOrDefault(p => string.Equals(p.Name, productName, StringComparison.OrdinalIgnoreCase));
            if (existingProduct == null)
            {
                return false;
            }
            existingProduct.Name = string.IsNullOrWhiteSpace(newProduct.Name) ? existingProduct.Name : newProduct.Name;
            existingProduct.Price = newProduct.Price < 0 ? existingProduct.Price : newProduct.Price;
            existingProduct.Quantity = newProduct.Quantity < 0 ? existingProduct.Quantity : newProduct.Quantity;

            return true;
        }
        public List<Product> GetAllProducts()
        {
            return Products;
        }

        public Product? FindProduct(string productName)
        {
            return Products.FirstOrDefault(p =>
                string.Equals(p.Name, productName, StringComparison.OrdinalIgnoreCase));
        }





    }
}
