namespace SuperWarehouseBackend.WebApi.Dtos;

public record ResourceTotalQuantityDtoOutput(Guid ResourceGuid, Guid MeasureUnitGuid, decimal Quantity);