using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.DTOs;

namespace HelsiTeskTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetById(string Id)
        {
            return Ok(await _service.GetByIdAsync(Id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateDTO userDTO)
        {
            await _service.CreateAsync(userDTO);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> Update(UserDTO userDTO)
        {
            await _service.UpdateAsync(userDTO);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(UserDTO userDTO)
        {
            await _service.DeleteAsync(userDTO);
            return Ok();
        }
    }
}
