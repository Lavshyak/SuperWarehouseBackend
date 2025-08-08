namespace SuperWarehouseBackend.WebApi.Dtos;

public record InboundDocumentInput(string Number, DateTime Date, InboundResourceInput[] InboundResourceInputs);

public record InboundDocumentOutput(Guid Guid, string Number, DateTime Date, InboundResourceOutput[] InboundResourceOutputs);