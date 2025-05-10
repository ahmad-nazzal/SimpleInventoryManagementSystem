using Microsoft.Data.SqlClient;
using Simple_Inventory_Management_System.InventoryManagement;
using Simple_Inventory_Management_System.ProductManagement;

namespace Simple_Inventory_Management_System.Data
{
    public class SqlProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public SqlProductRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("INVENTORY_DB_CONNECTION")
                ?? throw new InvalidOperationException("Database connection string is not set.");
            Console.WriteLine(_connectionString);
        }

        public void Add(Product product)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand("INSERT INTO Products (Name, Price, Quantity) VALUES (@Name, @Price, @Quantity)", conn);
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
            cmd.ExecuteNonQuery();
        }

        public List<Product> GetAll()
        {
            var products = new List<Product>();
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand("SELECT * FROM Products", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product
                (
                    reader["Name"].ToString()!,
                    Convert.ToDouble(reader["Price"]),
                    Convert.ToInt32(reader["Quantity"])
                ));
            }
            return products;
        }

        public bool Delete(string name)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand("DELETE FROM Products WHERE Name = @Name", conn);
            cmd.Parameters.AddWithValue("@Name", name);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Update(string name, Product updatedProduct)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand("UPDATE Products SET Name = @NewName, Price = @Price, Quantity = @Quantity WHERE Name = @Name", conn);
            cmd.Parameters.AddWithValue("@NewName", updatedProduct.Name);
            cmd.Parameters.AddWithValue("@Price", updatedProduct.Price);
            cmd.Parameters.AddWithValue("@Quantity", updatedProduct.Quantity);
            cmd.Parameters.AddWithValue("@Name", name);
            return cmd.ExecuteNonQuery() > 0;
        }

        public Product? Find(string name)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand("SELECT * FROM Products WHERE Name = @Name", conn);
            cmd.Parameters.AddWithValue("@Name", name);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Product
                (
                    reader["Name"].ToString()!,
                    Convert.ToDouble(reader["Price"]),
                    Convert.ToInt32(reader["Quantity"])
                );
            }
            return null;
        }
    }
}
