using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kanban.Api.Models.TaskStatus;
using Kanban.BusinessLogic.DTOs;
using Kanban.BusinessLogic.Interfaces.Sarvices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskStatusController : ControllerBase
    {
        private readonly ITaskStatusService _taskStatusService;
        private readonly IMapper _mapper; 


        public TaskStatusController(ITaskStatusService taskStatusService, IMapper mapper)
        {
            _taskStatusService = taskStatusService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tasks = await _taskStatusService.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<TaskStatusModel>>(tasks));
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
                var task = await _taskStatusService.GetByIdAsync(id);
                return Ok(_mapper.Map<TaskStatusModel>(task));
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
        public async Task<IActionResult> Post([FromBody]TaskStatusModel model)
        {
            try
            {
                var task = await _taskStatusService.CreateAsync(_mapper.Map<TaskStatusDto>(model));
                return Ok(_mapper.Map<TaskStatusModel>(task));
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
        public async Task<IActionResult> Put([FromBody]TaskStatusModel model)
        {
            try
            {
                var task = await _taskStatusService.UpdateAsync(_mapper.Map<TaskStatusDto>(model));
                return Ok(_mapper.Map<TaskStatusModel>(task));
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _taskStatusService.DeleteAsync(id);
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