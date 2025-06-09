using Application.DTOs;
using Domain.MongoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITaskListService
    {
        public Task<TaskListDTO?> GetByIdAsync(string senderId, string Id);
        public Task<List<TaskListDTO>> GetAllAsync(string senderId, int pageNumber, int pageSize);
        public Task CreateAsync(TaskListCreateDTO taskList);
        public Task UpdateAsync(TaskListDTO taskList);
        public Task DeleteAsync(string senderId, string taskListId);
        public Task<List<string>> GetTaskListRelations(string senderId, string taskListId);
        public Task AddTaskListRelation(TaskRelationDTO taskRelationDTO);
        public Task RemoveTaskListRelation(TaskRelationDTO taskRelationDTO);
    }
}
