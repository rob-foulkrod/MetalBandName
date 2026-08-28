using MetalBandName.Models;

namespace MetalBandName.Services;

public class InMemoryTodoStore : ITodoStore
{
    private readonly object syncRoot = new();
    private readonly List<TodoItem> items =
    [
        new() { Id = 1, Title = "Shape the opening riff", Status = TodoStatus.Backlog },
        new() { Id = 2, Title = "Book the rehearsal room", Status = TodoStatus.InProgress },
        new() { Id = 3, Title = "Name the new single", Status = TodoStatus.Done }
    ];
    private int nextId = 4;

    public IReadOnlyList<TodoItem> GetAll()
    {
        lock (syncRoot)
        {
            return items.Select(item => new TodoItem
            {
                Id = item.Id,
                Title = item.Title,
                Status = item.Status
            }).ToList();
        }
    }

    public void Add(string title)
    {
        lock (syncRoot)
        {
            items.Add(new TodoItem { Id = nextId++, Title = title, Status = TodoStatus.Backlog });
        }
    }

    public bool Move(int id, TodoStatus status)
    {
        lock (syncRoot)
        {
            var item = items.SingleOrDefault(item => item.Id == id);
            if (item is null)
            {
                return false;
            }

            item.Status = status;
            return true;
        }
    }
}
