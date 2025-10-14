using System;

namespace ToDoList.Domain.DTOs;

public record ToDoItemCreateRequestDto(string name, string Description, bool IsCompleted)
{

}
