using Application.DTOs;
using Domain.Entities;
using Domain.MongoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserDTO>> GetAllAsync();
        public Task<UserDTO?> GetByIdAsync(string id);
        public Task CreateAsync(UserCreateDTO user);
        public Task UpdateAsync(UserDTO userDTO);
        public Task DeleteAsync(string id);
    }
}
