using MetalBandName.Models;

namespace MetalBandName.Services;

public interface ITodoStore
{
    IReadOnlyList<TodoItem> GetAll();
    void Add(string title);
    bool Move(int id, TodoStatus status);
}
