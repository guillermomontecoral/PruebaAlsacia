using Application.UseCases.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class ChangeStatusTask : IChangeStatusTask
    {
        #region Inyeccion de dependencias
        private readonly ITaskRepository _taskRepository;

        public ChangeStatusTask(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        #endregion
        public async Task ChangeStatus(int id, int status)
        {
            await _taskRepository.ChangeStatus(id, status);
        }
    }
}
