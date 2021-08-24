using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Api.Models
{
    [CollectionName("Products")]
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [MaxLength(50)]
        [Display(Name = "Product Title")]
        public string Name { get; set; }

        [MaxLength(200)]
        [Display(Name = "Product Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Product Price")]
        public decimal Price { get; set; }

        [Display(Name = "Product Photo")]
        public string PhotoUrl { get; set; }

        public IList<Order> Orders { get; set; }

    }
}
