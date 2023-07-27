using System.Data;

namespace MatchDataManager.DataBase.Models
{
    public class Match : Entity
    {
        public string FirstTeam { get; set; }

        public string SecoundTeam { get; set; }

        public string Location { get; set; }

        public DateTime StartData { get; set; }
    }
}