
using Simple_Inventory_Management_System;
using Simple_Inventory_Management_System.Data;
using Simple_Inventory_Management_System.InventoryManagement;
using DotNetEnv;

Env.Load();

IProductRepository productRepository = new MongoProductRepository();
IInventory inventory = new Inventory(productRepository);
Utilities utilities = new Utilities(inventory);
utilities.Run();