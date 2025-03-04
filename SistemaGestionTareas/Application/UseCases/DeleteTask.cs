using Application.UseCases.Interfaces;
using Domain.Interfaces;

namespace Application.UseCases
{
    public class DeleteTask : IDeleteTask
    {
        #region Inyeccion de dependencias
        private readonly ITaskRepository _taskRepository;

        public DeleteTask(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        #endregion
        public async Task Delete(int id)
        {
            await _taskRepository.Delete(id);
        }
    }
}
