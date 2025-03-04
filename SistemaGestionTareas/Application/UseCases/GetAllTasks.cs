using Application.Dtos;
using Application.Mapping;
using Application.UseCases.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class GetAllTasks : IGetAllTasks
    {
        #region Inyeccion de dependencias
        private readonly ITaskRepository _taskRepository;

        public GetAllTasks(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        #endregion
        async Task<IEnumerable<TaskGetDto>> IGetAllTasks.GetAllTasks()
        {
            var tasks = await _taskRepository.GetAllTasks();

            return TaskMapper.ToListDto(tasks);
        }
    }
}
