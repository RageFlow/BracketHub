namespace BracketHubWeb.Models
{
    public class SimpleTournamentModel
    {
        public string? Name { get; set; }
        public bool IsPublic { get; set; }
        public string Type { get; set; } = string.Empty;
        public string? Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;

        public SimpleTournamentModel(string? name, bool isPublic, string id, string type, DateTime? date = null)
        {
            Name = name;
            IsPublic = isPublic;
            Id = id;
            Type = type;
            Date = date ?? DateTime.Today;
        }
    }

    public static class SimpleTournamentModelStatics
    {
        public static List<SimpleTournamentModel> TournamentList = new()
        {
            new SimpleTournamentModel("Rivals go go", true, "54624", "CS2"),
            new SimpleTournamentModel("Rivals go go", true, "65745", "CSS"),
            new SimpleTournamentModel("Rivals go go", true, "67786", "DEADLOCK"),
            new SimpleTournamentModel("Rivals go go", true, "87978", "FM24"),
            new SimpleTournamentModel("Rivals go go", true, "76867", "RL"),
            new SimpleTournamentModel("Rivals go go", true, "56756", "MRIVALS"),
            new SimpleTournamentModel("Rivals go go", true, "23423", "LOL"),
            new SimpleTournamentModel("Rivals go go", true, "23143", "R6S"),
            new SimpleTournamentModel("Rivals go go", true, "23423", "AFTERH"),
            new SimpleTournamentModel("Rivals go go", true, "32423", "APEX"),
            new SimpleTournamentModel("Rivals go go", true, "46532", "DOTA2"),
            new SimpleTournamentModel("Rivals go go", true, "43635", "FORTNITE"),
            new SimpleTournamentModel("Local testing", false, "42346", "CS2"),
            new SimpleTournamentModel("Local testing", false, "65463", "CS2"),
            new SimpleTournamentModel("Local testing", false, "32454", "CS2"),
            new SimpleTournamentModel("Local testing", false, "23453", "CS2"),
        };
    }
}
