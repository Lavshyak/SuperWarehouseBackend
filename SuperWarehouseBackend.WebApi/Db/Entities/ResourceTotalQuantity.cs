using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SuperWarehouseBackend.WebApi.Db.Entities;

[PrimaryKey(nameof(ResourceGuid), nameof(MeasureUnitGuid))]
public class ResourceTotalQuantity
{
    public Guid ResourceGuid { get; set; }
    public Guid MeasureUnitGuid { get; set; }
    
    [Column(TypeName = "numeric(20, 10)")]
    public decimal Quantity { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}