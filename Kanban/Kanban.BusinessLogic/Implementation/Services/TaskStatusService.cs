using AutoMapper;
using Kanban.BusinessLogic.DTOs;
using Kanban.BusinessLogic.Infrastructure;
using Kanban.BusinessLogic.Interfaces.Sarvices;
using Kanban.DataAccess.Entities;
using Kanban.DataAccess.Interfaces.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kanban.BusinessLogic.Implementation.Services
{
    public class TaskStatusService : Service<TaskStatusDto, TaskStatusEntity>, ITaskStatusService
    {
        private readonly ITaskStatusRepository _taskStatusRepository;


        public TaskStatusService(ITaskStatusRepository taskStatusRepository, IMapper mapper) : base(taskStatusRepository, mapper)
        {
            _taskStatusRepository = taskStatusRepository;
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
                ? _taskStatusRepository.Query().Any(t => t.Name == name && t.Id != id.Value)
                : _taskStatusRepository.Query().Any(t => t.Name == name);
        }

        public override Task<TaskStatusDto> CreateAsync(TaskStatusDto model)
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

        public override Task<TaskStatusDto> UpdateAsync(TaskStatusDto model)
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
