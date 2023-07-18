using MatchDataManager.DataBase.Dto.Location;
using MatchDataManager.Infrastructure.Interfaces;
using MatchDataManager.DataBase.Models;
using MatchDataManager.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.DataBase.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase
{
    private readonly ILocationInterface _locationInterface;

    public LocationsController(ILocationInterface locationInterface)
    {
        _locationInterface = locationInterface;
    }

    [HttpPost]
    public async Task<ActionResult> AddLocation([FromBody] CreateLocationDto location)
    {
        var result = await _locationInterface.Create(location);
        return Created($"/api/Location/{result}", null);
    }

    [HttpDelete]
    public async Task DeleteLocation([FromQuery] Guid locationId)
    {
        await _locationInterface.Delete(locationId);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LocationDto>>> GetAll()
    {
        var result = await _locationInterface.GetAll();
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LocationDto>> GetById([FromRoute] Guid id)
    {
        var result = await _locationInterface.GetById(id);
        return Ok(result);
    }

    [HttpPut]
    public async Task UpdateLocation([FromQuery] Guid id, [FromBody] UpdateLocationDto location)
    {
        await _locationInterface.Update(id, location);
    }
}