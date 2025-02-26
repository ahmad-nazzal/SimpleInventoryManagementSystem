using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Inventory_Management_System.ProductManagement
{
    public class Product
    {
        private double _price;
        private int _quantity;
        public string Name { get; set; }
        public double Price
        {
            get => _price;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price cannot be negative");
                }
                _price = value;
            }
        }
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Quantity cannot be negative");
                }
                _quantity = value;
            }
        }

        public Product(string name, double price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"{Name}\t\t{Price}\t\t{Quantity}";
        }

    }
}
