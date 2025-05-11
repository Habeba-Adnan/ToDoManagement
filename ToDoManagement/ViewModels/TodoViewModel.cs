using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToDoManagement.ViewModels
{
    public class TodoViewModel
    {
        public string SelectedStatus { get; set; }
        public string SelectedPriority { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<TodoViewModelItem> Todos { get; set; }
        public List<SelectListItem> StatusOptions { get; set; }
        public List<SelectListItem> PriorityOptions { get; set; }

       
       
    }
}
