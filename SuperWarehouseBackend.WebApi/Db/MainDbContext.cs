using Microsoft.EntityFrameworkCore;
using SuperWarehouseBackend.WebApi.Db.Entities;

namespace SuperWarehouseBackend.WebApi.Db;

public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
    }

    public DbSet<MeasureUnit> MeasureUnits { get; private set; } = null!;
    public DbSet<Resource> Resources { get; private set; } = null!;
    public DbSet<InboundResource> InboundResources { get; private set; } = null!;
    public DbSet<InboundDocument> InboundDocuments { get; private set; } = null!;
    public DbSet<ResourceTotalQuantity> ResourceTotalQuantities { get; private set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
     
        modelBuilder.HasDefaultSchema("public");
        
        modelBuilder.Entity<InboundDocument>(entity =>
        {
            entity.HasIndex(e => e.Number).IsUnique();
        });
        modelBuilder.Entity<InboundDocument>()
            .Property<uint>("xmin")
            .IsRowVersion();
        
        modelBuilder.Entity<Resource>(entity =>
        {
            entity.HasIndex(e => e.Name).IsUnique();
        });
        modelBuilder.Entity<Resource>()
            .Property<uint>("xmin")
            .IsRowVersion();
        
        modelBuilder.Entity<MeasureUnit>(entity =>
        {
            entity.HasIndex(e => e.Name).IsUnique();
        });
        modelBuilder.Entity<MeasureUnit>()
            .Property<uint>("xmin")
            .IsRowVersion();
        
        modelBuilder.Entity<ResourceTotalQuantity>()
            .Property<uint>("xmin")
            .IsRowVersion();
        
        modelBuilder.Entity<InboundResource>()
            .Property<uint>("xmin")
            .IsRowVersion();
    }
}