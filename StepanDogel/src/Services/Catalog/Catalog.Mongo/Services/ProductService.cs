using Catalog.Mongo.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Mongo.Services
{
    public class ProductService
    {
        IMongoCollection<Product> Products;
        public ProductService()
        {
            string connectionString = "mongodb://localhost:27017/store";
            var connection = new MongoUrlBuilder(connectionString);
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(connection.DatabaseName);
            Products = database.GetCollection<Product>("Products");
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await Products.AsQueryable().ToListAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await Products.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Product p)
        {
            await Products.InsertOneAsync(p);
        }

        public async Task UpdateAsync(Product p)
        {
            await Products.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(p.Id)), p);
        }

        public async Task DeleteAsync(string id)
        {
            await Products.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }
    }
}
