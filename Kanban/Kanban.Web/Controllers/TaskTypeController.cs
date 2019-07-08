using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kanban.Api.Infrastructure;
using Kanban.Api.Models.TaskType;
using Kanban.BusinessLogic.DTOs;
using Kanban.BusinessLogic.Interfaces.Sarvices;
using Kanban.DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskTypeController : ControllerBase
    {
        private readonly ITaskTypeService _taskTypeService;
        private readonly IMapper _mapper;

        public TaskTypeController(ITaskTypeService taskTypeService, IMapper mapper)
        {
            _taskTypeService = taskTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var types = await _taskTypeService.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<TaskTypeModel>>(types));
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var type = await _taskTypeService.GetByIdAsync(id);
                return Ok(_mapper.Map<TaskTypeModel>(type));
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
        public async Task<IActionResult> Post([FromBody]TaskTypeModel model)
        {
            try
            {
                var type = await _taskTypeService.CreateAsync(_mapper.Map<TaskTypeDto>(model));
                return Ok(_mapper.Map<TaskTypeModel>(type));
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
        public async Task<IActionResult> Put([FromBody]TaskTypeModel model)
        {
            try
            {
                var type = await _taskTypeService.UpdateAsync(_mapper.Map<TaskTypeDto>(model));
                return Ok(_mapper.Map<TaskTypeModel>(type));
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
                await _taskTypeService.DeleteAsync(id);
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