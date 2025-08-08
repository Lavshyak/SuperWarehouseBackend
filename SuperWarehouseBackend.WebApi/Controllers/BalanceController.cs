using Microsoft.AspNetCore.Mvc;
using SuperWarehouseBackend.WebApi.Db.Entities;
using SuperWarehouseBackend.WebApi.Dtos;
using SuperWarehouseBackend.WebApi.Repositories;

namespace SuperWarehouseBackend.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BalanceController : ControllerBase
{
    private BalanceRepository _balanceRepository;

    public BalanceController(BalanceRepository balanceRepository)
    {
        _balanceRepository = balanceRepository;
    }

    [HttpGet]
    public async Task<ResourceTotalQuantityDtoOutput[]> GetAll()
    {
        var balances = await _balanceRepository.GetAll();
        var balancesDto = balances
            .Select(b => new ResourceTotalQuantityDtoOutput(b.ResourceGuid, b.MeasureUnitGuid, b.Quantity))
            .ToArray();
        return balancesDto;
    }
}