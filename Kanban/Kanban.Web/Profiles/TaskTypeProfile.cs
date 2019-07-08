using AutoMapper;
using Kanban.Api.Models.TaskType;
using Kanban.BusinessLogic.DTOs;

namespace Kanban.Api.Profiles
{
    public class TaskTypeProfile : Profile
    {
        public TaskTypeProfile()
        {
            CreateMap<TaskTypeModel, TaskTypeDto>();
            CreateMap<TaskTypeDto, TaskTypeModel>();
        }
    }
}
