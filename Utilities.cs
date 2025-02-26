using Simple_Inventory_Management_System.InventoryManagement;
using Simple_Inventory_Management_System.ProductManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Inventory_Management_System
{

    internal class Utilities
    {
        private IInventory _inventory;

        private enum MainMenuOptions
        {
            AddProduct = 1,
            ViewAllProducts = 2,
            EditProduct = 3,
            SearchProduct = 4,
            DeleteProduct = 5,
            Close = 0
        }

        public Utilities(IInventory inventory)
        {
            _inventory = inventory;

        }

        public void Run()
        {
            ShowInventoryManagementMenu();
        }


        private void ShowInventoryManagementMenu()
        {
            int? userSelection;
            MainMenuOptions mainMenuOptions;

            do
            {
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("************************");
                Console.WriteLine("* Inventory management *");
                Console.WriteLine("************************");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("What do you want to do?");
                Console.ResetColor();

                Console.WriteLine("1: Add new product");
                Console.WriteLine("2: View all products");
                Console.WriteLine("3: Edit a product");
                Console.WriteLine("4: Search for a product");
                Console.WriteLine("0: Close");

                Console.Write("Your selection: ");

                try
                {
                    userSelection = int.Parse(Console.ReadLine() ?? "0");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    userSelection = -1;
                }

                mainMenuOptions = (MainMenuOptions)userSelection;
                switch (mainMenuOptions)
                {
                    case MainMenuOptions.AddProduct:
                        AddProduct();
                        break;

                    case MainMenuOptions.ViewAllProducts:
                        ShowAllProductsOverview();
                        break;

                    case MainMenuOptions.EditProduct:
                        EditProduct();
                        break;

                    case MainMenuOptions.SearchProduct:
                        SearchProduct();
                        break;

                    case MainMenuOptions.DeleteProduct:
                        DeleteProduct();
                        break;
                    
                    case MainMenuOptions.Close:
                        Console.WriteLine("Closing application...");
                        break;

                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
                Console.WriteLine("Press Enter to continue.");
                Console.ReadLine();
            }
            while (mainMenuOptions != MainMenuOptions.Close);
        }

        private void SearchProduct()
        {
            Console.Clear();
            Console.WriteLine("************************");
            Console.WriteLine("* Search Product *");
            Console.WriteLine("************************");

            Console.WriteLine(
                "Enter the name of the product you want to search for: ");
            string? productName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(productName))
            {
                Console.WriteLine("Invalid input. Please enter a valid product name.");
                return;
            }

            Product? product = _inventory.FindProduct(productName);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
            }
            else
            {
                Console.WriteLine(
                    $"Name: {product.Name}\nPrice: {product.Price}\nQuantity: {product.Quantity}");
            }
        }

        private void EditProduct()
        {
            Console.Clear();
            Console.WriteLine("************************");
            Console.WriteLine("* Edit Product *");
            Console.WriteLine("************************");

            Console.Write("Enter the name of the product you want to edit (Press Enter to cancel): ");
            string? name = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Edit canceled. Product name cannot be empty.");
                return;
            }

            var existingProduct = _inventory.GetAllProducts().FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
            if (existingProduct == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.Write("New product name (Press Enter to keep old value): ");
            string? newNameString = Console.ReadLine()?.Trim();
            string newName = string.IsNullOrWhiteSpace(newNameString) ? existingProduct.Name : newNameString;


            Console.Write("New product price (Press Enter to keep old value): ");
            string? newPriceString = Console.ReadLine()?.Trim();
            double newPrice;
            if (!double.TryParse(newPriceString, out newPrice))
            {
                newPrice = existingProduct.Price;
            }

            Console.WriteLine(
                "New product quantity: ");
            string newQuantityString = Console.ReadLine()?.Trim() ?? "";
            int newQuantity = string.IsNullOrWhiteSpace(newQuantityString) ? -1 : int.Parse(newQuantityString);
            Console.WriteLine(
                "Are you sure you want to edit this product? (Y/N)");
            string confirm = Console.ReadLine()?.Trim().ToLower() ?? "";
            if (confirm == "y")
            {
                if (_inventory.EditProduct(name, new Product(newName, newPrice, newQuantity)))
                {
                    Console.WriteLine("Product edited successfully.");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
            else
            {
                Console.WriteLine("Edit canceled.");
            }
            Console.WriteLine(
                "Press Enter to return to the main menu.");
            Console.ReadLine();

        }

        private void ShowAllProductsOverview()
        {
            Console.Clear();
            Console.WriteLine("************************");
            Console.WriteLine("* All products *");
            Console.WriteLine("************************");
            Console.WriteLine(
                "Name\t\tPrice\t\tQuantity");
            foreach (var product in _inventory.GetAllProducts())
            {
                Console.WriteLine(product);
            }
        }

        private void AddProduct()
        {
            Console.Clear();
            Console.WriteLine("************************");
            Console.WriteLine("* Add new product *");
            Console.WriteLine("************************");

            Console.Write("Enter product name: ");
            string name = Console.ReadLine() ?? "Default";

            Console.Write("Enter product price: ");
            double price;
            while (!double.TryParse(Console.ReadLine(), out price) || price < 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid positive number.");
                Console.Write("Enter the price: ");
            }

            Console.Write("Enter product quantity: ");
            int quantity;
            while (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid positive number.");
                Console.Write("Enter the quantity: ");
            }

            _inventory.AddProduct(name, price, quantity);
            Console.WriteLine("Product added successfully.");

        }
        private void DeleteProduct()
        {
            Console.Clear();
            Console.WriteLine("************************");
            Console.WriteLine("* Delete Product *");
            Console.WriteLine("************************");

            Console.Write("Enter product name: ");
            string name = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid product name. Please try again.");
                return;
            }


            Console.Write($"Are you sure you want to delete \"{name}\"? (Y/N): ");
            string? confirm = Console.ReadLine()?.Trim().ToLower();

            if (confirm == "y")
            {
                if (_inventory.DeleteProduct(name))
                {
                    Console.WriteLine("Product deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
            else
            {
                Console.WriteLine("Deletion canceled.");
            }
        }


    }

}
