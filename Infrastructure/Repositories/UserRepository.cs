using Application.Interfaces;
using Domain;
using Domain.Entities;
using Domain.MongoModels;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<MongoUser> _collection;

        public UserRepository(IOptions<MongoDbSettings> options)
        {
            var settings = options.Value;
            var client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.Database);
            _collection = db.GetCollection<MongoUser>("Users");
        }

        public async Task<List<MongoUser>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<MongoUser?> GetByIdAsync(string id) =>
            await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(MongoUser user) =>
            await _collection.InsertOneAsync(user);

        public async Task UpdateAsync(MongoUser user) =>
            await _collection.ReplaceOneAsync(u => u.Id == user.Id, user);

        public async Task DeleteAsync(MongoUser user) =>
            await _collection.DeleteOneAsync(u => u.Id == user.Id || u.Name == user.Name);
    }
}
