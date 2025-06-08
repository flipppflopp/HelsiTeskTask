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

        private UserDTO MapFromMongoToDto(MongoUser mongoUser)
        {
            User user = _mapper.Map<User>(mongoUser);
            return _mapper.Map<UserDTO>(user);
        }

        private List<UserDTO> MapFromMongoToDtoList(List<MongoUser> mongoUsers)
        {
            List<User> users = _mapper.Map<List<User>>(mongoUsers);
            return _mapper.Map<List<UserDTO>>(users);
        }

        private MongoUser MapFromDtoToMongo(UserDTO userDto)
        {
            User user = _mapper.Map<User>(userDto);
            return _mapper.Map<MongoUser>(user);
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            return MapFromMongoToDtoList(await _repo.GetAllAsync());
        }

        public async Task<UserDTO?> GetByIdAsync(string id)
        {

            return MapFromMongoToDto(await _repo.GetByIdAsync(id));
        }

        public async Task CreateAsync(UserCreateDTO userDTO)
        {
            User user = _mapper.Map<User>(userDTO);
            MongoUser mongoUser = _mapper.Map<MongoUser>(user);

            await _repo.CreateAsync(mongoUser);
        }

        public async Task UpdateAsync(UserDTO userDTO)
        {
            await _repo.UpdateAsync(MapFromDtoToMongo(userDTO));
        }

        public async Task DeleteAsync(UserDTO userDTO)
        {
            await _repo.DeleteAsync(MapFromDtoToMongo(userDTO));
        }
    }
}
