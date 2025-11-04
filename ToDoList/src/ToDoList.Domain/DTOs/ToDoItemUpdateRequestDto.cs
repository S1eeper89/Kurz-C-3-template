using System;
using ToDoList.Domain.Models;

namespace ToDoList.Domain.DTOs;

using ToDoList.Domain.Models;

public record ToDoItemUpdateRequestDto(string Name, string Description, bool IsCompleted)
{
    public ToDoItem ToDomain(int toDoItemId) => new() { ToDoItemId = toDoItemId, Name = Name, Description = Description, IsCompleted = IsCompleted };
}
