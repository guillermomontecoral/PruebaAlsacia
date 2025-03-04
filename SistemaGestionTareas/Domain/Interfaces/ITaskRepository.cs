using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task Add(Domain.Entities.Task task);
        Task Delete(int id);
        Task Update(int id, Domain.Entities.Task task);
        Task<Domain.Entities.Task> GetById(int id);
        Task<IEnumerable<Domain.Entities.Task>> GetAllTasks();
        Task ChangeStatus(int id, int status);
    }
}
