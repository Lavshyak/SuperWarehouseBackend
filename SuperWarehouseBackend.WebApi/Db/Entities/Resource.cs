using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SuperWarehouseBackend.WebApi.Db.Entities;

public class Resource
{
    [Key]
    public Guid Guid { get; set; }

    public const int NameMaxLength = 50;

    /// <summary>
    /// index, unique
    /// </summary>
    [MaxLength(NameMaxLength)]
    public required string Name { get; set; }

    public bool IsArchived { get; set; }
}