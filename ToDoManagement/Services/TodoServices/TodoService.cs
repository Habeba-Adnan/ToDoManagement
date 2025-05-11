using ToDoManagement.Models;
using ToDoManagement.Repositories.TodoRepositories;
using ToDoManagement.Repositories;
using ToDoManagement.ViewModels;
using ToDoManagement.Enums;
using Serilog;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ToDoManagement.Services.TodoServices
{
    public class TodoService : Service<Todo>, ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public void AddTodo(AddTodoViewModel model)
        {
            try
            {
                Log.Information("Started adding a new Todo. Title: {Title}, Priority: {Priority}", model.Title, model.Priority);

                var todo = new Todo
                {
                    Title = model.Title,
                    Description = model.Description,
                    Priority = model.Priority,
                    DueDate = model.DueDate,
                    Status = TodoStatus.Pending,
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = null
                };

                _todoRepository.Insert(todo);
                _todoRepository.Save();

                Log.Information("Todo added successfully. Title: {Title}, Priority: {Priority}, DueDate: {DueDate}", model.Title, model.Priority, model.DueDate);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while adding a new Todo. Title: {Title}, Priority: {Priority}, DueDate: {DueDate}", model.Title, model.Priority, model.DueDate);
                throw;
            }
        }


        public List<GetAllTodos> GetAllTodos()
        {
            try
            {
                Log.Information("Started fetching all todos.");

                var todos = _todoRepository.GetAll();

                var result = todos.Select(todo => new GetAllTodos
                {
                    Title = todo.Title,
                    Status = todo.Status,
                    CreatedDate = todo.CreatedDate
                }).ToList();

                Log.Information("Successfully fetched all todos. Total todos: {Count}", result.Count);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching all todos.");
                throw;
            }
        }



        public TodoViewModel GetFilteredTodos(string status, string priority, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var todos = _todoRepository.GetFilteredTodos(status, priority, startDate, endDate);

                var statusOptions = Enum.GetValues(typeof(ToDoManagement.Enums.TodoStatus)).Cast<ToDoManagement.Enums.TodoStatus>().Select(s => new SelectListItem
                {
                    Text = s.ToString(),
                    Value = s.ToString(),
                    Selected = s.ToString() == status
                }).ToList();

                var priorityOptions = Enum.GetValues(typeof(ToDoManagement.Enums.TodoPriority)).Cast<ToDoManagement.Enums.TodoPriority>().Select(p => new SelectListItem
                {
                    Text = p.ToString(),
                    Value = p.ToString(),
                    Selected = p.ToString() == priority
                }).ToList();

                var model = new TodoViewModel
                {
                    
                    SelectedStatus = status,
                    SelectedPriority = priority,
                    StartDate = startDate,
                    EndDate = endDate,
                    StatusOptions = statusOptions,
                    PriorityOptions = priorityOptions,
                    Todos = todos.Select(todo => new TodoViewModelItem
                    {
                        Id = todo.Id,
                        Title = todo.Title,
                        Status = todo.Status,
                        CreatedDate = todo.CreatedDate,
                        Priority = todo.Priority
                    }).ToList()
                };

                Log.Information("Successfully fetched filtered todos. Total todos: {Count}", model.Todos.Count);
                return model;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching filtered todos.");
                throw;
            }
        }
        public TodoDetailsViewModel GetTodoDetails(Guid id)
        {
            try
            {
                Log.Information("Fetching Todo details for ID: {TodoId}", id);

                var todo = _todoRepository.Get(i => i.Id == id);
                if (todo == null)
                {
                    Log.Warning("Todo not found for ID: {TodoId}", id);
                    return null;
                }

                var model = new TodoDetailsViewModel
                {
                    Id = id,
                    Title = todo.Title,
                    Description = todo.Description,
                    Status = todo.Status,
                    Priority = todo.Priority,
                    DueDate = todo.DueDate,
                    CreatedDate = todo.CreatedDate,
                    LastModifiedDate = todo.LastModifiedDate
                };

                Log.Information("Successfully fetched Todo details for ID: {TodoId}", id);
                return model;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching Todo details for ID: {TodoId}", id);
                throw;  
            }
        }

        public void DeleteTodo(Guid id)
        {
            try
            {
                Log.Information("Attempting to delete Todo with ID: {TodoId}", id);

                var todo = _todoRepository.Get(i => i.Id == id);
                if (todo != null)
                {
                    _todoRepository.Delete(todo);
                    _todoRepository.Save();

                    Log.Information("Successfully deleted Todo with ID: {TodoId}", id);
                }
                else
                {
                    Log.Warning("Todo with ID: {TodoId} not found for deletion.", id);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while attempting to delete Todo with ID: {TodoId}", id);
                throw; 
            }
        }

        public Todo GetTodoById(Guid id)
        {
            try
            {
                Log.Information("Fetching Todo with ID: {TodoId}.", id);

                var todo = _todoRepository.Get(t => t.Id == id);

                if (todo == null)
                {
                    Log.Warning("Todo with ID: {TodoId} not found.", id);
                }

                return todo;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching Todo with ID: {TodoId}.", id);
                throw;
            }
        }

        public void UpdateTodo(Todo todo)
        {
            try
            {
                Log.Information("Started updating Todo with ID: {TodoId}.", todo.Id);

                _todoRepository.Update(todo);
                _todoRepository.Save();

                Log.Information("Successfully updated Todo with ID: {TodoId}.", todo.Id);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating Todo with ID: {TodoId}.", todo.Id);
                throw;
            }
        }


    }
}
