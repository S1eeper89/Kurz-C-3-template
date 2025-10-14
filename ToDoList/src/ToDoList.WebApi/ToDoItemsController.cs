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
        //map to Domain object as soon as possible
        var item = request.ToDomain();

        //try to create an item
        try
        {
            item.ToDoItemId = items.Count == 0 ? 1 : items.Max(o => o.ToDoItemId) + 1;
            items.Add(item);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return Created(); //201 //tato metoda z nějakého důvodu vrací status code No Content 204, zjištujeme proč ;)
    }
    [HttpGet]
    public IActionResult Read() //api/ToDoItems GET
    {
        return Ok();
    }
    [HttpGet("{toDoItemId:int}")]
    public IActionResult ReadById(int toDoItemId) //api/ToDoItems
    {
        try
        {
            var item = items.Find(x => x.ToDoItemId == toDoItemId);

            if (item == null)
                return NotFound(); //404

            var result = ToDoItemGetResponseDto.FromDomain(item);
            return Ok(result); //200
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

    }
    [HttpPut]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        try
        {
            var index = items.FindIndex(x => x.ToDoItemId == toDoItemId);
            if (index == -1)
                return NotFound(); //404

            var updatedItem = request.ToDomain(toDoItemId);
            items[index] = updatedItem;

            return NoContent(); //204
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }
    [HttpDelete]
    public IActionResult DeleteById(int toDoItemId)
    {
        try
        {
            var item = items.Find(x => x.ToDoItemId == toDoItemId);
            if (item == null)
                return NotFound(); //404

            items.Remove(item);
            return NoContent(); //204
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }
}
