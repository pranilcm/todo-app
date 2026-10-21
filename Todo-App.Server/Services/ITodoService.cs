using Todo_App.Server.Models;

namespace Todo_App.Server.Services
{
    public interface ITodoService
    {
        IReadOnlyCollection<TodoResponse> GetAll(string ownerId);

        TodoResponse Create(string ownerId, string title);

        bool Delete(string ownerId, Guid id);
    }
}
