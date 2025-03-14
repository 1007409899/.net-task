using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskApp.Data;
using TaskApp.Models;

namespace TaskApp.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task AddAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskItem task)
        {
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var task = await GetByIdAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<int> GetMaxIdAsync()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return tasks.Any() ? tasks.Max(t => t.Id) : 0;
        }
      
    }
}
