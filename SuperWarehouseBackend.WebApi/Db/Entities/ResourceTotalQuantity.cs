using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SuperWarehouseBackend.WebApi.Db.Entities;

[PrimaryKey(nameof(ResourceGuid), nameof(MeasureUnitGuid))]
public class ResourceTotalQuantity
{
    [ForeignKey(nameof(Resource))]
    public Guid ResourceGuid { get; set; }

    public Resource? Resource { get; set; }

    [ForeignKey(nameof(MeasureUnit))]
    public Guid MeasureUnitGuid { get; set; }

    public MeasureUnit? MeasureUnit { get; set; }

    [Column(TypeName = "numeric(20, 10)")]
    public decimal Quantity { get; set; }
}