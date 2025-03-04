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
    public class GetByIdTask : IGetByIdTask
    {
        #region Inyeccion de dependencias
        private readonly ITaskRepository _taskRepository;

        public GetByIdTask(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        #endregion
        public async Task<TaskGetDto> GetByIdAsync(int id)
        {
            var task = await _taskRepository.GetById(id);

            return TaskMapper.ToDto(task);
        }
    }
}
