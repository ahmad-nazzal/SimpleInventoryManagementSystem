using DotNetEnv;
using MongoDB.Driver;
using Simple_Inventory_Management_System.InventoryManagement;
using Simple_Inventory_Management_System.ProductManagement;

namespace Simple_Inventory_Management_System.Data
{
    public class MongoProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public MongoProductRepository()
        {
            Env.Load();
            var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI")!;
            var dbName = Environment.GetEnvironmentVariable("MONGODB_DB")!;
            var collectionName = Environment.GetEnvironmentVariable("MONGODB_COLLECTION")!;

            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase(dbName);
            _products = database.GetCollection<Product>(collectionName);
        }

        public void Add(Product product)
        {
            _products.InsertOne(product);
        }

        public bool Delete(string name)
        {
            var result = _products.DeleteOne(p => p.Name.ToLower() == name.ToLower());
            return result.DeletedCount > 0;
        }

        public bool Update(string productName, Product updated)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Name, productName);
            var update = Builders<Product>.Update
                .Set(p => p.Name, updated.Name)
                .Set(p => p.Price, updated.Price)
                .Set(p => p.Quantity, updated.Quantity);

            var result = _products.UpdateOne(filter, update);
            return result.ModifiedCount > 0;
        }

        public Product? Find(string name)
        {
            return _products.Find(p => p.Name.ToLower() == name.ToLower()).FirstOrDefault();
        }

        public List<Product> GetAll()
        {
            return _products.Find(_ => true).ToList();
        }
    }
}
