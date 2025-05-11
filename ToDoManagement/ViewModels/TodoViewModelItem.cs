using ToDoManagement.Enums;

namespace ToDoManagement.ViewModels
{
    public class TodoViewModelItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public TodoStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public TodoPriority Priority { get; set; }
    }
}
