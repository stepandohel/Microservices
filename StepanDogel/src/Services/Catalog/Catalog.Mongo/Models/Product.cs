using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Mongo.Models
{
    public class Product
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }

    }
}