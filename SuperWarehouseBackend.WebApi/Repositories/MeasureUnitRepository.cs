using Microsoft.EntityFrameworkCore;
using SuperWarehouseBackend.WebApi.Db;
using SuperWarehouseBackend.WebApi.Dtos;
using SuperWarehouseBackend.WebApi.General;
using MeasureUnit = SuperWarehouseBackend.WebApi.Db.Entities.MeasureUnit;

namespace SuperWarehouseBackend.WebApi.Repositories;

public class MeasureUnitRepository
{
    private readonly MainDbContext _dbContext;

    public MeasureUnitRepository(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public enum MeasureUnitAddError
    {
        InvalidName,
        NameAlreadyExists,
    }
    public async Task<ResultOrError<MeasureUnit, MeasureUnitAddError>> Add(MeasureUnitDtoAdd measureUnitDtoAdd)
    {
        if (!ValidateMeasureUnitName(measureUnitDtoAdd.Name))
            return ResultOrError<MeasureUnit, MeasureUnitAddError>.FromError(MeasureUnitAddError.InvalidName);
        
        if (_dbContext.MeasureUnits.Any(x => x.Name == measureUnitDtoAdd.Name))
        {
            return ResultOrError<MeasureUnit, MeasureUnitAddError>.FromError(MeasureUnitAddError.NameAlreadyExists);
        }

        var measureUnit = new MeasureUnit()
        {
            Name = measureUnitDtoAdd.Name,
            IsArchived = measureUnitDtoAdd.IsArchived
        };
        
        _dbContext.Add(measureUnit);
        await _dbContext.SaveChangesAsync();
        return ResultOrError<MeasureUnit, MeasureUnitAddError>.FromResult(measureUnit);
    }

    public async Task<bool> Update(MeasureUnitDtoUpdate measureUnitDtoUpdate)
    {
        if (!ValidateMeasureUnitName(measureUnitDtoUpdate.Name))
            return false;
        
        var measureUnit = _dbContext.MeasureUnits.FirstOrDefault(x => x.Guid == measureUnitDtoUpdate.Guid);
        if (measureUnit is null)
        {
            return false;
        }
        
        measureUnit.Name = measureUnitDtoUpdate.Name;
        measureUnit.IsArchived = measureUnitDtoUpdate.IsArchived;
        
        await _dbContext.SaveChangesAsync();
        return true;
    }

    private bool ValidateMeasureUnitName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > MeasureUnit.NameMaxLength)
            return false;
        return true;
    }
    
    public async Task<bool> Delete(MeasureUnitDtoDelete measureUnitDtoDelete)
    {
        var deletedCount = await _dbContext.MeasureUnits.Where(mu => mu.Guid == measureUnitDtoDelete.Guid).ExecuteDeleteAsync();
        
        if(deletedCount == 1)
            return true;
        else if (deletedCount == 0)
            return false;
        else
            // TODO: log critical
            throw new InvalidOperationException("something went very bad");
    }
    
    public async Task<MeasureUnit[]> GetAll(bool isArchived = false)
    {
        if (isArchived)
        {
            var result = await _dbContext.MeasureUnits.Where(mu => isArchived).ToArrayAsync();
            return result;
        }
        else
        {
            var result = await _dbContext.MeasureUnits.Where(mu => !isArchived).ToArrayAsync();
            return result;
        }
    }
}