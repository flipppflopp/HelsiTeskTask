using Application.Interfaces;
using Domain.MongoModels;
using Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using static MongoDB.Driver.WriteConcern;
using System.Diagnostics;
using AutoMapper;

namespace Infrastructure.Repositories
{
    public class TaskListRepository : ITaskListRepository
    {
        private readonly IMongoCollection<MongoTaskList> _collection;
        private readonly IMapper _mapper;

        public TaskListRepository(IMongoCollection<MongoTaskList> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<TaskList?> GetByIdAsync(string senderId, string Id)
        {
            MongoTaskList mongoTaskList = await _collection.Find(tl =>
                (tl.OwnerUserId == senderId || tl.AllowedUserIds.Contains(senderId))
                && tl.Id == Id
            ).FirstOrDefaultAsync();

            return _mapper.Map<TaskList?>(mongoTaskList);
        }

        public async Task<List<TaskList>> GetAllAsync(string senderId, int pageNumber, int pageSize)
        {
            List<MongoTaskList> mongoTaskLists = await _collection.Find(tl =>
                    tl.OwnerUserId == senderId || tl.AllowedUserIds.Contains(senderId))
                 .SortByDescending(t => t.CreatedAt)
                 .Skip((pageNumber - 1) * pageSize)
                 .Limit(pageSize)
                 .ToListAsync();

            return _mapper.Map<List<TaskList>>(mongoTaskLists);
        }

        public async Task CreateAsync(TaskList taskList)
        {
            taskList.CreatedAt = DateTime.Now;
            await _collection.InsertOneAsync(_mapper.Map<MongoTaskList>(taskList));
        }

        public async Task<TaskList> UpdateAsync(string senderId, TaskList taskList)
        {
            FilterDefinition<MongoTaskList> filter = 
                Builders<MongoTaskList>.Filter.Where(tl =>
                (tl.OwnerUserId == senderId || tl.AllowedUserIds.Contains(senderId)) &&
                tl.Id == taskList.Id);

            UpdateDefinition<MongoTaskList> update = Builders<MongoTaskList>.Update
                .Set(tl => tl.Name, taskList.Name)
                .Set(tl => tl.Tasks, taskList.Tasks);

            MongoTaskList mongoTaskList = await _collection.FindOneAndUpdateAsync(
                filter,
                update,
                new FindOneAndUpdateOptions<MongoTaskList>
                {
                    ReturnDocument = ReturnDocument.After
                });

            return _mapper.Map<TaskList>(mongoTaskList);
        }
        public async Task<TaskList> DeleteAsync(string senderId, string taskListId)
        {
            MongoTaskList mongoTaskList = await _collection.FindOneAndDeleteAsync(tl =>
                tl.Id == taskListId && tl.OwnerUserId == senderId);

            return _mapper.Map<TaskList>(mongoTaskList);
        }

        public async Task<List<string>> GetTaskListRelations(string senderId, string taskListId) 
        {
            MongoTaskList taskList = await _collection.Find(tl =>
                (tl.OwnerUserId == senderId || tl.AllowedUserIds.Contains(senderId))
                && tl.Id == taskListId
            ).FirstOrDefaultAsync();

            return taskList.AllowedUserIds;
        }

        public async Task<TaskList> AddTaskListRelation(string senderId, string taskListId, string accessedUserId)
        {
            FilterDefinition<MongoTaskList> filter = Builders<MongoTaskList>.Filter.Where(tl =>
                (tl.OwnerUserId == senderId || tl.AllowedUserIds.Contains(senderId)) &&
                tl.Id == taskListId);
            UpdateDefinition<MongoTaskList> update = Builders<MongoTaskList>.Update.Push(u => u.AllowedUserIds, accessedUserId);

            return _mapper.Map<TaskList>(await _collection.FindOneAndUpdateAsync(filter, update));
        }

        public async Task<TaskList> RemoveTaskListRelation(string senderId, string taskListId, string accessedUserId)
        {
            FilterDefinition<MongoTaskList> filter = Builders<MongoTaskList>.Filter.Where(tl =>
                (tl.OwnerUserId == senderId || tl.AllowedUserIds.Contains(senderId)) &&
                tl.Id == taskListId);
            UpdateDefinition<MongoTaskList> update = Builders<MongoTaskList>.Update.Pull(tl => tl.AllowedUserIds, accessedUserId);

            return _mapper.Map<TaskList>(await _collection.FindOneAndUpdateAsync(filter, update));
        }
    }
}
