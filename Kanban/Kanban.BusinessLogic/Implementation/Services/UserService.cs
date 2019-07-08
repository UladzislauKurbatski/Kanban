using AutoMapper;
using Kanban.BusinessLogic.DTOs;
using Kanban.BusinessLogic.Infrastructure;
using Kanban.BusinessLogic.Interfaces.Sarvices;
using Kanban.DataAccess.Entities;
using Kanban.DataAccess.Enums;
using Kanban.DataAccess.Interfaces.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kanban.BusinessLogic.Implementation.Services
{
    public class UserService : Service<UserDto, UserEntity>, IUserService
    {
        private readonly IUserRepository _userRepository;


        public UserService(IUserRepository userRepository, IMapper mapper) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
        }

        public UserDto AuthenticateUser(string login, string password)
        {
            var user = _userRepository.Query().FirstOrDefault(u => u.Login == login && u.Password == password);
            if (user == null)
            {
                throw new ArgumentException(Constant.Validation.User.InvalidLoginOrPassword);
            }

            return Map(user);
        }

        public bool CheckIfLoginExists(string login, int? id = null)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentException(Constant.Validation.CantBeNullOrEmpty(nameof(login)));
            }

            if (id.HasValue && id.Value <= 0)
            {
                throw new ArgumentException(Constant.Validation.InvalidArgument(nameof(id)));
            }

            return id.HasValue
                ? _userRepository.Query().Any(u => u.Login == login && u.Id != id.Value)
                : _userRepository.Query().Any(u => u.Login == login);
        }

        public override Task<UserDto> CreateAsync(UserDto model)
        {
            ValidateModel(model);

            if (CheckIfLoginExists(model.Login))
            {
                throw new ArgumentException(Constant.Validation.ShouldBeUnique(nameof(model.Login)));
            }
            
            return base.CreateAsync(model);
        }

        public override Task<UserDto> UpdateAsync(UserDto model)
        {
            ValidateModel(model);

            if (CheckIfLoginExists(model.Login, model.Id))
            {
                throw new ArgumentException(Constant.Validation.ShouldBeUnique(nameof(model.Login)));
            }

            return base.UpdateAsync(model);
        }

        private void ValidateModel(UserDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Login))
            {
                throw new ArgumentException(Constant.Validation.CantBeNullOrEmpty(nameof(model.Login)));
            }

            if (string.IsNullOrWhiteSpace(model.Password))
            {
                throw new ArgumentException(Constant.Validation.CantBeNullOrEmpty(nameof(model.Password)));
            }

            if (string.IsNullOrWhiteSpace(model.Role))
            {
                throw new ArgumentException(Constant.Validation.CantBeNullOrEmpty(nameof(model.Role)));
            }

            if (!Enum.TryParse<Roles>(model.Role, out var role))
            {
                throw new ArgumentException(Constant.Validation.InvalidArgument(nameof(model.Role)));
            }
        }
    }
}
