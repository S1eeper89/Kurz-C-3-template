namespace ToDoList.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
[Route("api/[controller]")] //localhost:5000/api/ToDoItems
[ApiController]
public class ToDoItemsController : ControllerBase
{
    public static List<ToDoItem> Items = [];
    public object items;

    [HttpPost]
    public IActionResult Create(ToDoItemCreateRequestDto request)
    {
        //map to Domain object as soon as possible
        var item = request.ToDomain();

        //try to create an item
        try
        {
            item.ToDoItemId = Items.Count == 0 ? 1 : Items.Max(o => o.ToDoItemId) + 1;
            Items.Add(item);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return Created(); //201 //tato metoda z nějakého důvodu vrací status code No Content 204, zjištujeme proč ;)
    }

    //     public IActionResult Read() //api/ToDoItems GET
    //     {
    //         try
    //         {
    //             // Ověříme, jestli seznam není prázdný
    //             if (items == null || !items.Any())
    //                 return NotFound(); // 404 pokud žádné úkoly nejsou

    //             // Převedeme seznam ToDoItem na DTO
    //             var result = items
    //                 .Select(ToDoItemGetResponseDto.FromDomain)
    //                 .ToList();

    //             return Ok(result); // 200 OK s daty
    //         }
    //     catch (Exception ex)
    //     {
    //         // 500 Internal Server Error při výjimce
    //         return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
    //     }
    // }
    [HttpGet]
    public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()
    {
        List<ToDoItem> itemsToGet;
        try
        {
            itemsToGet = Items;
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return (itemsToGet is null)
            ? NotFound() //404
            : Ok(itemsToGet.Select(ToDoItemGetResponseDto.FromDomain)); //200
    }

    [HttpGet("{toDoItemId:int}")]
    public IActionResult ReadById(int toDoItemId) //api/ToDoItems
    {
        try
        {
            var item = Items.Find(x => x.ToDoItemId == toDoItemId);

            if (item == null)
            {
                return NotFound(); //404
            }

            var result = ToDoItemGetResponseDto.FromDomain(item);
            return Ok(result); //200
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

    }
    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        //map to Domain object as soon as possible
        var updatedItem = request.ToDomain(toDoItemId);

        //try to update the item by retrieving it with given id
        try
        {
            //retrieve the item
            var itemIndexToUpdate = Items.FindIndex(i => i.ToDoItemId == toDoItemId);
            if (itemIndexToUpdate == -1)
            {
                return NotFound(); //404
            }
            updatedItem.ToDoItemId = toDoItemId;
            Items[itemIndexToUpdate] = updatedItem;
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return NoContent(); //204
    }
    [HttpDelete]
    public IActionResult DeleteById(int toDoItemId)
    {
        try
        {
            var item = Items.Find(x => x.ToDoItemId == toDoItemId);
            if (item == null)
            {
                return NotFound(); //404
            }

            Items.Remove(item);
            return NoContent(); //204
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    public void AddItemToStorage(ToDoItem item) => Items.Add(item);
}


// namespace ToDoList.WebApi.Controllers;

// using Microsoft.AspNetCore.Mvc;
// using ToDoList.Domain.DTOs;
// using ToDoList.Domain.Models;
// using ToDoList.Persistence;

// [Route("api/[controller]")] //localhost:5000/api/ToDoItems
// [ApiController]
// public class ToDoItemsController : ControllerBase
// {
//     public readonly List<ToDoItem> items = []; // po dopsání úkolu již není potřeba a můžeme smazat
//     private readonly ToDoItemsContext context;

//     public ToDoItemsController(ToDoItemsContext context)
//     {
//         this.context = context;
//     }

//     [HttpPost]
//     public ActionResult<ToDoItemGetResponseDto> Create(ToDoItemCreateRequestDto request)
//     {
//         //map to Domain object as soon as possible
//         var item = request.ToDomain();

//         //try to create an item
//         try
//         {
//             // item.ToDoItemId = items.Count == 0 ? 1 : items.Max(o => o.ToDoItemId) + 1;
//             // items.Add(item);
//             context.ToDoItems.Add(item);
//             context.SaveChanges();
//         }
//         catch (Exception ex)
//         {
//             return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
//         }

//         //respond to client
//         return CreatedAtAction(
//             nameof(ReadById),
//             new { toDoItemId = item.ToDoItemId },
//             ToDoItemGetResponseDto.FromDomain(item)); //201
//     }

//     [HttpGet]
//     public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()
//     {
//         List<ToDoItem> itemsToGet;
//         try
//         {
//             itemsToGet = items;
//         }
//         catch (Exception ex)
//         {
//             return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
//         }

//         //respond to client
//         return (itemsToGet is null)
//             ? NotFound() //404
//             : Ok(itemsToGet.Select(ToDoItemGetResponseDto.FromDomain)); //200
//     }

//     [HttpGet("{toDoItemId:int}")]
//     public ActionResult<ToDoItemGetResponseDto> ReadById(int toDoItemId)
//     {
//         //try to retrieve the item by id
//         ToDoItem? itemToGet;
//         try
//         {
//             itemToGet = items.Find(i => i.ToDoItemId == toDoItemId);
//         }
//         catch (Exception ex)
//         {
//             return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
//         }

//         //respond to client
//         return (itemToGet is null)
//             ? NotFound() //404
//             : Ok(ToDoItemGetResponseDto.FromDomain(itemToGet)); //200
//     }

//     [HttpPut("{toDoItemId:int}")]
//     public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
//     {
//         //map to Domain object as soon as possible
//         var updatedItem = request.ToDomain();

//         //try to update the item by retrieving it with given id
//         try
//         {
//             //retrieve the item
//             var itemIndexToUpdate = items.FindIndex(i => i.ToDoItemId == toDoItemId);
//             if (itemIndexToUpdate == -1)
//             {
//                 return NotFound(); //404
//             }
//             updatedItem.ToDoItemId = toDoItemId;
//             items[itemIndexToUpdate] = updatedItem;
//         }
//         catch (Exception ex)
//         {
//             return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
//         }

//         //respond to client
//         return NoContent(); //204
//     }

//     [HttpDelete("{toDoItemId:int}")]
//     public IActionResult DeleteById(int toDoItemId)
//     {
//         //try to delete the item
//         try
//         {
//             var itemToDelete = items.Find(i => i.ToDoItemId == toDoItemId);
//             if (itemToDelete is null)
//             {
//                 return NotFound(); //404
//             }
//             items.Remove(itemToDelete);
//         }
//         catch (Exception ex)
//         {
//             return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
//         }

//         //respond to client
//         return NoContent(); //204
//     }

//     public void AddItemToStorage(ToDoItem item)
//     {
//         items.Add(item);
//     }
// }
