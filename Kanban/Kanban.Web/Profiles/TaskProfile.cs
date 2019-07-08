using AutoMapper;
using Kanban.Api.Models.Task;
using Kanban.BusinessLogic.DTOs;

namespace Kanban.Api.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskModel, TaskDto>();
            CreateMap<TaskDto, TaskModel>();
        }
    }
}
