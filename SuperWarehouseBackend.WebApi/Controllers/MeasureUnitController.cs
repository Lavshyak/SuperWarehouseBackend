using Microsoft.AspNetCore.Mvc;
using SuperWarehouseBackend.WebApi.Dtos;
using SuperWarehouseBackend.WebApi.General;
using SuperWarehouseBackend.WebApi.Repositories;

namespace SuperWarehouseBackend.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class MeasureUnitController : ControllerBase
{
    private readonly MeasureUnitRepository _measureUnitRepository;

    public MeasureUnitController(MeasureUnitRepository measureUnitRepository)
    {
        _measureUnitRepository = measureUnitRepository;
    }

    [HttpPost]
    public async Task<ResultOrError<MeasureUnitDtoAddResult, MeasureUnitRepository.MeasureUnitAddError>> Add(MeasureUnitDtoAdd measureUnitDtoAdd)
    {
        var result = await _measureUnitRepository.Add(measureUnitDtoAdd);
        if (result.IsSuccess)
        {
            var measureUnit = result.Result;
            return ResultOrError<MeasureUnitDtoAddResult, MeasureUnitRepository.MeasureUnitAddError>
                .FromResult(new MeasureUnitDtoAddResult(measureUnit.Guid, measureUnit.Name, measureUnit.IsArchived));
        }
        else
        {
            return ResultOrError<MeasureUnitDtoAddResult, MeasureUnitRepository.MeasureUnitAddError>
                .FromError(result.Error);
        }
    }

    [HttpPost]
    public async Task<bool> Update(MeasureUnitDtoUpdate measureUnitDtoUpdate)
    {
        var result = await _measureUnitRepository.Update(measureUnitDtoUpdate);
        return result;
    }
    
    [HttpPost]
    public async Task<bool> Delete(MeasureUnitDtoDelete measureUnitDtoDelete)
    {
        var result = await _measureUnitRepository.Delete(measureUnitDtoDelete);
        return result;
    }

    [HttpGet]
    public async Task<MeasureUnitDtoGetResult[]> GetAll(bool isArchived = false)
    {
        var result = await _measureUnitRepository.GetAll(isArchived);
        var measureUnitDtos = result.Select(mu => new MeasureUnitDtoGetResult(mu.Guid, mu.Name, mu.IsArchived)).ToArray();
        return measureUnitDtos;
    }
}