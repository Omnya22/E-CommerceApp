using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System.Collections.Generic;

namespace E_Commerce.Api.Models
{
    [CollectionName("Orders")]

    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public bool IsWaited { get; set; }

        public bool IsAccepted { get; set; }

        public bool IsRejected { get; set; }

        public IList<Product> Products { get; set; }

    }
}
