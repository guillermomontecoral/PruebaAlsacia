using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        #region Inyeccion de dependencias
        private readonly TaskDbContext _taskDbContext;

        public TaskRepository(TaskDbContext taskDbContext)
        {
            _taskDbContext = taskDbContext;
        }
        #endregion

        public async Task Add(Domain.Entities.Task task)
        {

            try
            {
                if (task == null)
                    throw new Exception("Task cannot be null");

                await _taskDbContext.Tasks.AddAsync(task);
                await _taskDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var task = await _taskDbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);

                if (task == null)
                    throw new Exception("Task not found.");

                _taskDbContext.Tasks.Remove(task);
                await _taskDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Domain.Entities.Task>> GetAllTasks()
        {
            try
            {
                var tasks = await _taskDbContext.Tasks
                    .Include(t => t.User)
                    .OrderBy(t => t.ExpirationDate)
                    .ThenBy(t => t.Id)
                    .ThenBy(t => t.Status)
                    .ToListAsync();

                return tasks;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Domain.Entities.Task> GetById(int id)
        {
            try
            {
                var task = await _taskDbContext.Tasks
                    .Include(t => t.User)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (task == null)
                    throw new Exception("Task not found.");

                return task;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(int id, Domain.Entities.Task task)
        {
            try
            {

                if (task == null)
                {
                    throw new Exception("Task not found");
                }

                // Marca la entidad como modificada
                _taskDbContext.Entry(task).State = EntityState.Modified;

                await _taskDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating task: " + ex.Message);
            }

        }

        public async Task ChangeStatus(int id, int status)
        {
            var task = await _taskDbContext.Tasks
                .FirstOrDefaultAsync(x => x.Id == id);

            task.Status = (Domain.Enums.TaskStatus)status;
            _taskDbContext.Entry(task).State = EntityState.Modified;

            await _taskDbContext.SaveChangesAsync();
        }
    }
}
