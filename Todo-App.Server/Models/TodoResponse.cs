namespace Todo_App.Server.Models
{
    public sealed record TodoResponse(Guid Id, string Title, DateTime CreatedAt)
    {
        public static TodoResponse FromModel(TodoItem item) =>
            new(item.Id, item.Title, item.CreatedAt);
    }
}
