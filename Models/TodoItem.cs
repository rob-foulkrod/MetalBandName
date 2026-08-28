using System.ComponentModel.DataAnnotations;

namespace MetalBandName.Models;

public class TodoItem
{
    public int Id { get; init; }

    [Required, StringLength(120)]
    public required string Title { get; init; }

    public TodoStatus Status { get; set; }
}
