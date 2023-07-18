using AutoMapper;
using MatchDataManager.Application.Authentication;
using MatchDataManager.DataBase.Dto.Location;
using MatchDataManager.DataBase.Dto.Team;
using MatchDataManager.Application.Exceptions;
using MatchDataManager.Infrastructure.Interfaces;
using MatchDataManager.DataBase.Models;
using MatchDataManager.DataBase.Models.Paination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace MatchDataManager.Infrastructure.Services;

public class TeamServices : ITeamInterface
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<TeamServices> _logger;
    private readonly AuthenticationSettings _authenticationSetting;

    public TeamServices(AppDbContext appDbContext, IMapper mapper, ILogger<TeamServices> logger,
        AuthenticationSettings authenticationSettings)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _logger = logger;
        _authenticationSetting = authenticationSettings;
    }

    public async Task<TeamDto> GetById(Guid id)
    {
        var team = await GetTeamById(id);
        var result = _mapper.Map<TeamDto>(team);
        return result;
    }

    public async Task<PagedResult<TeamDto>> GetAll(Query query)
    {
        var basequery = _appDbContext
            .Team
            .Where(r => query.SerchName == null || (r.Name.ToLower().Contains(query.SerchName.ToLower())));

        var team = await basequery
            .Skip(query.PageSize * (query.PageNumber - 1))
            .Take(query.PageSize)
            .ToListAsync();

        var totalItemsCount = basequery.Count();
        var teamDto = _mapper.Map<List<TeamDto>>(team);
        var result = new PagedResult<TeamDto>(teamDto, totalItemsCount, query.PageSize, query.PageNumber);
        return result;
    }

    public async Task<Guid> Create(CreateTeamDto dto)
    {
        var team = _mapper.Map<Team>(dto);
        _appDbContext.Team.Add(team);
        await _appDbContext.SaveChangesAsync();
        return team.Id;
    }

    public async Task Delete(Guid id)
    {
        _logger.LogError($"Team with id:{id} Deleted action invoked");
        _logger.LogInformation($"Information{id}");

        var team = await GetTeamById(id);
        _appDbContext.Team.Remove(team);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Update(Guid id, UpdateTeamDto location)
    {
        var result = await GetTeamById(id);
        result.Name = location.Name;
        result.CoachName = location.CoachName;
        await _appDbContext.SaveChangesAsync();
    }

    private async Task<Team> GetTeamById(Guid id)

    {
        var team = await _appDbContext.Team.FindAsync(id);
        if (team == null)
            throw new NotFoundException("Team Not Found");


        return team;
    }

    public async Task<string> GenerateJWt(Guid id, TeamDto dto)
    {
        var team = await _appDbContext
            .Team
            .FirstOrDefaultAsync(t => t.Id == dto.Id);

        if (team == null)
        {
            throw new BadHttpRequestException("Bad team");
        }
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, team.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{team.Name}"),
            new Claim("Coach", team.CoachName)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSetting.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_authenticationSetting.JwtExpireDays);

        var token = new JwtSecurityToken(_authenticationSetting.JwtIssuer,
            _authenticationSetting.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred);

        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }
}