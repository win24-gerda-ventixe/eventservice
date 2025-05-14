using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Services;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]

public class EventsController(EventService eventService) : ControllerBase
{
    private readonly EventService _eventService = eventService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _eventService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetAll(string Id)
    {
        var result = await _eventService.GetAsync(Id);

        return result != null ? Ok(result) : NotFound();

    }
}
