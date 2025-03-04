using Application.Dtos;
using Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaGestionTareas.Controllers
{
    [Route("api/task/")]
    [ApiController]
    [Produces("application/json")]
    public class TaskController : ControllerBase
    {
        private readonly IAddTask _addTask;
        private readonly IGetAllTasks _getAllTasks;
        private readonly IGetByIdTask _getByIdTask;
        private readonly IDeleteTask _deleteTask;
        private readonly IUpdateTask _updateTask;
        private readonly IChangeStatusTask _changeStatusTask;

        public TaskController(IAddTask addTask, IGetAllTasks getAllTasks, IGetByIdTask getByIdTask, IDeleteTask deleteTask, IUpdateTask updateTask, IChangeStatusTask changeStatusTask)
        {
            _addTask = addTask;
            _getAllTasks = getAllTasks;
            _getByIdTask = getByIdTask;
            _deleteTask = deleteTask;
            _updateTask = updateTask;
            _changeStatusTask = changeStatusTask;
        }

        [HttpPost("add-new")]
        [Authorize]
        public async Task<ActionResult> Add([FromBody] TaskDto task)
        {
            try
            {
                if (task == null)
                {
                    return BadRequest("Task cannot be null");
                }

                await _addTask.Add(task);
                return Created(string.Empty, null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-all")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TaskGetDto>>> GetAll()
        {
            try
            {
                var tasks = await _getAllTasks.GetAllTasks();

                if (tasks.Count() < 1)
                {
                    return NotFound();
                }

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-by-id/{id}")]
        [Authorize]
        public async Task<ActionResult<TaskGetDto>> GetById(int id)
        {
            try
            {
                var task = await _getByIdTask.GetByIdAsync(id);
                if (task == null)
                {
                    return NotFound();
                }
                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _deleteTask.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<ActionResult> Update(int id, [FromBody] TaskUpdateDto task)
        {
            try
            {
                await _updateTask.Update(id, task);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("change-status/{id}")]
        [Authorize]
        public async Task<ActionResult> ChangeStatus(int id, [FromBody] int status)
        {
            try
            {
                await _changeStatusTask.ChangeStatus(id, status);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
