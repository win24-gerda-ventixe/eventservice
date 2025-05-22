using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Infrastructure.Repositories;

public class EventRepository(DataContext context) : BaseRepository<EventEntity>(context), IEventRepository
{
    public override async Task<RepositoryResult<IEnumerable<EventEntity>>> GetAllAsync()
    {
        try
        {
            var entities = await _table.Include(x => x.Packages).ToListAsync();
            return new RepositoryResult<IEnumerable<EventEntity>>
            {
                Success = true,
                Result = entities
            };
        }

        catch (Exception ex)
        {
            return new RepositoryResult<IEnumerable<EventEntity>>
            {
                Success = false,
                Error = ex.Message,
            };
        }
    }

    public override async Task<RepositoryResult<EventEntity?>> GetAsync(Expression<Func<EventEntity, bool>> expression)
    {
        try
        {
            var entity = await _table.Include(x => x.Packages).FirstOrDefaultAsync(expression) ?? throw new Exception("Entity not found");
            return new RepositoryResult<EventEntity?>
            {
                Success = true,
                Result = entity,
            };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<EventEntity?>
            {
                Success = false,
                Error = ex.Message,
            };
        }
    }
    public async Task<RepositoryResult<EventEntity?>> GetAsync(string id)
    {
        return await GetAsync(e => e.Id == id);
    }

    //public async Task<RepositoryResult<IEnumerable<EventEntity>>> GetByStatusAsync(string status)
    //{
    //    try
    //    {
    //        var events = await _table
    //            .Include(x => x.Packages)
    //            .Where(e => e.Status == status)
    //            .ToListAsync();

    //        return new RepositoryResult<IEnumerable<EventEntity>>
    //        {
    //            Success = true,
    //            Result = events
    //        };
    //    }
    //    catch (Exception ex)
    //    {
    //        return new RepositoryResult<IEnumerable<EventEntity>>
    //        {
    //            Success = false,
    //            Error = ex.Message
    //        };
    //    }
    //}

}
