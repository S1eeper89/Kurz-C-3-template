using System;

namespace ToDoList.Domain.Models;

public class ToDoItem
{
    public int ToDoItemID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }

}
