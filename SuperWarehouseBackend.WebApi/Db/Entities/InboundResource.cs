using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SuperWarehouseBackend.WebApi.Db.Entities;

[PrimaryKey(nameof(InboundDocumentGuid), nameof(ResourceGuid), nameof(MeasureUnitGuid))]
public class InboundResource
{
    [ForeignKey(nameof(InboundDocument))]
    public Guid InboundDocumentGuid { get; set; }

    public InboundDocument? InboundDocument { get; set; }

    [ForeignKey(nameof(Resource))]
    public required Guid ResourceGuid { get; set; }

    public Resource? Resource { get; set; }

    [ForeignKey(nameof(MeasureUnit))]
    public required Guid MeasureUnitGuid { get; set; }

    public MeasureUnit? MeasureUnit { get; set; }

    [Column(TypeName = "numeric(20, 10)")]
    public required decimal Quantity { get; set; }
}