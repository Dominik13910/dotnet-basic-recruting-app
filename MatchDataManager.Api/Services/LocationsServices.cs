using AutoMapper;
using MatchDataManager.Api.Dto.Location;
using MatchDataManager.Api.Exceptions;
using MatchDataManager.Api.Interfaces;
using MatchDataManager.Api.Models;
using Microsoft.AspNetCore.Mvc;

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
        var location = GetLocationById(id);
        var result = _mapper.Map<LocationDto>(location);
        return result;
    }

    public async Task<ActionResult<IEnumerable<LocationDto>>> GetAll()
    {
        var location = _appDbContext.Location.ToList();
        var result = _mapper.Map<List<LocationDto>>(location);
        return result;
    }
    public async Task<Guid> Create(CreateLocationDto dto)
    {
        var location = _mapper.Map<Location>(dto);
        _appDbContext.Location.Add(location);
        _appDbContext.SaveChanges();
        return  location.Id;
    }
    public async Task Delete(Guid id)
    {
        _logger.LogError($"Location with id:{id} Deleted action invoked");
        var location = GetLocationById(id);
        _appDbContext.Location.Remove(location);
        _appDbContext.SaveChanges();
        
    }
    public async Task Update(Guid id, UpdateLocationDto location)
    {
        var result = GetLocationById(id);
        result.Name = location.Name;
        result.City = location.City;
        _appDbContext.SaveChanges();
        
    }


    private Location GetLocationById(Guid Id)
        
    {
        var location = _appDbContext.Location.FirstOrDefault(x => x.Id == Id);
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