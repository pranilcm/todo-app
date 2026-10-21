namespace Todo_App.Server.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string OwnerId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
