using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.MongoModels;
using MongoDB.Driver.Core.Servers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Constants;

namespace Application.Services
{
    public class TaskListService : ITaskListService
    {
        private readonly ITaskListRepository _repo;
        private readonly IMapper _mapper;

        public TaskListService(ITaskListRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<TaskListDTO?> GetByIdAsync(string senderId, string Id)
        {
            TaskList? taskList = await _repo.GetByIdAsync(senderId, Id);

            if (taskList == null)
            {
                throw new UnauthorizedAccessException(ErrorMessages.NotOwnerOrAccessedUser);
            }
            else
            {
                return _mapper.Map<TaskListDTO?>(taskList);
            }
        }

        public async Task<List<TaskListDTO>> GetAllAsync(string senderId, int pageNumber, int pageSize)
        {
            return _mapper.Map<List<TaskListDTO>>(await _repo.GetAllAsync(senderId, pageNumber, pageSize));
        }

        public async Task CreateAsync(TaskListCreateDTO taskListDTO)
        {
            taskListDTO.OwnerUserId = taskListDTO.SenderId;
            await _repo.CreateAsync(_mapper.Map<TaskList>(taskListDTO));
        }

        public async Task UpdateAsync(TaskListDTO taskListDTO)
        {
            TaskList taskList = await _repo.UpdateAsync(taskListDTO.SenderId, _mapper.Map<TaskList>(taskListDTO));
            
            if(taskList == null)
            {
                throw new UnauthorizedAccessException(ErrorMessages.NotOwnerOrAccessedUser);
            }   
        }

        public async Task DeleteAsync(string senderId, string taskListId)
        {
            TaskList taskList = await _repo.DeleteAsync(senderId, taskListId);

            if (taskList == null)
            {
                throw new UnauthorizedAccessException(ErrorMessages.NotOwner);
            }
        }

        public async Task<List<string>> GetTaskListRelations(string senderId, string taskListId)
        {
            return await _repo.GetTaskListRelations(senderId, taskListId);
        }

        public async Task AddTaskListRelation(TaskRelationDTO taskRelationDTO)
        {
            TaskList taskList = 
                await _repo.AddTaskListRelation(taskRelationDTO.SenderId, taskRelationDTO.TaskListId, taskRelationDTO.AccessedUserId);

            if (taskList == null)
            {
                throw new UnauthorizedAccessException(ErrorMessages.NotOwnerOrAccessedUser);
            }
        }

        public async Task RemoveTaskListRelation(TaskRelationDTO taskRelationDTO)
        {
            TaskList taskList = 
                await _repo.RemoveTaskListRelation(taskRelationDTO.SenderId, taskRelationDTO.TaskListId, taskRelationDTO.AccessedUserId);

            if (taskList == null)
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
