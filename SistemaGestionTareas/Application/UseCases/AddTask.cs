using Application.Dtos;
using Application.Mapping;
using Application.UseCases.Interfaces;
using Domain.Interfaces;

namespace Application.UseCases
{
    public class AddTask : IAddTask
    {
        #region Inyeccion de dependencias
        private readonly ITaskRepository _taskRepository;

        public AddTask(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        #endregion
        public async Task Add(TaskDto taskDto)
        {

            if (taskDto == null)
            {
                throw new ArgumentNullException("Task cannot be null");
            }

            var task = TaskMapper.AddFromDto(taskDto);
            await _taskRepository.Add(task);
        }
    }
}
