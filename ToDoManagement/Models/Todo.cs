using ToDoManagement.Enums;

namespace ToDoManagement.Models
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public TodoStatus Status { get; set; }
        public TodoPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

       
    }

   

    
}
