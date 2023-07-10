using MatchDataManager.Api.Dto.Location;
using MatchDataManager.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Api.Interfaces
{
    public interface ILocationInterface
    {
        Task<LocationDto> GetById(Guid id);
        Task<ActionResult<IEnumerable<LocationDto>>> GetAll();
        Task<Guid> Create(CreateLocationDto dto);
        Task Delete(Guid id);
        Task Update(Guid id, UpdateLocationDto location);


    }
}
