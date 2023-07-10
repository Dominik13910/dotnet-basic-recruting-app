using MatchDataManager.Api.Dto.Team;
using MatchDataManager.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Api.Interfaces
{
    public interface ITeamInterface
    {
        Task<TeamDto> GetById(Guid id);
        Task<ActionResult<IEnumerable<TeamDto>>> GetAll();
        Task<Guid> Create(CreateTeamDto dto);
        Task Delete(Guid id);
        Task Update(Guid id, UpdateTeamDto location);

    }
}
