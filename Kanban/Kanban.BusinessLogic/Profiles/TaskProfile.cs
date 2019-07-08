using AutoMapper;
using Kanban.BusinessLogic.DTOs;
using Kanban.DataAccess.Entities;


namespace Kanban.BusinessLogic.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskEntity, TaskDto>();
            CreateMap<TaskDto, TaskEntity>();
        }
    }
}
