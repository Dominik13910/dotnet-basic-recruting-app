using MatchDataManager.Api.Dto.Location;
using MatchDataManager.Api.Dto.Team;
using MatchDataManager.Api.Interfaces;
using MatchDataManager.Api.Models;
using MatchDataManager.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamsController : ControllerBase
{
    private readonly ITeamInterface _teamInterface;

    public TeamsController(ITeamInterface locationInterface)
    {
        _teamInterface = locationInterface;
    }



    [HttpPost]
    public async Task<ActionResult> AddTeam([FromBody] CreateTeamDto team)
    {
        var result = await _teamInterface.Create(team);
        return Created($"/api/Team/{result}", null);
    }

    [HttpDelete]
    public async Task DeleteTeam([FromQuery] Guid id)
    {
       await _teamInterface.Delete(id);
        

    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TeamDto>>> GetAll()
    {
        var result =  await _teamInterface.GetAll();
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TeamDto>> GetById([FromRoute] Guid id)
    {
        var result = await _teamInterface.GetById(id);
        return Ok(result);
    }
     [HttpPut]
    public async Task  UpdateTeam([FromQuery] Guid id, [FromBody] UpdateTeamDto team)
    {
        await _teamInterface.Update(id, team);
        
    }
}