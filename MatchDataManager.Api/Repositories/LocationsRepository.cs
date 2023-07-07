using AutoMapper;
using MatchDataManager.Api.Dto.Location;
using MatchDataManager.Api.Exceptions;
using MatchDataManager.Api.Interfaces;
using MatchDataManager.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Api.Repositories;

public class LocationsRepository : ILocationInterface
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<LocationsRepository> _logger;

    public LocationsRepository(AppDbContext appDbContext, IMapper mapper, ILogger<LocationsRepository> logger)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _logger = logger;
            
    }

    public LocationDto GetById(int id)
    {
        var location = GetLocationById;
        var result = _mapper.Map<LocationDto>(location);
        return result;
    }

    public ActionResult<IEnumerable<LocationDto>> GetAll()
    {
        var location = _appDbContext.Location.ToList();
        var result = _mapper.Map<List<LocationDto>>(location);
        return result;
    }
    public Guid Create(CreateLocationDto dto)
    {
        var location = _mapper.Map<Location>(dto);
        _appDbContext.Location.Add(location);
        _appDbContext.SaveChanges();
        return location.Id;
    }
    public void Delete(int id)
    {
        _logger.LogError($"User with id:{id} Deleted action invoked");
        var location = GetLocationById;
        _appDbContext.Location.Remove(location);
        _appDbContext.SaveChanges();
    }


    private Location GetLocationById(Guid Id)
        
    {
        var location = _appDbContext.Location.FirstOrDefault(x => x.Id == Id);
        if (location == null)
        {
            throw new NotFoundException("Location Not Found");
            return location;
        }
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