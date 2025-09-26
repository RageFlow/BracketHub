using BracketHubShared.Enums;
using BracketHubShared.Statics;

namespace BracketHubShared.Models
{
    public class TournamentModel
    {
        public int? Id { get; set; }
        public string GameType { get; set; } = string.Empty;
        public Status Status { get; set; }
        public string Name { get; set; } = $"New Tournament"; // Placeholder
        public string? Banner { get; set; }
        public DateTime? Date { get; set; } = DateTime.Today;
        public bool IsPublic { get; set; }

        public TournamentModel(int id, string name, bool isPublic, string gameType, Status status = Status.Open, DateTime? date = null)
        {
            Id = id;
            Name = name;
            IsPublic = isPublic;
            GameType = gameType;
            Status = status;
            Date = date ?? DateTime.Today;
        }

        public TournamentModel() { }
    }

#if DEBUG
    public static class TournamentModelConversion
    {
        public static List<TournamentModel> TournamentList = new()
        {
            new TournamentModel(3423, "Test tournament", true, "CS2")
            {
                Banner = ImageStatics.BannerUrl("CSS")
            },
            new TournamentModel(1231, "Rivals go go", true, "CSS"),
            new TournamentModel(9863, "Rivals go go", true, "DEADLOCK"),
            new TournamentModel(3245, "Rivals go go", true, "FM24"),
            new TournamentModel(8763, "Rivals go go", true, "RL"),
            new TournamentModel(1123, "Rivals go go", true, "MRIVALS"),
            new TournamentModel(2283, "Rivals go go", true, "LOL"),
            new TournamentModel(3465, "Rivals go go", true, "R6S"),
            new TournamentModel(1293, "Rivals go go", true, "AFTERH"),
            new TournamentModel(3746, "Rivals go go", true, "APEX"),
            new TournamentModel(3633,"Rivals go go", true, "DOTA2"),
            new TournamentModel(9436, "Rivals go go", true, "FORTNITE"),
            new TournamentModel(9846, "Local testing", false, "CS2"),
            new TournamentModel(1232, "Local testing", false, "CS2", Status.Closed),
            new TournamentModel(5343, "Local testing", false, "CS2", Status.Running),
            new TournamentModel(1543, "Local testing", false, "CS2", Status.Ended),
        };
    }
#endif
}
