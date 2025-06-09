using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.MongoModels;
using AutoMapper;
using Application.DTOs;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            return _mapper.Map<List<UserDTO>>(await _repo.GetAllAsync());
        }

        public async Task<UserDTO?> GetByIdAsync(string id)
        {

            return _mapper.Map<UserDTO?>(await _repo.GetByIdAsync(id));
        }

        public async Task CreateAsync(UserCreateDTO userDTO)
        {
            await _repo.CreateAsync(_mapper.Map<User>(userDTO));
        }

        public async Task UpdateAsync(UserDTO userDTO)
        {
            await _repo.UpdateAsync(_mapper.Map<User>(userDTO));
        }

        public async Task DeleteAsync(string id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
