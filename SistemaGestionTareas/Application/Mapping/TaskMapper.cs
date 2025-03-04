using Application.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    internal class TaskMapper
    {
        internal static Domain.Entities.Task AddFromDto(TaskDto taskDto)
        {
            if(taskDto == null)
                throw new ArgumentNullException("TaskDto cannot be null");

            return new Domain.Entities.Task()
            {
                UserId = taskDto.UserId,
                Title = taskDto.Title,
                Description = taskDto.Description,
                ExpirationDate = taskDto.ExpirationDate,
                //Status = (Domain.Enums.TaskStatus)taskDto.Status
            };
        }

        internal static Domain.Entities.Task UpdateFromDto(TaskUpdateDto taskDto)
        {
            if (taskDto == null)
                throw new ArgumentNullException("TaskDto cannot be null");

            return new Domain.Entities.Task()
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                ExpirationDate = (DateOnly)taskDto.ExpirationDate,
            };
        }


        internal static IEnumerable<TaskGetDto> ToListDto(IEnumerable<Domain.Entities.Task> tasks)
        {
            var tasksDtos = tasks.Select(t => ToDto(t)).ToList();

            return tasksDtos;
        }

        internal static TaskGetDto ToDto(Domain.Entities.Task task)
        {
            if (task == null)
                throw new ArgumentNullException("Task cannot be null");

            return new TaskGetDto()
            {
                Id = task.Id,
                UserId = task.UserId,
                Title = task.Title,
                Description = task.Description,
                ExpirationDate = task.ExpirationDate,
                Status = task.Status.ToString()
            };
        }
    }
}
