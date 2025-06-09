using Application.Interfaces;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserRepository(IMongoCollection<MongoUser> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<List<User>> GetAllAsync()
        {
            List<MongoUser> mongoUsers = await _collection.Find(_ => true).ToListAsync();
            return _mapper.Map<List<User>>(mongoUsers);
        }

        public async Task<User> GetByIdAsync(string id)
        {
            MongoUser user = await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<User>(user);
        }

        public async Task CreateAsync(User user)
        {
            await _collection.InsertOneAsync(_mapper.Map<MongoUser>(user));
        }

        public async Task UpdateAsync(User user)
        {
            MongoUser mongoUser = _mapper.Map<MongoUser>(user);
            await _collection.ReplaceOneAsync(u => u.Id == mongoUser.Id, mongoUser);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(u => u.Id == id);
        }
    }
}
