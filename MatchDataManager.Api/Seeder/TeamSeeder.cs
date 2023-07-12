using MatchDataManager.Api.Models;

namespace MatchDataManager.Api.Seeder
{
    public class TeamSeeder
    {
       private readonly AppDbContext _appDbContext;

        public TeamSeeder(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Seed()
        {
            if (_appDbContext.Database.CanConnect())
            {

                if (!_appDbContext.Team.Any())
                {
                    var roles = GetTeam();
                    _appDbContext.Team.AddRange(roles);
                    _appDbContext.SaveChanges();
                }
            }
        }
        private IEnumerable<Team> GetTeam()
        {
            var team = new List<Team>()
            {
                new Team()
                {
                    Name = "YellowBoys",
                    CoachName = "Marcin"
                },
                 new Team()
                 {
                     Name = "BlueBoys",
                     CoachName = "Andrzej"
                 },
                  new Team()
                  {
                      Name = "Greenboys",
                      CoachName = "Adam"
                  },
                   new Team()
                   {
                       Name = "RedBoys",
                       CoachName = "Adrian"
                   },
                    new Team()
                    {
                        Name = "GreyBoys",
                        CoachName = "Paweł"
                    },
                     new Team()
                     {
                         Name = "ImgBoys",
                         CoachName = "Krzysztof"
                     }
            };
            return team;
        }
    }
}
