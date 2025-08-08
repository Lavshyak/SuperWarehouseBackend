namespace SuperWarehouseBackend.WebApi.Dtos;

public record MeasureUnitDtoGetResult(Guid Guid, string Name, bool IsArchived);

public record MeasureUnitDtoAdd(string Name, bool IsArchived);

public record MeasureUnitDtoAddResult(Guid Guid, string Name, bool IsArchived);

public record MeasureUnitDtoUpdate(Guid Guid, string Name, bool IsArchived);

public record MeasureUnitDtoDelete(Guid Guid);