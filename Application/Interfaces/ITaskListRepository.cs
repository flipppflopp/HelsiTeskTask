using Domain.Entities;
using Domain.MongoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITaskListRepository
    {
        public Task<TaskList?> GetByIdAsync(string senderId, string Id);
        public Task<List<TaskList>> GetAllAsync(string senderId, int pageNumber, int pageSize);
        public Task CreateAsync(TaskList taskList);
        public Task<TaskList> UpdateAsync(string senderId, TaskList taskList);
        public Task<TaskList> DeleteAsync(string senderId, string taskListId);
        public Task<List<string>> GetTaskListRelations(string senderId, string taskListId);
        public Task<TaskList> AddTaskListRelation(string senderId, string taskListId, string accessedUserId);
        public Task<TaskList> RemoveTaskListRelation(string senderId, string taskListId, string accessedUserId);
    }
}
