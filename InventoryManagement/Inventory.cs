using Simple_Inventory_Management_System.ProductManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Inventory_Management_System.InventoryManagement
{
    public class Inventory
    {
        public List<Product> Proudcts { get; }

        public Inventory()
        {
            Proudcts = new List<Product>();
        }


    }
}
