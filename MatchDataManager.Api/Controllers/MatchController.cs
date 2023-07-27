using MatchDataManager.DataBase.Dto.Location;
using MatchDataManager.DataBase.Dto.Match;
using MatchDataManager.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.DataBase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly IMatchInterface _matchInterface;

        public MatchController(IMatchInterface locationInterface)
        {
            _matchInterface = locationInterface;
        }

        [HttpPost]
        public async Task<ActionResult> AddMatch([FromBody] CreateMatchDto location)
        {
            var result = await _matchInterface.Create(location);
            return Created($"/api/Match/{result}", null);
        }

        [HttpDelete]
        public async Task DeleteMatch([FromQuery] Guid matchId)
        {
            await _matchInterface.Delete(matchId);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetAll()
        {
            var result = await _matchInterface.GetAll();
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<MatchDto>> GetById([FromRoute] Guid id)
        {
            var result = await _matchInterface.GetById(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task UpdateMatch([FromQuery] Guid id, [FromBody] UpdateMatchDto location)
        {
            await _matchInterface.Update(id, location);
        }
    }
}