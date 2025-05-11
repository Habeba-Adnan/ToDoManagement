using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ToDoManagement.Enums;
using ToDoManagement.Validation;

namespace ToDoManagement.ViewModels
{
    public class AddTodoViewModel
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        [Required(ErrorMessage = "Priority is required.")]
        public TodoPriority Priority { get; set; }
        [ValidDate]
        public DateTime? DueDate { get; set; }

    }
}
