namespace MatchDataManager.DataBase.Dto.Match
{
    public class CreateMatchDto
    {
        public string FirstTeam { get; set; }

        public string SecoundTeam { get; set; }

        public string Location { get; set; }

        public DateTime StartData { get; set; }
    }
}