using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Kanban.Api.Infrastructure;
using Kanban.Api.Models.User;
using Kanban.BusinessLogic.DTOs;
using Kanban.BusinessLogic.Interfaces.Sarvices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<UserModel>>(users));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                return Ok(_mapper.Map<UserModel>(user));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Admin)]
        public async Task<IActionResult> Post([FromBody]UserModel model)
        {
            try
            {
                var user = await _userService.CreateAsync(_mapper.Map<UserDto>(model));
                return Ok(_mapper.Map<UserModel>(user));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = Constant.Roles.Admin)]
        public async Task<IActionResult> Put([FromBody]UserModel model)
        {
            try
            {
                var user = await _userService.UpdateAsync(_mapper.Map<UserDto>(model));
                return Ok(_mapper.Map<UserModel>(user));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = Constant.Roles.Admin)]  
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}