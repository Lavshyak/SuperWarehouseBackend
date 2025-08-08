namespace SuperWarehouseBackend.WebApi.Entities;

public record Resource(Guid Guid, string Name, bool IsArchived)
{
}