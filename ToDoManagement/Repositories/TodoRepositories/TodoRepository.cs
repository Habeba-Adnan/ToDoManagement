using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Numerics;
using ToDoManagement.Models;

namespace ToDoManagement.Repositories.TodoRepositories
{
    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
        private readonly Context context;
        internal DbSet<Todo> Todos => context.todos;
        public TodoRepository(Context _context) : base(_context)
        {
            this.context = _context;
          
        }
        public List<Todo> GetAllTodos()
        {
            try
            {
                var todos = GetAll();
                Log.Information("Successfully fetched all todos. Total todos: {Count}", todos.Count);
                return todos;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching all todos.");
                throw;
            }
        }


        public List<Todo> GetFilteredTodos(string status, string priority, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var todos = dbSet.AsQueryable();
                todos = FilterTodos(todos, status, priority, startDate, endDate);
                Log.Information("Successfully fetched filtered todos. Total todos: {Count}", todos.Count());
                return todos.ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching filtered todos.");
                throw;
            }
        }

    }
}
