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
    public class TaskService : Service<TaskDto, TaskEntity>, ITaskService
    {
        const int ALLOWED_MINUTES_OFFSET = 3;
        const int ALLOWED_TITLE_TEXT_LENGTH = 256;
        const int ALLOWED_DESCRIPTION_TEXT_LENGTH = 512;

        private readonly ITaskRepository _taskRepository;
        private readonly IUserService _userService;


        public TaskService(ITaskRepository taskRepository, IMapper mapper, IUserService userService) : base(taskRepository, mapper)
        {
            _taskRepository = taskRepository;
            _userService = userService;
        }

        public async Task<TaskDto> AttachToUserAsync(int taskId, int userId)
        {
            if (taskId <= 0)
            {
                throw new ArgumentException(Constant.Validation.InvalidArgument(nameof(taskId)));
            }
            if (userId <= 0)
            {
                throw new ArgumentException(Constant.Validation.InvalidArgument(nameof(userId)));
            }

            var task = await _repository.GetByIdAsync(taskId);
            if (task == null)
            {
                throw new ArgumentException(Constant.Validation.InvalidArgument(nameof(taskId)));
            }

            var user = await _userService.GetByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException(Constant.Validation.InvalidArgument(nameof(userId)));
            }

            task.UserId = userId;
            var result =  await _taskRepository.TransactionUpdateAsync(task);
            return Map(result);
        }

        public bool CheckIfTitleExists(string title, int? id = null)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException(Constant.Validation.CantBeNullOrEmpty(nameof(title)));
            }

            if (id.HasValue && id.Value <= 0)
            {
                throw new ArgumentException(Constant.Validation.InvalidArgument(nameof(id)));
            }

            return id.HasValue
                ? _repository.Query().Any(t => t.Title == title && t.Id != id.Value)
                : _repository.Query().Any(t => t.Title == title);
        }

        public override async Task<TaskDto> CreateAsync(TaskDto model)
        {
            if (CheckIfTitleExists(model.Title))
            {
                throw new ArgumentException(Constant.Validation.ShouldBeUnique(nameof(model.Title)));
            }

            ValidateModel(model);
            return await base.CreateAsync(model);
        }

        public override Task<TaskDto> UpdateAsync(TaskDto model)
        {
            if (CheckIfTitleExists(model.Title, model.Id))
            {
                throw new ArgumentException(Constant.Validation.ShouldBeUnique(nameof(model.Title)));
            }

            ValidateModel(model);
            return base.UpdateAsync(model);
        }

        private void ValidateModel(TaskDto model)
        {
            if (model.Title.Length > ALLOWED_TITLE_TEXT_LENGTH)
            {
                throw new ArgumentException(Constant.Validation.TextLengthEqualOrLessThan(nameof(model.Title), ALLOWED_TITLE_TEXT_LENGTH));
            }

            if (Math.Abs((model.CreationDate.ToUniversalTime() - DateTime.UtcNow).Minutes) > ALLOWED_MINUTES_OFFSET)
            {
                throw new ArgumentException(Constant.Validation.Task.InvalidDateTime);
            }

            if (model.TypeId <= 0)
            {
                throw new ArgumentException(Constant.Validation.InvalidArgument(nameof(model.TypeId)));
            }

            if (model.StatusId <= 0)
            {
                throw new ArgumentException(Constant.Validation.InvalidArgument(nameof(model.StatusId)));
            }

            if (model.UserId.HasValue && model.UserId.Value <= 0)
            {
                throw new ArgumentException(Constant.Validation.InvalidArgument(nameof(model.UserId)));
            }

            if (model.ParentId.HasValue && model.ParentId.Value <= 0)
            {
                throw new ArgumentException(Constant.Validation.InvalidArgument(nameof(model.ParentId)));
            }

            if (model.Description.Length > ALLOWED_DESCRIPTION_TEXT_LENGTH)
            {
                throw new ArgumentException(Constant.Validation.TextLengthEqualOrLessThan(nameof(model.Description), ALLOWED_DESCRIPTION_TEXT_LENGTH));
            }
        }
    }
}
