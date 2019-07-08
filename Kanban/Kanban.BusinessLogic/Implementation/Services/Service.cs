using AutoMapper;
using Kanban.BusinessLogic.Infrastructure;
using Kanban.BusinessLogic.Interfaces.Sarvices;
using Kanban.DataAccess.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.BusinessLogic.Implementation.Services
{
    abstract public class Service<TDto, TEntity> : IService<TDto>
        where TDto : class, DataAccess.Interfaces.IIdentity 
        where TEntity : class, DataAccess.Interfaces.IIdentity
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public Service(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return Map(entities);
        }

        public virtual async Task<TDto> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(Constant.Validation.InvalidArgument(nameof(id)));
            }

            var entity = await _repository.GetByIdAsync(id);
            return Map(entity);
        }

        public virtual async Task<TDto> CreateAsync(TDto model)
        {
            var entity = await _repository.CreateAsync(Map(model));
            return Map(entity);
        }

        public virtual async Task<TDto> UpdateAsync(TDto model)
        {
            var entity = await _repository.UpdateAsync(Map(model));
            return Map(entity);
        }

        public virtual async Task DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(Constant.Validation.InvalidArgument(nameof(id)));
            }

            await _repository.DeleteAsync(id);
        }

        protected TDto Map(TEntity entity)
        {
            return _mapper.Map<TDto>(entity);
        }

        protected TEntity Map(TDto dto)
        {
            return _mapper.Map<TEntity>(dto);
        }

        protected IEnumerable<TDto> Map(IEnumerable<TEntity> entities)
        {
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        protected IEnumerable<TEntity> Map(IEnumerable<TDto> dtos)
        {
            return _mapper.Map<IEnumerable<TEntity>>(dtos);
        }
    }
}
