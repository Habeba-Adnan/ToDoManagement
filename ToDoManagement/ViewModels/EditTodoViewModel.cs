using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ToDoManagement.Enums;

namespace ToDoManagement.ViewModels
{
    public class EditTodoViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]

        public string Title { get; set; }
        public string? Description { get; set; }


        [JsonIgnore]
        [Required(ErrorMessage = "Priority is required.")]
        public TodoPriority Priority { get; set; }
        //[NotMapped]
        //public string PriorityString { get; set; }

        public DateTime? DueDate { get; set; }

    }
}
