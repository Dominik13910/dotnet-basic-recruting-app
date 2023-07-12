using AutoMapper;
using MatchDataManager.Api.Dto.Location;
using MatchDataManager.Api.Exceptions;
using MatchDataManager.Api.Interfaces;
using MatchDataManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatchDataManager.Api.Repositories;

public class LocationsServices : ILocationInterface
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<LocationsServices> _logger;

    public LocationsServices(AppDbContext appDbContext, IMapper mapper, ILogger<LocationsServices> logger)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _logger = logger;
            
    }

    public async Task<LocationDto> GetById(Guid id)
    {
        var location = await  GetLocationById(id);
        var result = _mapper.Map<LocationDto>(location);
        return result;
    }

    public async Task<ActionResult<IEnumerable<LocationDto>>> GetAll()
    {
        var location = await _appDbContext.Location.ToListAsync();
        var result = _mapper.Map<List<LocationDto>>(location);
        return  result;
    }
    public async Task<Guid> Create(CreateLocationDto dto)
    {
        var location = _mapper.Map<Location>(dto);
        _appDbContext.Location.Add(location);
        await _appDbContext.SaveChangesAsync();
        return  location.Id;
    }
    public async Task Delete(Guid id)
    {
        _logger.LogError($"Location with id:{id} Deleted action invoked");
        var location = await GetLocationById(id);
        _appDbContext.Location.Remove(location);
       await _appDbContext.SaveChangesAsync();
        
    }
    public async Task Update(Guid id, UpdateLocationDto location)
    {
        var result = await GetLocationById(id);
        result.Name = location.Name;
        result.City = location.City;
      await  _appDbContext.SaveChangesAsync();
        
    }


    private async  Task<Location> GetLocationById(Guid id)
        
    {
        var location = await _appDbContext.Location.FindAsync(id);
        if (location == null)
        {
            throw new NotFoundException("Location Not Found");
            
        }
        return location;
    }














































        /* private static readonly List<Location> _locations = new();

          public static void AddLocation(Location location)
          {
              location.Id = Guid.NewGuid();
              _locations.Add(location);
          }

          public static void DeleteLocation(Guid locationId)
          {
              var location = _locations.FirstOrDefault(x => x.Id == locationId);
              if (location is not null)
              {
                  _locations.Remove(location);
              }
          }

          public static IEnumerable<Location> GetAllLocations()
          {
              return _locations;
          }

          public static Location GetLocationById(Guid id)
          {
              return _locations.FirstOrDefault(x => x.Id == id);
          }

          public static void UpdateLocation(Location location)
          {
              var existingLocation = _locations.FirstOrDefault(x => x.Id == location.Id);
              if (existingLocation is null || location is null)
              {
                  throw new ArgumentException("Location doesn't exist.", nameof(location));
              }

              existingLocation.City = location.City;
              existingLocation.Name = location.Name;
          }*/
    
}