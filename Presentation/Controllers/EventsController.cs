using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bussiness.Services;
using Bussiness.Models;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]

public class EventsController(IEventService eventService) : ControllerBase
{
    private readonly IEventService _eventService = eventService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var events = await _eventService.GetEventsAsync();
        return Ok(events);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> Get(string Id)
    {
        var currentEvent = await _eventService.GetEventAsync(Id);

        return currentEvent != null ? Ok(currentEvent) : NotFound();

    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEventRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _eventService.CreateEventAsync(request);
        return result.Success ? Ok() : StatusCode(500, result.Error);

    }
}
