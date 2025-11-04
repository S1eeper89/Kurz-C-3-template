namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;



public class DeleteTests
{
    [Fact]
    public void DeleteExistingItemReturnsNoContent()
    {
        // Arrange
        ToDoItemsController.Items.Clear();
        var todoItem1 = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno1",
            Description = "Popis1",
            IsCompleted = false
        };
        var todoItem2 = new ToDoItem
        {
            ToDoItemId = 2,
            Name = "Jmeno2",
            Description = "Popis2",
            IsCompleted = true
        };
        var controller = new ToDoItemsController();
        controller.AddItemToStorage(todoItem1);
        controller.AddItemToStorage(todoItem2);

        // Act
        var result = controller.DeleteById(2);

        // Assert
        Assert.IsType<NoContentResult>(result);
        Assert.Single(ToDoItemsController.Items);
        Assert.Equal(1, ToDoItemsController.Items[0].ToDoItemId);
    }
    [Fact]
    public void DeleteNotEXistingItemReturnsNotFoundAndDoesNotChangeCollection()
    {
        // Arrange
        ToDoItemsController.Items.Clear();
        var todoItem = new ToDoItem
        {
            ToDoItemId = 5,
            Name = "A",
            Description = "B",
            IsCompleted = false
        };
        var controller = new ToDoItemsController();
        controller.AddItemToStorage(todoItem);

        // Act
        var result = controller.DeleteById(999);

        // Assert (stavová odpověď)
        Assert.IsType<NotFoundResult>(result);
        Assert.Single(ToDoItemsController.Items);
        Assert.Equal(5, ToDoItemsController.Items[0].ToDoItemId);
    }
}
