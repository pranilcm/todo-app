using Todo_App.Server.Models;
using Todo_App.Server.Repositories;

namespace Todo_App.Server.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public IReadOnlyCollection<TodoResponse> GetAll(string ownerId) =>
            _repository.GetAll(ownerId).Select(TodoResponse.FromModel).ToList();

        public TodoResponse Create(string ownerId, string title) =>
            TodoResponse.FromModel(_repository.Add(ownerId, title.Trim()));

        public bool Delete(string ownerId, Guid id) => _repository.Delete(ownerId, id);
    }
}
