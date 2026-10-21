using System.Collections.Concurrent;
using Todo_App.Server.Models;

namespace Todo_App.Server.Repositories
{
    public class InMemoryTodoRepository : ITodoRepository
    {
        // Using ConcurrentDictionary for concurrent read/writes
        private readonly ConcurrentDictionary<Guid, TodoItem> _items = new();

        public IList<TodoItem> GetAll(string ownerId) =>
            _items.Values.Where(i => i.OwnerId == ownerId).OrderBy(i => i.CreatedAt).ToList();

        public TodoItem Add(string ownerId, string title)
        {
            var item = new TodoItem
            {
                Id = Guid.NewGuid(),
                Title = title,
                OwnerId = ownerId,
                CreatedAt = DateTime.UtcNow,
            };

            _items[item.Id] = item;
            return item;
        }

        public bool Delete(string ownerId, Guid id)
        {
            if (!_items.TryGetValue(id, out var item) || item.OwnerId != ownerId)
            {
                return false;
            }

            return _items.TryRemove(id, out _);
        }
    }
}
