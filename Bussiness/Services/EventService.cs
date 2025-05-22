using Bussiness.Models;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace Bussiness.Services;

public class EventService(IEventRepository eventRepository) : IEventService
{
    private readonly IEventRepository _eventRepository = eventRepository;

    public async Task<EventResult> CreateEventAsync(CreateEventRequest request)
    {
        try
        {
            var eventEntity = new EventEntity
            {
                Id = Guid.NewGuid().ToString(),
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                Price = request.Price,
                EventDate = request.EventDate,
                Time = request.Time,
                Image = request.Image,
                Category = request.Category,
                Status = request.Status
            };

            var result = await _eventRepository.AddAsync(eventEntity);
            return result.Success 
                ? new EventResult { Success = true }
                : new EventResult{ Success = false, Error = result.Error };
        }
        catch (Exception ex)
        {
            return new EventResult
            {
                Success = false,
                Error = ex.Message
            };
        }
    }


    public async Task<EventResult<IEnumerable<Event>>> GetEventsAsync()
    {
        var result = await _eventRepository.GetAllAsync();
        var events = result.Result?.Select(x => new Event
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Location = x.Location,
            Price = x.Price,
            EventDate = x.EventDate,
            Time = x.Time,
            Image = x.Image,
            Category = x.Category,
            Status = x.Status
        });
        return new EventResult<IEnumerable<Event>>
        {
            Success = true,
            Result = events
        };
    }

    public async Task<EventResult<Event?>> GetEventAsync(string eventId)
    {
        var result = await _eventRepository.GetAsync(x => x.Id == eventId);
        if (result.Success && result.Result != null)
                { 
                var currentEvent = new Event
                {
                    Id = result.Result.Id,
                    Title = result.Result.Title,
                    Description = result.Result.Description,
                    Location = result.Result.Location,
                    Price = result.Result.Price,
                    EventDate = result.Result.EventDate,
                    Time = result.Result.Time,
                    Image = result.Result.Image,
                    Category = result.Result.Category,
                    Status = result.Result.Status
                };

                return new EventResult<Event?> { Success = result.Success, Result = currentEvent };
        }
        return new EventResult<Event?> { Success = false, Error = "Event not found" };
    }




    //public async Task<IEnumerable<EventEntity>> GetByStatusAsync(string status)
    //    {
    //        var result = await _repository.GetByStatusAsync(status);
    //        return result.Success && result.Result != null ? result.Result : Enumerable.Empty<EventEntity>();
    //    }


    //    public async Task<bool> UpdateAsync(EventEntity eventEntity)
    //    {
    //        var existing = await _repository.GetAsync(eventEntity.Id);
    //        if (existing == null) return false;

    //        await _repository.UpdateAsync(eventEntity);
    //        return true;
    //    }

    //    public async Task<bool> DeleteAsync(string eventId)
    //    {
    //        var existing = await _repository.GetAsync(eventId);
    //        if (existing == null) return false;

    //        await _repository.DeleteAsync(existing);
    //        return true;
    //    }

    //public async Task<IEnumerable<EventEntity>> GetAllAsync()
    //{
    //    var result = await _repository.GetAllAsync();
    //    return result.Success && result.Result != null ? result.Result : [];
    //}
}
