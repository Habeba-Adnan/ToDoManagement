using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog;
using ToDoManagement.Services.TodoServices;
using ToDoManagement.ViewModels;

namespace ToDoManagement.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }
        public IActionResult Index(string status, string priority, DateTime? startDate, DateTime? endDate)
        {
            var model = _todoService.GetFilteredTodos(status, priority, startDate, endDate);

            return View(model);
        }



        public IActionResult AddTodo()
        {
            try
            {        
                Log.Information("Started adding a new Todo.");

                return View("AddTodo");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while adding a new Todo.");
                return View("Error"); 
            }
        }
        [HttpPost]
        public IActionResult Create(AddTodoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Log.Information("Started adding a new Todo. Title: {Title}, Priority: {Priority}", model.Title, model.Priority);

                    _todoService.AddTodo(model);

                    Log.Information("Todo added successfully. Title: {Title}, Priority: {Priority}, DueDate: {DueDate}", model.Title, model.Priority, model.DueDate);

                    return RedirectToAction("Index", "Todo");
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occurred while adding a new Todo. Title: {Title}, Priority: {Priority}, DueDate: {DueDate}", model.Title, model.Priority, model.DueDate);
                    return View("Error");
                }
            }

            Log.Warning("AddTodo failed. Model state is invalid. Title: {Title}, Priority: {Priority}, DueDate: {DueDate}", model.Title, model.Priority, model.DueDate);
            return View("AddTodo", model);
        }

        public IActionResult Details(Guid id)
        {
            try
            {
                var todo = _todoService.GetTodoDetails(id);
                if (todo == null)
                {
                    return NotFound();
                }
                return View(todo);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching todo details.");
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _todoService.DeleteTodo(id);
                Log.Information("Todo with ID {Id} deleted successfully.", id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while deleting todo with ID {Id}.", id);
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                Log.Information("Started fetching Todo with ID: {TodoId} for editing.", id);

                var todo = _todoService.GetTodoById(id);

                if (todo == null)
                {
                    Log.Warning("Todo with ID: {TodoId} not found.", id);
                    return NotFound();
                }

                var viewModel = new EditTodoViewModel
                {
                    Title = todo.Title,
                    Description = todo.Description,
                    Priority = todo.Priority,
                    DueDate = todo.DueDate
                };

                Log.Information("Successfully fetched Todo with ID: {TodoId} for editing.", id);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching Todo with ID: {TodoId} for editing.", id);
                return View("Error");
            }
        }


        [HttpPost]
        public IActionResult Edit(EditTodoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Log.Information("Started updating Todo with ID: {TodoId}.", model.Id);

                    var todo = _todoService.GetTodoById(model.Id);
                    if (todo == null)
                    {
                        Log.Warning("Todo with ID: {TodoId} not found for editing.", model.Id);
                        return NotFound();
                    }

                    todo.Title = model.Title;
                    todo.Description = model.Description;
                    todo.Priority = model.Priority;
                    todo.DueDate = model.DueDate;
                    todo.LastModifiedDate = DateTime.Now;

                    _todoService.UpdateTodo(todo);

                    Log.Information("Successfully updated Todo with ID: {TodoId}.", model.Id);
                    return RedirectToAction("Index");
                }

                Log.Warning("ModelState is invalid for Todo with ID: {TodoId}.", model.Id);
                return View(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating Todo with ID: {TodoId}.", model.Id);
                return View("Error");
            }
        }

    }
}
