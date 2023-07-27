using AutoMapper;
using MatchDataManager.DataBase.Dto.Location;
using MatchDataManager.DataBase.Dto.Match;
using MatchDataManager.Application.Exceptions;
using MatchDataManager.Infrastructure.Interfaces;
using MatchDataManager.DataBase.Models;
using MatchDataManager.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MatchDataManager.Infrastructure.Mappers;

namespace MatchDataManager.Infrastructure.Services
{
    public class MatchServices : IMatchInterface
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<MatchServices> _logger;

        public MatchServices(AppDbContext appDbContext, IMapper mapper, ILogger<MatchServices> logger)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<MatchDto> GetById(Guid id)
        {
            var location = await GetMatchById(id);
            var result = _mapper.Map<MatchDto>(location);
            return result;
        }

        public async Task<ActionResult<IEnumerable<MatchDto>>> GetAll()
        {
            var location = await _appDbContext.Match.ToListAsync();
            var result = _mapper.Map<List<MatchDto>>(location);
            return result;
        }

        public async Task<Guid> Create(CreateMatchDto dto)
        {
            var location = _mapper.Map<Match>(dto);
            _appDbContext.Match.Add(location);
            await _appDbContext.SaveChangesAsync();
            return location.Id;
        }

        public async Task Delete(Guid id)
        {
            _logger.LogError($"Location with id:{id} Deleted action invoked");
            var location = await GetMatchById(id);
            _appDbContext.Match.Remove(location);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Update(Guid id, UpdateMatchDto location)
        {
            var result = await GetMatchById(id);
            result.FirstTeam = location.FirstTeam;
            result.SecoundTeam = location.SecoundTeam;
            result.Location = location.Location;
            result.StartData = location.StartData;
            await _appDbContext.SaveChangesAsync();
        }
        
        private async Task<Match> GetMatchById(Guid id)

        {
            var location = await _appDbContext.Match.FindAsync(id);
            if (location == null)
            {
                throw new NotFoundException("Location Not Found");
            }
            return location;
        }
    }
}