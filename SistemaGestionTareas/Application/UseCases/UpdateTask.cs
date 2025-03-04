using Application.Dtos;
using Application.Mapping;
using Application.UseCases.Interfaces;
using Domain.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class UpdateTask : IUpdateTask
    {
        #region Inyeccion de dependencias
        private readonly ITaskRepository _taskRepository;

        public UpdateTask(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        #endregion
        public async Task Update(int id, TaskUpdateDto taskDto)
        {
            var task = await _taskRepository.GetById(id);
            if (task == null)
            {
                throw new Exception("Task not found");
            }

            task.Title = taskDto.Title;
            task.Description = taskDto.Description;
            task.ExpirationDate = (DateOnly)taskDto.ExpirationDate;

            await _taskRepository.Update(id, task);
        }
    }
}
