namespace SuperWarehouseBackend.WebApi.Dtos;

public record InboundResourceOutput(Guid Guid, Guid ResourceGuid, Guid MeasureUnitGuid, Guid InboundDocumentGuid, decimal Quantity);
public record InboundResourceInput(Guid ResourceGuid, Guid MeasureUnitGuid, Guid InboundDocumentGuid, decimal Quantity);