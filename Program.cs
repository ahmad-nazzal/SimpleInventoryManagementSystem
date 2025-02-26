
using Simple_Inventory_Management_System;
using Simple_Inventory_Management_System.InventoryManagement;

IInventory inventory = new Inventory();
Utilities utilities = new Utilities(inventory);
utilities.Run();