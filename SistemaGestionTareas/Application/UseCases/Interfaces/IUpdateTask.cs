using Application.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Interfaces
{
    public interface IUpdateTask
    {
        Task Update(int id, TaskUpdateDto taskDto);
    }
}
