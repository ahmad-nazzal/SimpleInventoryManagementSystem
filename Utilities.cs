using Simple_Inventory_Management_System.InventoryManagement;
using Simple_Inventory_Management_System.ProductManagement;
using System;
using System.Collections.Generic;
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

                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }
            while (mainMenuOptions != MainMenuOptions.Close);
        }

        private void SearchProduct()
        {
            throw new NotImplementedException();
        }

        private void EditProduct()
        {
            throw new NotImplementedException();
        }

        private void ShowAllProductsOverview()
        {
            throw new NotImplementedException();
        }

        private void AddProduct()
        {
            Console.Clear();
            Console.WriteLine("************************");
            Console.WriteLine("* Add new product *");
            Console.WriteLine("************************");
            Console.Write("Enter product name: ");
            string name = Console.ReadLine() ?? "0";
            Console.Write("Enter product price: ");
            double price = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Enter product quantity: ");
            int quantity = int.Parse(Console.ReadLine() ?? "0");
            _inventory.AddProduct(name, price, quantity);
            Console.WriteLine("Product added successfully.");
            Console.ReadLine();

        }


    }

}
