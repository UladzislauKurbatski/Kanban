using AutoMapper;
using Kanban.BusinessLogic.DTOs;
using Kanban.DataAccess.Entities;

namespace Kanban.BusinessLogic.Profiles
{
    public class TaskStatusProfile : Profile
    {
        public TaskStatusProfile()
        {
            CreateMap<TaskStatusEntity, TaskStatusDto>();
            CreateMap<TaskStatusDto, TaskStatusEntity>();
        }
    }
}
