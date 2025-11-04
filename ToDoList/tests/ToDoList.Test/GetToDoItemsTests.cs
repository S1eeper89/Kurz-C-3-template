namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;

public class GetToDoItemsTests
{
    [Fact]
    public void GetExistingToDoItemsReturnsOk()
    {
        // Arrange
        var controller = new ToDoItemsController();
        var toDoItemId = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        controller.AddItemToStorage(toDoItemId);

        // Act
        var result = controller.ReadById(toDoItemId.ToDoItemId);

        //Assert
        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        var returnedItem = okResult.Value as ToDoItemGetResponseDto;
        Assert.NotNull(returnedItem);
        Assert.Equal(toDoItemId.ToDoItemId, returnedItem!.Id);
        Assert.Equal(toDoItemId.Name, returnedItem.Name);
        Assert.Equal(toDoItemId.Description, returnedItem.Description);
        Assert.Equal(toDoItemId.IsCompleted, returnedItem.IsCompleted);
    }

    [Fact]
    public void GetNonExistingItemReturnsNotFound()
    {
        // Arrange
        var controller = new ToDoItemsController();
        var toDoItemId = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        ToDoItemsController.Items.Add(toDoItemId);

        // Act
        var result = controller.ReadById(666);

        //Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
