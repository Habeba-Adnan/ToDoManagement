using ToDoManagement.Models;
using ToDoManagement.Repositories;
using ToDoManagement.ViewModels;

namespace ToDoManagement.Services.TodoServices
{
    public interface ITodoService : IService<Todo>
    {
        public void AddTodo(AddTodoViewModel model);
        public List<GetAllTodos> GetAllTodos();
        public TodoViewModel GetFilteredTodos(string status, string priority, DateTime? startDate, DateTime? endDate);
        public TodoDetailsViewModel GetTodoDetails(Guid id);
        public void DeleteTodo(Guid id);
        public Todo GetTodoById(Guid id);
        public void UpdateTodo(Todo todo);

    }
}
