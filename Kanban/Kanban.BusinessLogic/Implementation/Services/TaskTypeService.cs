using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kanban.BusinessLogic.DTOs;
using Kanban.BusinessLogic.Infrastructure;
using Kanban.BusinessLogic.Interfaces.Sarvices;
using Kanban.DataAccess.Entities;
using Kanban.DataAccess.Interfaces.Repositories;

namespace Kanban.BusinessLogic.Implementation.Services
{
    public class TaskTypeService : Service<TaskTypeDto, TaskTypeEntity>, ITaskTypeService
    {
        private readonly ITaskTypeRepository _taskTypeRepository;


        public TaskTypeService(ITaskTypeRepository taskTypeRepository, IMapper mapper) : base(taskTypeRepository, mapper)
        {
            _taskTypeRepository = taskTypeRepository;
        }
        
        public bool CheckIfNameExists(string name, int? id = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(Constant.Validation.CantBeNullOrEmpty(nameof(name)));
            }

            if (id.HasValue && id.Value <= 0)
            {
                throw new ArgumentException(Constant.Validation.InvalidArgument(nameof(id)));
            }

            return id.HasValue
                ? _taskTypeRepository.Query().Any(t => t.Name == name && t.Id != id.Value)
                : _taskTypeRepository.Query().Any(t => t.Name == name);
        }


        public override Task<TaskTypeDto> CreateAsync(TaskTypeDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new ArgumentException(Constant.Validation.CantBeNullOrEmpty(nameof(model.Name)));
            }

            if (CheckIfNameExists(model.Name))
            {
                throw new ArgumentException(Constant.Validation.ShouldBeUnique(nameof(model.Name)));
            }

            return base.CreateAsync(model);
        }

        public override Task<TaskTypeDto> UpdateAsync(TaskTypeDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new ArgumentException(Constant.Validation.CantBeNullOrEmpty(nameof(model.Name)));
            }
            
            if (CheckIfNameExists(model.Name, model.Id))
            {
                throw new ArgumentException(Constant.Validation.ShouldBeUnique(nameof(model.Name)));
            }

            return base.UpdateAsync(model);
        }
    }
}
