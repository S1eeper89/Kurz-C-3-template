using System;
using ToDoList.Domain.Models;

namespace ToDoList.Domain.DTOs;

public class ToDoItemUpdateRequestDto(string Name, string Description, bool IsCompleted)
{
    public ToDoItem ToDomain(int id) =>
        new() { Id = id, Name = Name, Description = Description, IsCompleted = IsCompleted };
}
