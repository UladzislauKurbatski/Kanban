using AutoMapper;
using Kanban.BusinessLogic.DTOs;
using Kanban.BusinessLogic.Implementation.Services;
using Kanban.BusinessLogic.Interfaces.Sarvices;
using Kanban.DataAccess.Entities;
using Kanban.DataAccess.Interfaces.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class TaskServiceTest
    {
        private static List<TaskEntity> fakeEntities => new List<TaskEntity>
        {
            new TaskEntity
            {
                Id  = 1,
                Title = "Task 1",
                Description = "Task 1 Description",
                CreationDate = DateTime.UtcNow,
                TypeId = 1,
                StatusId = 1
            },
            new TaskEntity
            {
                Id  = 2,
                Title = "Task 2",
                Description = "Task 2 Description",
                CreationDate = DateTime.UtcNow,
                TypeId = 2,
                StatusId = 2
            },
            new TaskEntity
            {
                Id  = 3,
                Title = "Task 3",
                Description = "Task 3 Description",
                CreationDate = DateTime.UtcNow,
                TypeId = 3,
                StatusId = 3
            },
        };

        private static TaskEntity fakeEntity => new TaskEntity
        {
            Id = 1,
            Title = "Task 1",
            Description = "Task 1 Description",
            CreationDate = DateTime.UtcNow,
            TypeId = 1,
            StatusId = 1
        };

        private static List<TaskDto> fakeDtos => new List<TaskDto>
        {
            new TaskDto
            {
                Id  = 1,
                Title = "Task 1",
                Description = "Task 1 Description",
                CreationDate = DateTime.UtcNow,
                TypeId = 1,
                StatusId = 1
            },
            new TaskDto
            {
                Id  = 2,
                Title = "Task 2",
                Description = "Task 2 Description",
                CreationDate = DateTime.UtcNow,
                TypeId = 2,
                StatusId = 2
            },
            new TaskDto
            {
                Id  = 3,
                Title = "Task 3",
                Description = "Task 3 Description",
                CreationDate = DateTime.UtcNow,
                TypeId = 3,
                StatusId = 3
            },
        };

        private static TaskDto fakeDto => new TaskDto
        {
            Id = 1,
            Title = "Task 1",
            Description = "Task 1 Description",
            CreationDate = DateTime.UtcNow,
            TypeId = 1,
            StatusId = 1
        };

        private ITaskService _taskService;
        private Mock<ITaskRepository> _taskRepositoryMock;
        private Mock<IUserService> _userServiceMock;
        private Mock<IMapper> _mapperMock;


        [SetUp]
        public void Setup()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _userServiceMock = new Mock<IUserService>();
            _mapperMock = new Mock<IMapper>();
            _taskService = new TaskService(_taskRepositoryMock.Object, _mapperMock.Object, _userServiceMock.Object);
        }

        [Test]
        public void GetAllShouldReturnAllEntitiesFromFakeEntitiesListTest()
        {
            //Arrange
            _mapperMock.Setup(m => m.Map<IEnumerable<TaskDto>>(It.IsAny<IEnumerable<TaskEntity>>())).Returns(fakeDtos);
            _taskRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(fakeEntities);

            //Act
            var tasks = _taskService.GetAllAsync().Result.ToList();

            //Assert
            Assert.NotNull(tasks);
            Assert.AreEqual(fakeDtos.Count, tasks.Count);
            Assert.AreEqual(fakeDtos.Last().Id, tasks.Last().Id);
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(15)]
        public void GetByIdShouldReturnFakeEntityFieldTest(int id)
        {
            //Arrange
            _mapperMock.Setup(m => m.Map<TaskDto>(It.IsAny<TaskEntity>())).Returns(fakeDto);
            _taskRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(fakeEntity);
            
            //Act
            var task = _taskService.GetByIdAsync(id).Result;

            //Assert
            Assert.NotNull(task);
            Assert.AreEqual(fakeDto.Id, task.Id);
            Assert.AreEqual(fakeDto.Title, fakeDto.Title);
        }

        [Test]
        public void CreateTaskShouldReturnCreatedTask()
        {
            //Arrange
            _mapperMock.Setup(m => m.Map<TaskDto>(It.IsAny<TaskEntity>())).Returns(fakeDto);
            _taskRepositoryMock.Setup(r => r.Query()).Returns(fakeEntities.Where(t => t.Title != fakeDto.Title).AsQueryable);
            _taskRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<TaskEntity>())).ReturnsAsync(fakeEntity);

            //Act
            var task = _taskService.CreateAsync(fakeDto).Result;

            //Assert
            Assert.NotNull(task);
            Assert.AreEqual(fakeDto.Id, task.Id);
            Assert.AreEqual(fakeDto.Title, task.Title);
        }

        [Test]
        public void UpdateTaskShouldReturnUpdatedTask()
        {
            //Arrange
            _mapperMock.Setup(m => m.Map<TaskDto>(It.IsAny<TaskEntity>())).Returns(fakeDto);
            _taskRepositoryMock.Setup(r => r.Query()).Returns(fakeEntities.Where(t => t.Title != fakeDto.Title).AsQueryable);
            _taskRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<TaskEntity>())).ReturnsAsync(fakeEntity);

            //Act
            var task = _taskService.UpdateAsync(fakeDto).Result;

            //Assert
            Assert.NotNull(task);
            Assert.AreEqual(fakeDto.Id, task.Id);
            Assert.AreEqual(fakeDto.Title, task.Title);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void DeleteTaskShouldntThrowExceptionIfEntityWithIdExists(int id)
        {
            //Arrange
            var entities = fakeEntities;
            _taskRepositoryMock
                .Setup(r => r.DeleteAsync(It.IsAny<int>()))
                .Callback<int>(taskId => entities.Remove(entities.First(e => e.Id == taskId)));
            var count = entities.Count;

            //Act
            //Assert
            Assert.DoesNotThrowAsync(async () => await _taskService.DeleteAsync(id));
            Assert.IsTrue(entities.Count < count);
        }

        [TestCase(-10)]
        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        public void GetByIdShouldThrowExceptionIfIdIsNotValid(int id)
        {
            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.GetByIdAsync(id));
        }

        [Test]
        public void CreatingWithExistingTitleShouldThrowArgumentException()
        {
            //Arrange
            _taskRepositoryMock.Setup(r => r.Query()).Returns(fakeEntities.AsQueryable);

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.CreateAsync(fakeDto));
            _taskRepositoryMock.Verify(r => r.Query(), Times.Once);
        }

        [TestCase(-10)]
        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        public void CreatingWithInvalidTypeIdShouldThrowArgumentException(int id)
        {
            //Arrange
            var dto = fakeDto;
            dto.TypeId = id;
            
            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.CreateAsync(dto));
            _taskRepositoryMock.Verify(r => r.Query(), Times.Once);
        }

        [TestCase(-10)]
        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        public void CreatingWithInvalidStatusIdShouldThrowArgumentException(int id)
        {
            //Arrange
            var dto = fakeDto;
            dto.StatusId = id;

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.CreateAsync(dto));
            _taskRepositoryMock.Verify(r => r.Query(), Times.Once);
        }

        [Test]
        public void CreatingWithEmptyTitleShouldThrowArgumentException()
        {
            //Arrange
            _taskRepositoryMock.Setup(r => r.Query()).Returns(fakeEntities.AsQueryable);
            var dto = fakeDto;
            dto.Title = null;

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.CreateAsync(dto));
            _taskRepositoryMock.Verify(r => r.Query(), Times.Never);
        }

        [TestCase(-10)]
        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        public void CreatingWithInvalidUserIdShouldThrowArgumentException(int id)
        {
            //Arrange
            var dto = fakeDto;
            dto.UserId = id;

            _taskRepositoryMock.Setup(r => r.Query()).Returns(fakeEntities.Where(t => t.Title != dto.Title).AsQueryable);

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.CreateAsync(dto));
            _taskRepositoryMock.Verify(r => r.Query(), Times.Once);
        }

        [TestCase(-10)]
        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        public void CreatingWithInvalidParentIdShouldThrowArgumentException(int id)
        {
            //Arrange
            var dto = fakeDto;
            dto.ParentId = id;

            _taskRepositoryMock.Setup(r => r.Query()).Returns(fakeEntities.Where(t => t.Title != dto.Title).AsQueryable);

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.CreateAsync(dto));
            _taskRepositoryMock.Verify(r => r.Query(), Times.Once);
        }

        [TestCase(513)]
        [TestCase(1000)]
        [TestCase(1500)]
        public void CreatingWithDescriptionLengthMoreThan512ShouldThrowArgumentException(int length)
        {
            //Arrange
            var dto = fakeDto;
            dto.Description = new string('*', length);

            _taskRepositoryMock.Setup(r => r.Query()).Returns(fakeEntities.Where(t => t.Title != dto.Title).AsQueryable);

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.CreateAsync(dto));
            _taskRepositoryMock.Verify(r => r.Query(), Times.Once);
        }

        [TestCase(257)]
        [TestCase(512)]
        [TestCase(1024)]
        public void CreatingWitTitleLengthMoreThan256ShouldThrowArgumentException(int length)
        {
            //Arrange
            var dto = fakeDto;
            dto.Title = new string('*', length);

            _taskRepositoryMock.Setup(r => r.Query()).Returns(fakeEntities.Where(t => t.Title != dto.Title).AsQueryable);

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.CreateAsync(dto));
            _taskRepositoryMock.Verify(r => r.Query(), Times.Once);
        }

        [Test]
        public void UpdatingWithExistingTitleShouldThrowArgumentException()
        {
            //Arrange
            _taskRepositoryMock.Setup(r => r.Query()).Returns(fakeEntities.AsQueryable);
            var dto = fakeDto;
            dto.Title = fakeEntities.FirstOrDefault(t => t.Id != dto.Id).Title;

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.UpdateAsync(dto));
            _taskRepositoryMock.Verify(r => r.Query(), Times.Once);
        }

        [TestCase(-10)]
        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        public void UpdatingWithInvalidTypeIdShouldThrowArgumentException(int id)
        {
            //Arrange
            var dto = fakeDto;
            dto.TypeId = id;

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.UpdateAsync(dto));
            _taskRepositoryMock.Verify(r => r.Query(), Times.Once);
        }

        [TestCase(-10)]
        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        public void UpdatingWithInvalidStatusIdShouldThrowArgumentException(int id)
        {
            //Arrange
            var dto = fakeDto;
            dto.StatusId = id;

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.UpdateAsync(dto));
            _taskRepositoryMock.Verify(r => r.Query(), Times.Once);
        }

        [Test]
        public void UpdatingWithEmptyTitleShouldThrowArgumentException()
        {
            //Arrange
            _taskRepositoryMock.Setup(r => r.Query()).Returns(fakeEntities.AsQueryable);
            var dto = fakeDto;
            dto.Title = null;

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.UpdateAsync(dto));
            _taskRepositoryMock.Verify(r => r.Query(), Times.Never);
        }

        [TestCase(-10)]
        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        public void UpdatingWithInvalidUserIdShouldThrowArgumentException(int id)
        {
            //Arrange
            var dto = fakeDto;
            dto.UserId = id;

            _taskRepositoryMock.Setup(r => r.Query()).Returns(fakeEntities.Where(t => t.Title != dto.Title).AsQueryable);

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.UpdateAsync(dto));
            _taskRepositoryMock.Verify(r => r.Query(), Times.Once);
        }

        [TestCase(-10)]
        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        public void UpdatingWithInvalidParentIdShouldThrowArgumentException(int id)
        {
            //Arrange
            var dto = fakeDto;
            dto.ParentId = id;

            _taskRepositoryMock.Setup(r => r.Query()).Returns(fakeEntities.Where(t => t.Title != dto.Title).AsQueryable);

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.UpdateAsync(dto));
            _taskRepositoryMock.Verify(r => r.Query(), Times.Once);
        }

        [TestCase(513)]
        [TestCase(1000)]
        [TestCase(1500)]
        public void UpdatingWithDescriptionLengthMoreThan512ShouldThrowArgumentException(int length)
        {
            //Arrange
            var dto = fakeDto;
            dto.Description = new string('*', length);

            _taskRepositoryMock.Setup(r => r.Query()).Returns(fakeEntities.Where(t => t.Title != dto.Title).AsQueryable);

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.UpdateAsync(dto));
            _taskRepositoryMock.Verify(r => r.Query(), Times.Once);
        }

        [TestCase(257)]
        [TestCase(512)]
        [TestCase(1024)]
        public void UpdatingWitTitleLengthMoreThan256ShouldThrowArgumentException(int length)
        {
            //Arrange
            var dto = fakeDto;
            dto.Title = new string('*', length);

            _taskRepositoryMock.Setup(r => r.Query()).Returns(fakeEntities.Where(t => t.Title != dto.Title).AsQueryable);

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.UpdateAsync(dto));
            _taskRepositoryMock.Verify(r => r.Query(), Times.Once);
        }

        [TestCase(-15)]
        [TestCase(-10)]
        [TestCase(0)]
        public void DeletingWithInvalidIdShouldnThrowArgumentException(int id)
        {
            //Arrange
            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.DeleteAsync(id));
        }
    }
}