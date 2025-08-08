namespace SuperWarehouseBackend.WebApi.Dtos;

public record ResourceDtoOutput(Guid Guid, string Name, bool IsArchived);

public record ResourceDtoAdd(string Name);

public record ResourceDtoUpdate(Guid Guid, string Name, bool IsArchived);

public record ResourceDtoDelete(Guid Guid);