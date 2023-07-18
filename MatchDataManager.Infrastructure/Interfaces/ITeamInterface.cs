using MatchDataManager.DataBase.Dto.Team;
using MatchDataManager.DataBase.Models.Paination;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Infrastructure.Interfaces
{
    public interface ITeamInterface
    {
        Task<TeamDto> GetById(Guid id);
        Task<PagedResult<TeamDto>> GetAll(Query query);
        Task<Guid> Create(CreateTeamDto dto);
        Task Delete(Guid id);
        Task Update(Guid id, UpdateTeamDto location);
        Task<string> GenerateJWt(Guid id, TeamDto dto);
    }
}