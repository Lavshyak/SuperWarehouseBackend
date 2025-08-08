using System.ComponentModel.DataAnnotations;

namespace SuperWarehouseBackend.WebApi.Db.Entities;

public class InboundDocument
{
    [Key]
    public Guid Guid { get; set; }

    public const int NumberMaxLength = 50;

    /// <summary>
    /// index, unique
    /// </summary>
    [MaxLength(NumberMaxLength)]
    public required string Number { get; set; }

    public required DateTime Date { get; set; }
    public List<InboundResource> InboundResources { get; set; } = [];
}