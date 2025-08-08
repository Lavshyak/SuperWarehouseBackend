using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SuperWarehouseBackend.WebApi.Dtos;
using SuperWarehouseBackend.WebApi.Repositories;

namespace SuperWarehouseBackend.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ResourcesController : ControllerBase
{
    private readonly ResourcesRepository _resourcesRepository;

    public ResourcesController(ResourcesRepository resourcesRepository)
    {
        _resourcesRepository = resourcesRepository;
    }

    [HttpGet]
    public async Task<ResourceDtoOutput[]> GetAllNotArchived()
    {
        var resources = await _resourcesRepository.GetAllNotArchived();
        var resourcesOutput = resources.Select(r => new ResourceDtoOutput(r.Guid, r.Name, r.IsArchived)).ToArray();
        
        return resourcesOutput;
    }

    public enum ResourcesAddResult
    {
        Ok,
        InvalidName,
        NameAlreadyExists,
        UnknownError
    }
    
    [HttpPost]
    public async Task<ResourcesAddResult> Add(ResourceDtoAdd resourceDtoAdd)
    {
        var result = await _resourcesRepository.Add(resourceDtoAdd);

        switch (result)
        {
            case ResourcesRepository.AddResult.Ok:
                return ResourcesAddResult.Ok;
            case ResourcesRepository.AddResult.InvalidName:
                return ResourcesAddResult.InvalidName;
            case ResourcesRepository.AddResult.NameAlreadyExists:
                return ResourcesAddResult.NameAlreadyExists;
            default:
                // log error
                return ResourcesAddResult.UnknownError;
        }
    }

    [HttpPost]
    public async Task<Results<Ok, Conflict>> Update(ResourceDtoUpdate resourceDtoUpdate)
    {
        var result = await _resourcesRepository.Update(resourceDtoUpdate);
        switch (result)
        {
            case true:
                return TypedResults.Ok();
            default:
                return TypedResults.Conflict();
        }
    }

    [HttpPost]
    public async Task<Results<Ok, Conflict>> Delete(ResourceDtoDelete resourceDtoDelete)
    {
        var result = await _resourcesRepository.Delete(resourceDtoDelete);
        switch (result)
        {
            case true:
                return TypedResults.Ok();
            default:
                return TypedResults.Conflict();
        }
    }
}