using Microsoft.EntityFrameworkCore;
using SuperWarehouseBackend.WebApi.Db;
using SuperWarehouseBackend.WebApi.Db.Entities;
using SuperWarehouseBackend.WebApi.Dtos;
using InboundDocument = SuperWarehouseBackend.WebApi.Db.Entities.InboundDocument;

namespace SuperWarehouseBackend.WebApi.Repositories;

public class InboundDocumentsRepository
{
    private readonly MainDbContext _dbContext;
    private readonly InboundResourcesRepository _inboundResourcesRepository;

    public InboundDocumentsRepository(MainDbContext dbContext, InboundResourcesRepository inboundResourcesRepository)
    {
        _dbContext = dbContext;
        _inboundResourcesRepository = inboundResourcesRepository;
    }

    public enum TryAddResult
    {
        Ok,
        DocumentNumberExists,
    }
    public async Task<TryAddResult> TryAdd(InboundDocumentInput inboundDocumentInput)
    {
        if (_dbContext.InboundDocuments.Any(x => x.Number == inboundDocumentInput.Number))
        {
            return TryAddResult.DocumentNumberExists;
        }

        var inboundResources = inboundDocumentInput.InboundResourceInputs.Select(iri => new InboundResource()
        {
            ResourceGuid = iri.ResourceGuid,
            MeasureUnitGuid = iri.MeasureUnitGuid,
            Quantity = iri.Quantity
        }).ToList();
        
        _dbContext.InboundDocuments.Add(new InboundDocument
        {
            Number = inboundDocumentInput.Number,
            Date = inboundDocumentInput.Date,
            InboundResources = inboundResources
        });
        await _dbContext.SaveChangesAsync();
        return TryAddResult.Ok;
    }

    private record struct ResourceChangeKey(Guid ResourceGuid, Guid MeasureUnitGuid);

    public enum UpdateResult
    {
        Ok,
        NotFound,
        NotEnoughResources
    }

    public async Task<UpdateResult> Update(InboundDocumentInput inboundDocumentInput)
    {
        InboundDocument? existsInboundDocument = await
            _dbContext.InboundDocuments.Include(ind => ind.InboundResources)
                .FirstOrDefaultAsync(x => x.Number == inboundDocumentInput.Number);

        if (existsInboundDocument is null)
            return UpdateResult.NotFound;

        // Document update
        existsInboundDocument.Date = inboundDocumentInput.Date;

        // Dictionaries
        var existingInboundResourcesDict = existsInboundDocument.InboundResources
            .ToDictionary(r => new ResourceChangeKey(r.ResourceGuid, r.MeasureUnitGuid));

        var inputInboundResourcesDict = inboundDocumentInput.InboundResourceInputs
            .ToDictionary(r => new ResourceChangeKey(r.ResourceGuid, r.MeasureUnitGuid));

        var resourceQuantitiesChangesDict = new Dictionary<ResourceChangeKey, decimal>();

        // Update or add InboundResource's
        foreach (var input in inboundDocumentInput.InboundResourceInputs)
        {
            var resourceChangeKey = new ResourceChangeKey(input.ResourceGuid, input.MeasureUnitGuid);
            if (existingInboundResourcesDict.TryGetValue(resourceChangeKey, out var existing))
            {
                resourceQuantitiesChangesDict.Add(resourceChangeKey, existing.Quantity - input.Quantity);
                existing.Quantity = existing.Quantity;
            }
            else
            {
                resourceQuantitiesChangesDict.Add(resourceChangeKey, input.Quantity);
                existsInboundDocument.InboundResources.Add(new InboundResource
                {
                    InboundDocumentGuid = existsInboundDocument.Guid,
                    ResourceGuid = input.ResourceGuid,
                    MeasureUnitGuid = input.MeasureUnitGuid,
                    Quantity = input.Quantity
                });
            }
        }

        // Delete InboundResource's
        var toRemove = existsInboundDocument.InboundResources
            .Where(r => !inputInboundResourcesDict.ContainsKey(new ResourceChangeKey(r.ResourceGuid, r.MeasureUnitGuid)))
            .ToList();

        foreach (var removeItem in toRemove)
        {
            var resourceChangeKey = new ResourceChangeKey(removeItem.ResourceGuid, removeItem.MeasureUnitGuid);
            resourceQuantitiesChangesDict.Add(resourceChangeKey, -removeItem.Quantity);
            _dbContext.InboundResources.Remove(removeItem);
        }

        // Update and check total resource quantities
        var keys = resourceQuantitiesChangesDict.Keys.ToArray();
        var resourceTotalQuantities = _dbContext.ResourceTotalQuantities.Where(rtq =>
            keys.Any(rqc => rqc.ResourceGuid == rtq.ResourceGuid && rqc.MeasureUnitGuid == rtq.MeasureUnitGuid));

        foreach (var resourceTotalQuantity in resourceTotalQuantities)
        {
            var resourceChangeKey =
                new ResourceChangeKey(resourceTotalQuantity.ResourceGuid, resourceTotalQuantity.MeasureUnitGuid);
            resourceTotalQuantity.Quantity += resourceQuantitiesChangesDict[resourceChangeKey];
            if (resourceTotalQuantity.Quantity < 0)
                return UpdateResult.NotEnoughResources;
        }

        await _dbContext.SaveChangesAsync();
        return UpdateResult.Ok;
    }

    public async Task<InboundDocument[]> GetAll(bool includeResources, int skip, int take)
    {
        var query = _dbContext.InboundDocuments.AsNoTracking();
        if (includeResources)
        {
            query = query.Include(r => r.InboundResources);
        }
        query = query.OrderBy(id => id.Number).Skip(skip).Take(take);
        var result = await query.ToArrayAsync();
        return result;
    }

    public async Task<InboundDocument[]> GetAllWithResources(bool includeResources)
    {
        var result = await _dbContext.InboundDocuments.Include(x => x.InboundResources).ToArrayAsync();
        return result;
    }
}