namespace SuperWarehouseBackend.WebApi.Entities;

public record InboundResource(Guid Guid, Guid ResourceGuid, Guid MeasureUnitGuid, Guid InboundDocumentGuid)
{
}