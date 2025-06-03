using Bussiness.Models;

namespace Bussiness.Services;

public interface IEventService
{
    Task<EventResult> CreateEventAsync(CreateEventRequest request);
    Task<EventResult<IEnumerable<Event>>> GetEventsAsync();
    Task<EventResult<Event?>> GetEventAsync(string eventId);

    Task<EventResult> UpdateEventAsync(string id, UpdateEventRequest request);
    Task<EventResult> DeleteEventAsync(string id);

}

