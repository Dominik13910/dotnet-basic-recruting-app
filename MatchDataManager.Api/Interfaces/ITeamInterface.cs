using MatchDataManager.Api.Dto.Team;
using MatchDataManager.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Api.Interfaces
{
    public interface ITeamInterface
    {
        TeamDto GetById(Guid id);
        ActionResult<IEnumerable<TeamDto>> GetAll();
        Guid Create(CreateTeamDto dto);
        void Delete(Guid id);
        

    }
}
