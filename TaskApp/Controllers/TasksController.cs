using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskApp.Models;
using TaskApp.Services;

namespace TaskApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            var tasks = await _service.GetTasksAsync();
            return Ok(tasks);
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTask(int id)
        {
            var task = await _service.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        } 

        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult> PostTask([FromBody] TaskItem task)
        {
            // Suponiendo que _service.GetMaxIdAsync() te devuelve el último id
            var lastId = await _service.GetMaxIdAsync();
            task.Id = lastId + 1;
            await _service.CreateTaskAsync(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, new
            {
                message = $"Tarea creada correctamente con el Id: {task.Id}",
                data = task
            });

        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutTask(int id, [FromBody] TaskItem task)
        {
            if (id != task.Id)
                return BadRequest("El ID no coincide.");
            // Busca si la tarea existe antes de actualizar
            var existingTask = await _service.GetTaskByIdAsync(id);

            // Si no existe, devuelve 404 NotFound
            if (existingTask == null)
                return NotFound($"No se encontró la tarea con el ID {id}.");
            // Actualizar propiedades directamente en la instancia ya rastreada
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.IsCompleted = task.IsCompleted;

            await _service.UpdateTaskAsync(existingTask);

            return Ok(new
            {
                message = $"Tarea con ID {id} actualizada correctamente.",
                data = existingTask
            });
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            await _service.DeleteTaskAsync(id);
              return Ok(new
            {
                message = $"Borrado Exitosamente",
                data = ""
            });
        }
    }
}
