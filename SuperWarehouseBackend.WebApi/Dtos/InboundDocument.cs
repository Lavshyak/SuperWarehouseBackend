namespace SuperWarehouseBackend.WebApi.Entities;

public record InboundDocument(Guid Guid, long Number, DateTime Date)
{
    public InboundResource[] InboundResources { get; set; } = [];
}