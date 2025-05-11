using System.Numerics;
using ToDoManagement.Models;

namespace ToDoManagement.Repositories.TodoRepositories
{
    public interface ITodoRepository : IRepository<Todo>
    {
        public List<Todo> GetAllTodos();
        public List<Todo> GetFilteredTodos(string status, string priority, DateTime? startDate, DateTime? endDate);

    }
}
