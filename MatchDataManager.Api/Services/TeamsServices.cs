using AutoMapper;
using MatchDataManager.Api.Dto.Location;
using MatchDataManager.Api.Dto.Team;
using MatchDataManager.Api.Exceptions;
using MatchDataManager.Api.Interfaces;
using MatchDataManager.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Api.Repositories;

public class TeamRepository : ITeamInterface
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<LocationsServices> _logger;

    public TeamRepository(AppDbContext appDbContext, IMapper mapper, ILogger<LocationsServices> logger)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _logger = logger;

    }

    public async Task<TeamDto> GetById(Guid id)
    {
        var team = GetTeamById(id);
        var result = _mapper.Map<TeamDto>(team);
        return result;
    }

    public async Task<ActionResult<IEnumerable<TeamDto>>> GetAll()
    {
        var team = _appDbContext.Team.ToList();
        var result = _mapper.Map<List<TeamDto>>(team);
        return result;
    }
    public async Task<Guid> Create(CreateTeamDto dto)
    {
        var team = _mapper.Map<Team>(dto);
        _appDbContext.Team.Add(team);
        _appDbContext.SaveChanges();
        return team.Id;
    }
    public async Task Delete(Guid id)
    {
        _logger.LogError($"Team with id:{id} Deleted action invoked");
        var team = GetTeamById(id);
        _appDbContext.Team.Remove(team);
        _appDbContext.SaveChanges();
    }
    public async Task Update(Guid id, UpdateTeamDto location)
    {
        var result = GetTeamById(id);
        result.Name = location.Name;
        result.CoachName = location.CoachName;
        _appDbContext.SaveChanges();
    }

    private Team GetTeamById(Guid id)

    {
        var team = _appDbContext.Team.FirstOrDefault(t => t.Id == id);
        if (team == null)
            throw new NotFoundException("Team Not Found");

        
        return team;
    }

}