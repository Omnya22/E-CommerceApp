using AspNetCore.Identity.MongoDbCore.Models;
using E_Commerce.Api.Configuration;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Commerce.Api.DataAccess
{
    public class AppDbContext:MongoIdentityUser
    {
        private IMongoDatabase Database { get; set; }
        private readonly List<Func<Task>> _commands;

        public AppDbContext(IConfiguration Configuration)
        {
            // Set Guid to CSharp style (with dash -)
            BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;


            // Configure mongo 
            var mongoDbSettings = Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();
            var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);

            Database = mongoClient.GetDatabase(mongoDbSettings.Name);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }

    }
}
