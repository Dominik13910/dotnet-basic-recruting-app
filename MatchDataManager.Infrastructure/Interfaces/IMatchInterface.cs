using MatchDataManager.DataBase.Dto.Match;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Infrastructure.Interfaces
{
    public interface IMatchInterface
    {
        Task<MatchDto> GetById(Guid id);
        Task<ActionResult<IEnumerable<MatchDto>>> GetAll();
        Task<Guid> Create(CreateMatchDto dto);
        Task Delete(Guid id);
        Task Update(Guid id, UpdateMatchDto location);
    }
}