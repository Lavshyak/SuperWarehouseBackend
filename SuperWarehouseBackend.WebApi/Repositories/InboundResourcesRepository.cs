using Microsoft.EntityFrameworkCore;
using SuperWarehouseBackend.WebApi.Db;
using SuperWarehouseBackend.WebApi.Dtos;
using InboundResource = SuperWarehouseBackend.WebApi.Db.Entities.InboundResource;

namespace SuperWarehouseBackend.WebApi.Repositories;

public class InboundResourcesRepository
{
    private readonly MainDbContext _dbContext;

    public InboundResourcesRepository(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /*public async Task<bool> TryAdd(InboundResourceInput inboundResource)
    {
        if (_dbContext.InboundResources.Any(x => x. == measureUnit.Name))
        {
            return false;
        }
        _dbContext.Add(measureUnit);
        await _dbContext.SaveChangesAsync();
        return true;
    }*/
    
    public async Task<InboundResource[]> Get()
    {
        var result = await _dbContext.InboundResources.ToArrayAsync();
        return result;
    }
}