using Todo_App.Server.Models;

namespace Todo_App.Server.Repositories
{
    public interface ITodoRepository
    {
        IList<TodoItem> GetAll(string ownerId);

        TodoItem Add(string ownerId, string title);

        bool Delete(string ownerId, Guid id);
    }
}
