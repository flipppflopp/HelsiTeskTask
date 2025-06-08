using Domain.Entities;
using Domain.MongoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<MongoUser>> GetAllAsync();
        public Task<MongoUser?> GetByIdAsync(string id);
        public Task CreateAsync(MongoUser user);
        public Task UpdateAsync(MongoUser user);
        public Task DeleteAsync(MongoUser user);
    }
}
