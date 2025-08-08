using Microsoft.EntityFrameworkCore;
using SuperWarehouseBackend.WebApi.Db;
using SuperWarehouseBackend.WebApi.Db.Entities;

namespace SuperWarehouseBackend.WebApi.Repositories;

public class BalanceRepository
{
    private MainDbContext _mainDbContext;

    public BalanceRepository(MainDbContext mainDbContext)
    {
        _mainDbContext = mainDbContext;
    }

    public async Task<ResourceTotalQuantity[]> GetAll()
    {
        var resourceQuantities = await _mainDbContext.ResourceTotalQuantities.ToArrayAsync();
        return resourceQuantities;
    }
}