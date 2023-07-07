using MatchDataManager.Api.Dto.Location;
using MatchDataManager.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Api.Interfaces
{
    public interface ILocationInterface
    {
        LocationDto GetById(Guid id);
        ActionResult<IEnumerable<LocationDto>> GetAll();
        Guid Create(CreateLocationDto dto);
        void Delete(Guid id);
        
    }
}
