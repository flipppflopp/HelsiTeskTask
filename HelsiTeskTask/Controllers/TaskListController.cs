using Application.DTOs;
using Application.Interfaces;
using Domain.MongoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskListController : ControllerBase
    {
        private readonly ITaskListService _service;

        public TaskListController(ITaskListService service)
        {
            _service = service;
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetById(string senderId,string Id)
        {
            return Ok(await _service.GetByIdAsync(senderId, Id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string senderId, int pageNumber, int pageSize)
        {
            return Ok(await _service.GetAllAsync(senderId, pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskListCreateDTO taskListDTO)
        {
            await _service.CreateAsync(taskListDTO);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> Update(TaskListDTO taskListDTO)
        {
            await _service.UpdateAsync(taskListDTO);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string senderId, string taskListId)
        {
            await _service.DeleteAsync(senderId, taskListId);
            return Ok();
        }

        [HttpGet("relation")]
        public async Task<IActionResult> GetTaskListRelations(string senderId, string taskListId) 
        {
            return Ok(await _service.GetTaskListRelations(senderId, taskListId));
        }

        [HttpPost("relation")]
        public async Task<IActionResult> AddTaskListRelation(TaskRelationDTO taskRelationDTO) 
        {
            await _service.AddTaskListRelation(taskRelationDTO);
            return Ok();
        }

        [HttpDelete("relation")]
        public async Task<IActionResult> RemoveTaskListRelation(TaskRelationDTO taskRelationDTO) 
        {
            await _service.RemoveTaskListRelation(taskRelationDTO);
            return Ok();
        }
    }
}
