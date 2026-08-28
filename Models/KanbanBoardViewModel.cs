namespace MetalBandName.Models;

public class KanbanBoardViewModel
{
    public required IReadOnlyList<TodoItem> Backlog { get; init; }
    public required IReadOnlyList<TodoItem> InProgress { get; init; }
    public required IReadOnlyList<TodoItem> Done { get; init; }
}
