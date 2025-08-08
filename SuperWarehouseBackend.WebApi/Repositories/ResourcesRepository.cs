using Microsoft.EntityFrameworkCore;
using SuperWarehouseBackend.WebApi.Db;
using SuperWarehouseBackend.WebApi.Db.Entities;
using SuperWarehouseBackend.WebApi.Dtos;

namespace SuperWarehouseBackend.WebApi.Repositories;

public class ResourcesRepository
{
    private readonly MainDbContext _dbContext;

    public ResourcesRepository(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public enum AddResult
    {
        Ok,
        InvalidName,
        NameAlreadyExists,
    }
    public async Task<AddResult> Add(ResourceDtoAdd resourceDtoAdd)
    {
        if (string.IsNullOrWhiteSpace(resourceDtoAdd.Name) || resourceDtoAdd.Name.Length > Resource.NameMaxLength)
        {
            return AddResult.InvalidName;
        }
        
        if (_dbContext.Resources.Any(x => x.Name == resourceDtoAdd.Name))
        {
            return AddResult.NameAlreadyExists;
        }
        
        var resource = new Resource()
        {
            Name = resourceDtoAdd.Name,
        };
        _dbContext.Resources.Add(resource);
        await _dbContext.SaveChangesAsync();
        return AddResult.Ok;
    }
    
    public async Task<bool> Update(ResourceDtoUpdate resourceDtoUpdate)
    {
        var resource = await _dbContext.Resources.FirstOrDefaultAsync(r => r.Guid == resourceDtoUpdate.Guid);

        if (resource is null)
            return false;
        
        if (resource.Name != resourceDtoUpdate.Name)
        {
            if (!ValidateName(resourceDtoUpdate.Name))
                return false;
            
            resource.Name = resourceDtoUpdate.Name;
        }
        
        resource.IsArchived = resourceDtoUpdate.IsArchived;
        
        await _dbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> Delete(ResourceDtoDelete resourceDtoDelete)
    {
        // All entities reference Resource as part of primary key
        var deletedCount = await _dbContext.Resources.Where(r => r.Guid == resourceDtoDelete.Guid).ExecuteDeleteAsync();
        if(deletedCount == 1)
            return true;
        else if (deletedCount == 0)
            return false;
        else
            // TODO: log critical
            throw new InvalidOperationException("something went very bad");
    }

    private bool ValidateName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Resource.NameMaxLength)
        {
            return false;
        }

        return true;
    }
    
    public async Task<IEnumerable<Resource>> GetAllNotArchived()
    {
        var result = await _dbContext.Resources.AsNoTracking().Where(r=>!r.IsArchived).ToArrayAsync();
        return result;
    }
}