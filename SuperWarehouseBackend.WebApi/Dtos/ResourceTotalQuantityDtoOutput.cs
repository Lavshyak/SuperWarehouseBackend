namespace SuperWarehouseBackend.WebApi.Db.Entities;

public record ResourceTotalQuantityDtoOutput(Guid ResourceGuid, Guid MeasureUnitGuid, decimal Quantity);