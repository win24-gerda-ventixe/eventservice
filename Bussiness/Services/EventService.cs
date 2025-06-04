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
            Console.WriteLine("Exception: " + ex);
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

    //public async Task<EventResult> UpdateEventAsync(string id, UpdateEventRequest request)
    //{
    //    var existing = await _eventRepository.GetAsync(e => e.Id == id);
    //    if (!existing.Success || existing.Result == null)
    //    {
    //        return new EventResult { Success = false, Error = "Event not found." };
    //    }

    //    var eventEntity = existing.Result;

    //    eventEntity.Title = request.Title ?? eventEntity.Title;
    //    eventEntity.Description = request.Description ?? eventEntity.Description;
    //    eventEntity.Location = request.Location ?? eventEntity.Location;
    //    eventEntity.Price = request.Price != 0 ? request.Price : eventEntity.Price;
    //    eventEntity.EventDate = request.EventDate != default ? request.EventDate : eventEntity.EventDate;
    //    eventEntity.Time = request.Time != default ? request.Time : eventEntity.Time;
    //    eventEntity.Image = request.Image ?? eventEntity.Image;
    //    eventEntity.Category = request.Category ?? eventEntity.Category;
    //    eventEntity.Status = request.Status ?? eventEntity.Status;

    //    var result = await _eventRepository.UpdateAsync(eventEntity);
    //    return new EventResult { Success = result.Success, Error = result.Error };
    //}
    public async Task<EventResult> UpdateEventAsync(string id, UpdateEventRequest request)
    {
        var existing = await _eventRepository.GetAsync(e => e.Id == id);
        if (!existing.Success || existing.Result == null)
        {
            return new EventResult { Success = false, Error = "Event not found." };
        }

        var eventEntity = existing.Result;

        // Validate incoming date and time
        if (request.EventDate == default || request.EventDate.Year < 2000)
        {
            return new EventResult { Success = false, Error = "Invalid event date format." };
        }

        if (request.Time == default || request.Time.Year < 2000)
        {
            return new EventResult { Success = false, Error = "Invalid time format." };
        }

        // log for debugging
        Console.WriteLine($"[Update] ID: {id}, Date: {request.EventDate}, Time: {request.Time}");

        eventEntity.Title = request.Title ?? eventEntity.Title;
        eventEntity.Description = request.Description ?? eventEntity.Description;
        eventEntity.Location = request.Location ?? eventEntity.Location;
        eventEntity.Price = request.Price != 0 ? request.Price : eventEntity.Price;
        eventEntity.EventDate = request.EventDate;
        eventEntity.Time = request.Time;
        eventEntity.Image = request.Image ?? eventEntity.Image;
        eventEntity.Category = request.Category ?? eventEntity.Category;
        eventEntity.Status = request.Status ?? eventEntity.Status;

        var result = await _eventRepository.UpdateAsync(eventEntity);
        return new EventResult { Success = result.Success, Error = result.Error };
    }

    public async Task<EventResult> DeleteEventAsync(string id)
    {
        var existing = await _eventRepository.GetAsync(e => e.Id == id);
        if (!existing.Success || existing.Result == null)
        {
            return new EventResult { Success = false, Error = "Event not found." };
        }

        var result = await _eventRepository.DeleteAsync(existing.Result);
        return new EventResult { Success = result.Success, Error = result.Error };
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
