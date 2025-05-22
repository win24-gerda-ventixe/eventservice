using Bussiness.Models;

namespace Bussiness.Services;

public interface IEventService
{
    Task<EventResult> CreateEventAsync(CreateEventRequest request);
    Task<EventResult<IEnumerable<Event>>> GetEventsAsync();
    Task<EventResult<Event?>> GetEventAsync(string eventId);
}

//public interface IEventService
//{
//    Task<IEnumerable<EventEntity>> GetAllAsync();
//    Task<EventEntity?> GetAsync(string eventId);
//    Task<EventEntity> CreateAsync(EventEntity eventEntity);
//    Task<bool> UpdateAsync(EventEntity eventEntity);
//    Task<bool> DeleteAsync(string eventId);
//}
