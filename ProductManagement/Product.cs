using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Simple_Inventory_Management_System.ProductManagement
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
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
