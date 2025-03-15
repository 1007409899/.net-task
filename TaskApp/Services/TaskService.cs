using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp.Enums;
using TaskApp.Models;
using TaskApp.Repositories;

namespace TaskApp.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskItem>> GetTasksAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateTaskAsync(TaskItem task)
        {
            await _repository.AddAsync(task);
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            await _repository.UpdateAsync(task);
        }

        public async Task DeleteTaskAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<int> GetMaxIdAsync() => await _repository.GetMaxIdAsync();

        public async Task<IEnumerable<TaskItem>> GetTasksByStatus(StateTask state) => await _repository.GetTasksByStatus(state);

    }
}
