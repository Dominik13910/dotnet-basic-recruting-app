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
    private readonly ILogger<LocationsRepository> _logger;

    public TeamRepository(AppDbContext appDbContext, IMapper mapper, ILogger<LocationsRepository> logger)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _logger = logger;

    }

    public TeamDto GetById(Guid id)
    {
        var team = GetTeamById(id);
        var result = _mapper.Map<TeamDto>(team);
        return result;
    }

    public ActionResult<IEnumerable<TeamDto>> GetAll()
    {
        var team = _appDbContext.Location.ToList();
        var result = _mapper.Map<List<TeamDto>>(team);
        return result;
    }
    public Guid Create(CreateTeamDto dto)
    {
        var team = _mapper.Map<Team>(dto);
        _appDbContext.Team.Add(team);
        _appDbContext.SaveChanges();
        return team.Id;
    }
    public void Delete(Guid id)
    {
        _logger.LogError($"User with id:{id} Deleted action invoked");
        var team = GetTeamById(id);
        _appDbContext.Team.Remove(team);
        _appDbContext.SaveChanges();
    }


    private Team GetTeamById(Guid Id)

    {
        var team = _appDbContext.Team.FirstOrDefault(x => x.Id == Id);
        if (team == null)
        {
            throw new NotFoundException("Location Not Found");

        }
        return team;
    }

}