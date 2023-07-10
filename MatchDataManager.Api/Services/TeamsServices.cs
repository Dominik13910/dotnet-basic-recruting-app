using AutoMapper;
using MatchDataManager.Api.Dto.Location;
using MatchDataManager.Api.Dto.Team;
using MatchDataManager.Api.Exceptions;
using MatchDataManager.Api.Interfaces;
using MatchDataManager.Api.Models;
using MatchDataManager.Api.Models.Paination;
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

    public async Task<PagedResult<TeamDto>> GetAll(Query query)
    {
        var basequery = _appDbContext
            .Team
            .Where(r => query.SerchName == null || (r.Name.ToLower().Contains(query.SerchName.ToLower())));
            
        var team =  basequery
            .Skip(query.PageSize * (query.PageNumber - 1))
            .Take(query.PageSize)
            .ToList();

        var totalItemsCount = basequery.Count();
        var teamDto =   _mapper.Map<List<TeamDto>>(team);
        var result = new PagedResult<TeamDto>(teamDto, totalItemsCount, query.PageSize, query.PageNumber);
        return result;
    }
    public async Task<Guid> Create(CreateTeamDto dto)
    {
        var team = _mapper.Map<Team>(dto);
        _appDbContext.Team.Add(team);
        _appDbContext.SaveChanges();
        return  team.Id;
    }
    public async Task Delete(Guid id)
    {
        

        _logger.LogError($"Team with id:{id} Deleted action invoked");
        _logger.LogInformation($"Information{id}");

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