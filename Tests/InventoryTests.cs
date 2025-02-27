using Simple_Inventory_Management_System.InventoryManagement;
using Simple_Inventory_Management_System.ProductManagement;

namespace Simple_Inventory_Management_System_test
{
    public class InventoryTests
    {
        [Fact]
        public void AddProduct_Success()
        {
            var inventory = new Inventory();
            string productName = "Product 1";
            double price = 10;
            int quantity = 1;

            inventory.AddProduct(productName, price, quantity);

            var products = inventory.GetAllProducts();
            Assert.Equal(productName, products[products.Count-1].Name);
            Assert.Equal(price, products[products.Count - 1].Price);
            Assert.Equal(quantity, products[products.Count - 1].Quantity);

        }

        [Fact]
        public void AddProduct_Fail_NegativePrice()
        {
            var inventory = new Inventory();
            Assert.Throws<ArgumentException>(() => inventory.AddProduct("Invalid Product", -5, 1));
        }

        [Fact]
        public void AddProduct_Fail_NegativeQuantity()
        {
            var inventory = new Inventory();
            Assert.Throws<ArgumentException>(() => inventory.AddProduct("Invalid Product", 10, -2));
        }

        [Fact]
        public void DeleteProduct_Success()
        {
            var inventory = new Inventory();
            string existingProduct = "Apple";

            bool result = inventory.DeleteProduct(existingProduct);

            Assert.True(result);
            Assert.Null(inventory.FindProduct(existingProduct));
        }

        [Fact]
        public void DeleteProduct_Fail_ProductNotFound()
        {
            var inventory = new Inventory();
            bool result = inventory.DeleteProduct("NonExistentProduct");

            Assert.False(result);
        }

        [Fact]
        public void FindProduct_Success()
        {
            var inventory = new Inventory();
            var product = inventory.FindProduct("Apple");

            Assert.NotNull(product);
            Assert.Equal("Apple", product.Name);
        }

        [Fact]
        public void FindProduct_Fail_ProductNotFound()
        {
            var inventory = new Inventory();
            var product = inventory.FindProduct("NonExistentProduct");

            Assert.Null(product);
        }

    }
}