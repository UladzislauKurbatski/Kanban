using AutoMapper;
using Kanban.Api.Models.TaskStatus;
using Kanban.BusinessLogic.DTOs;

namespace Kanban.Api.Profiles
{
    public class TaskStatusProfile : Profile
    {
        public TaskStatusProfile()
        {
            CreateMap<TaskStatusModel, TaskStatusDto>();
            CreateMap<TaskStatusDto, TaskStatusModel>();
        }
    }
}
