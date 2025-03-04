using Application.Dtos;
using Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SistemaGestionTareas.Controllers;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Domain.Interfaces;

namespace Tests
{
    public class ControllerTests
    {
        [Fact]
        public async Task TestAddTask_ReturnsCreatedResult()
        {
            // Arrange
            var mockAddTask = new Mock<IAddTask>();
            var mockGetAllTasks = new Mock<IGetAllTasks>();
            var mockGetByIdTask = new Mock<IGetByIdTask>();
            var mockDeleteTask = new Mock<IDeleteTask>();
            var mockUpdateTask = new Mock<IUpdateTask>();
            var mockChangeStatusTask = new Mock<IChangeStatusTask>();

            // Crear un DTO de TaskDto para la tarea a agregar (sin Id)
            var taskDto = new TaskDto
            {
                Title = "New Task",
                Description = "Description for new task",
                ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddDays(5)),
                //Status = (int)Domain.Enums.TaskStatus.Created,
                UserId = 1
            };

            // Simular el proceso de mapeo y agregar en la base de datos, generando el Id
            var taskWithId = new Domain.Entities.Task
            {
                Id = 1, // El Id se genera automáticamente en la base de datos
                Title = taskDto.Title,
                Description = taskDto.Description,
                ExpirationDate = taskDto.ExpirationDate,
                //Status = (Domain.Enums.TaskStatus)taskDto.Status,
                UserId = taskDto.UserId
            };

            // Simular que la capa de aplicación guarda la tarea y la devuelve con el Id generado
            mockAddTask.Setup(service => service.Add(It.IsAny<TaskDto>()))
                                  .Returns(Task.FromResult(taskWithId));  // Devolvemos el objeto de dominio con Id envuelto en un Task

            var controller = new TaskController(
                mockAddTask.Object,
                mockGetAllTasks.Object,
                mockGetByIdTask.Object,
                mockDeleteTask.Object,
                mockUpdateTask.Object,
                mockChangeStatusTask.Object
            );

            // Act
            var result = await controller.Add(taskDto); // Llamada al método del controlador para agregar una tarea

            // Assert
            mockAddTask.Verify(service => service.Add(It.IsAny<TaskDto>()), Times.Once); // Verifica que se llamó al servicio de la capa de aplicación

            // Verificar que el resultado es un ActionResult y que es un CreatedResult
            var createdResult = Assert.IsType<CreatedResult>(result); // Asegúrate de que el resultado sea un CreatedResult

            // Verifica el código de estado HTTP (201)
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact] 
        public async Task TestGetAll_ReturnsOkResult() 
        {
            // Arrange
            var mockAddTask = new Mock<IAddTask>();
            var mockGetAllTasks = new Mock<IGetAllTasks>();
            var mockGetByIdTask = new Mock<IGetByIdTask>();
            var mockDeleteTask = new Mock<IDeleteTask>();
            var mockUpdateTask = new Mock<IUpdateTask>();
            var mockChangeStatusTask = new Mock<IChangeStatusTask>();

            // Configurar el mock de la capa de aplicación (no es necesario mockear el repositorio aquí)
            mockGetAllTasks.Setup(service => service.GetAllTasks())
                           .Returns(Task.FromResult<IEnumerable<TaskGetDto>>(new List<TaskGetDto>
                           {
                                new TaskGetDto
                                {
                                    Id = 1,
                                    Title = "Task One",
                                    Description = "Description for Task One",
                                    ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddDays(5)),
                                    Status = Domain.Enums.TaskStatus.Created.ToString(),
                                    UserId = 1
                                },
                                new TaskGetDto
                                {
                                    Id = 2,
                                    Title = "Task Two",
                                    Description = "Description for Task Two",
                                    ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddDays(10)),
                                    Status = Domain.Enums.TaskStatus.InProgress.ToString(),
                                    UserId = 2
                                },
                                new TaskGetDto
                                {
                                    Id = 3,
                                    Title = "Task Three",
                                    Description = "Description for Task Three",
                                    ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddDays(15)),
                                    Status = Domain.Enums.TaskStatus.Completed.ToString(),
                                    UserId = 3
                                },
                                new TaskGetDto
                                {
                                    Id = 4,
                                    Title = "Task Four",
                                    Description = "Description for Task Four",
                                    ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddDays(20)),
                                    Status = Domain.Enums.TaskStatus.Created.ToString(),
                                    UserId = 1
                                },
                                new TaskGetDto
                                {
                                    Id = 5,
                                    Title = "Task Five",
                                    Description = "Description for Task Five",
                                    ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddDays(25)),
                                    Status = Domain.Enums.TaskStatus.InProgress.ToString(),
                                    UserId = 2
                                }
                           }));

            var controller = new TaskController(
                mockAddTask.Object,
                mockGetAllTasks.Object,  // Inyectar el mock de la capa de aplicación
                mockGetByIdTask.Object,
                mockDeleteTask.Object,
                mockUpdateTask.Object,
                mockChangeStatusTask.Object
            );

            // Act
            var result = await controller.GetAll(); // Llamada al método del controlador

            // Assert
            mockGetAllTasks.Verify(service => service.GetAllTasks(), Times.Once); // Verifica que se llamó al servicio de aplicación
            var actionResult = Assert.IsType<ActionResult<IEnumerable<TaskGetDto>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<List<TaskGetDto>>(okResult.Value);

            // Verifica que el valor devuelto es el esperado
            Assert.True(returnValue.Count > 1); // Verifica que haya más de una tarea
            Assert.Equal("Task One", returnValue[0].Title); // Verifica que el título sea el esperado
            Assert.Equal("Description for Task One", returnValue[0].Description); // Verifica la descripción
        }

        [Fact]
        public async Task TestGetAll_ReturnsNotFoundResult()
        {
            // Arrange
            var mockAddTask = new Mock<IAddTask>();
            var mockGetAllTasks = new Mock<IGetAllTasks>(); // Solo mockea la capa de aplicación
            var mockGetByIdTask = new Mock<IGetByIdTask>();
            var mockDeleteTask = new Mock<IDeleteTask>();
            var mockUpdateTask = new Mock<IUpdateTask>();
            var mockChangeStatusTask = new Mock<IChangeStatusTask>();

            // Configurar el mock de la capa de aplicación para devolver una lista vacía de tareas
            mockGetAllTasks.Setup(service => service.GetAllTasks())
                           .Returns(Task.FromResult<IEnumerable<TaskGetDto>>(new List<TaskGetDto>()));

            var controller = new TaskController(
                mockAddTask.Object,
                mockGetAllTasks.Object,  // Inyectar el mock de la capa de aplicación
                mockGetByIdTask.Object,
                mockDeleteTask.Object,
                mockUpdateTask.Object,
                mockChangeStatusTask.Object
            );

            // Act
            var result = await controller.GetAll(); // Llamada al método del controlador con una lista vacía

            // Assert
            mockGetAllTasks.Verify(service => service.GetAllTasks(), Times.Once); // Verifica que se llamó al servicio de aplicación
            var actionResult = Assert.IsType<ActionResult<IEnumerable<TaskGetDto>>>(result); // Verifica que el resultado sea ActionResult de tipo IEnumerable<TaskGetDto>
            var notFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result); // Verifica que el resultado sea NotFoundResult

        }

        [Fact]
        public async Task TestGetById_ReturnsOkResult() 
        {
            // Arrange
            var mockAddTask = new Mock<IAddTask>();
            var mockGetAllTasks = new Mock<IGetAllTasks>(); // Solo mockea la capa de aplicación
            var mockGetByIdTask = new Mock<IGetByIdTask>();
            var mockDeleteTask = new Mock<IDeleteTask>();
            var mockUpdateTask = new Mock<IUpdateTask>();
            var mockChangeStatusTask = new Mock<IChangeStatusTask>();

            // Configurar el mock de la capa de aplicación (no es necesario mockear el repositorio aquí)
            mockGetByIdTask.Setup(service => service.GetByIdAsync(67))
                                              .Returns(Task.FromResult(new TaskGetDto
                                              {
                                                  Id = 67,
                                                  Title = "Test Task",
                                                  Description = "This is a test task",
                                                  ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                                                  Status = Domain.Enums.TaskStatus.Created.ToString(),
                                                  UserId = 1
                                              }));

            var controller = new TaskController(
                mockAddTask.Object,
                mockGetAllTasks.Object,  // Inyectar el mock de la capa de aplicación
                mockGetByIdTask.Object,
                mockDeleteTask.Object,
                mockUpdateTask.Object,
                mockChangeStatusTask.Object
            );

            // Act
            var result = await controller.GetById(67); // Llamada al método del controlador

            // Assert
            mockGetByIdTask.Verify(service => service.GetByIdAsync(67), Times.Once); // Verifica que se llamó al servicio de aplicación
            var actionResult = Assert.IsType<ActionResult<TaskGetDto>>(result); // Verifica que es un ActionResult de tipo TaskGetDto
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result); // Verifica que el resultado sea un OkObjectResult
            var returnValue = Assert.IsType<TaskGetDto>(okResult.Value); // Verifica que el valor devuelto sea de tipo TaskGetDto
            Assert.Equal(67, returnValue.Id); // Verifica que el Id sea el esperado
            Assert.Equal("Test Task", returnValue.Title); // Verifica que el título sea el esperado
            Assert.Equal("This is a test task", returnValue.Description); // Verifica la descripción
        }

        [Fact] 
        public async Task TestGetById_ReturnsNotFoundResult()
        {
            // Arrange
            var mockAddTask = new Mock<IAddTask>();
            var mockGetAllTasks = new Mock<IGetAllTasks>(); // Solo mockea la capa de aplicación
            var mockGetByIdTask = new Mock<IGetByIdTask>();
            var mockDeleteTask = new Mock<IDeleteTask>();
            var mockUpdateTask = new Mock<IUpdateTask>();
            var mockChangeStatusTask = new Mock<IChangeStatusTask>();

            // Configurar el mock de la capa de aplicación para simular que no se encuentra el dato
            mockGetByIdTask.Setup(service => service.GetByIdAsync(99)) // El id que no existe (por ejemplo, 99)
                           .Returns(Task.FromResult<TaskGetDto>(null)); // Devuelve null para simular que no se encuentra la tarea

            var controller = new TaskController(
                mockAddTask.Object,
                mockGetAllTasks.Object,  // Inyectar el mock de la capa de aplicación
                mockGetByIdTask.Object,
                mockDeleteTask.Object,
                mockUpdateTask.Object,
                mockChangeStatusTask.Object
            );

            // Act
            var result = await controller.GetById(99); // Llamada al método del controlador con un id que no existe

            // Assert
            mockGetByIdTask.Verify(service => service.GetByIdAsync(99), Times.Once); // Verifica que se llamó al servicio de aplicación
            var actionResult = Assert.IsType<ActionResult<TaskGetDto>>(result); // Verifica que el resultado es un ActionResult de tipo TaskGetDto
            var notFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result); // Verifica que el resultado sea un NotFoundResult
        }

        [Fact]
        public async Task TestDeleteTask_ReturnsOkResult()
        {
            // Arrange
            var mockAddTask = new Mock<IAddTask>();
            var mockGetAllTasks = new Mock<IGetAllTasks>();
            var mockGetByIdTask = new Mock<IGetByIdTask>();
            var mockDeleteTask = new Mock<IDeleteTask>(); // Mock de la capa de aplicación para eliminar tareas
            var mockUpdateTask = new Mock<IUpdateTask>();
            var mockChangeStatusTask = new Mock<IChangeStatusTask>();

            // Simular que la capa de aplicación elimina la tarea
            mockDeleteTask.Setup(service => service.Delete(It.IsAny<int>()))
                          .Returns(Task.CompletedTask); // Simula una operación de eliminación exitosa

            var controller = new TaskController(
                mockAddTask.Object,
                mockGetAllTasks.Object,
                mockGetByIdTask.Object,
                mockDeleteTask.Object,
                mockUpdateTask.Object,
                mockChangeStatusTask.Object
            );

            int taskIdToDelete = 1; // ID de la tarea a eliminar

            // Act
            var result = await controller.Delete(taskIdToDelete); // Llamada al método del controlador para eliminar la tarea

            // Assert
            mockDeleteTask.Verify(service => service.Delete(taskIdToDelete), Times.Once); // Verifica que se llamó al servicio de eliminación

            // Verifica que el resultado es un OkResult (200 OK)
            var okResult = Assert.IsType<OkResult>(result); // Verifica que el resultado es un OkResult
            Assert.Equal(200, okResult.StatusCode); // Verifica que el código de estado HTTP sea 200
        }

        [Fact]
        public async Task TestUpdateTask_ReturnsNoContentResult()
        {
            // Arrange
            var mockAddTask = new Mock<IAddTask>();
            var mockGetAllTasks = new Mock<IGetAllTasks>();
            var mockGetByIdTask = new Mock<IGetByIdTask>();
            var mockDeleteTask = new Mock<IDeleteTask>();
            var mockUpdateTask = new Mock<IUpdateTask>(); 
            var mockChangeStatusTask = new Mock<IChangeStatusTask>();

            var taskUpdateDto = new TaskUpdateDto
            {
                Title = "Updated Task",
                Description = "Updated description for the task",
                ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddDays(5)),
            };

            // Simular que la capa de aplicación actualiza la tarea correctamente
            mockUpdateTask.Setup(service => service.Update(It.IsAny<int>(), It.IsAny<TaskUpdateDto>()))
                          .Returns(Task.CompletedTask); // Simula una operación de actualización exitosa

            var controller = new TaskController(
                mockAddTask.Object,
                mockGetAllTasks.Object,
                mockGetByIdTask.Object,
                mockDeleteTask.Object,
                mockUpdateTask.Object,
                mockChangeStatusTask.Object
            );

            int taskIdToUpdate = 1; // ID de la tarea que se va a actualizar

            // Act
            var result = await controller.Update(taskIdToUpdate, taskUpdateDto); // Llamada al método del controlador para actualizar la tarea

            // Assert
            mockUpdateTask.Verify(service => service.Update(taskIdToUpdate, taskUpdateDto), Times.Once); // Verifica que se llamó al servicio de actualización

            // Verifica que el resultado es un NoContentResult (204 No Content)
            var noContentResult = Assert.IsType<NoContentResult>(result); // Verifica que el resultado es un NoContentResult
            Assert.Equal(204, noContentResult.StatusCode); // Verifica que el código de estado HTTP sea 204
        }

    }
}



