namespace ToDoList.WebApi;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
[Route("api/[controller]")] //localhost:5000/api/ToDoItems
[ApiController]
public class ToDoItemsController : ControllerBase
{
    private static List<ToDoItem> items = [];
    [HttpPost]
    public IActionResult Create(ToDoItemCreateRequestDto request)
    {
        return Ok();
    }
    [HttpGet]
    public IActionResult Read() //api/ToDoItems GET
    {
        return Ok();
    }
    [HttpGet("{toDoItemId:int}")]
    public IActionResult ReadById(int toDoItemId) //api/ToDoItems
    {
        return Ok();

    }
    [HttpPut]
    public IActionResult UpdateById(int toDoItemId,[FromBody] ToDoItemUpdateRequestDto request)
    {
        try
        {
            throw new Exception("Něco se opravdu nepovedo.");
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }
        return Ok();
    }
    [HttpDelete]
    public IActionResult DeleteById(int toDoItemId)
    {
    return Ok();
    }
}
