namespace BracketHubWeb.Models
{
    public class MemberModel
    {
        public int Id { get; set; }
        public string Name {  get; set; }
        public string Nickname { get; set; }
        public string NameOrNickname => Name.Equals(Nickname, StringComparison.CurrentCultureIgnoreCase) ? Name : $"{Nickname} ({Name})";

        public MemberModel(int id, string? name = null, string? nickname = null)
        {
            Id = id;
            Name = name ?? $"Member-{id}";
            Nickname = nickname ?? Name;
        }
    }

    public static class MemberModelStatics
    {
        public static List<MemberModel> MemberList = new()
        {
            new MemberModel((int)Team.Test, "Testing Team", Team.Test.ToString()),
            new MemberModel((int)Team.Kombo, "Team Kombo", Team.Kombo.ToString()),

            new MemberModel((int)Team.Yes, "YesForAll", Team.Yes.ToString()),
            new MemberModel((int)Team.No, "No", Team.No.ToString()),

            new MemberModel((int)Team.Maybe, "Maybe", Team.Maybe.ToString()),
            new MemberModel((int)Team.Jojo, "Jojo's Gang", Team.Jojo.ToString()),

            new MemberModel((int)Team.Jiko, "Jiko Juko", Team.Jiko.ToString()),
            new MemberModel((int)Team.Flod, "Flod V2", Team.Flod.ToString()),

            new MemberModel((int)Team.Wongde, "Wongde", Team.Wongde.ToString()),
            new MemberModel((int)Team.Tokolo, "Team Tokolo", Team.Tokolo.ToString()),

            new MemberModel((int)Team.Ditch, "Ditch-Up", Team.Ditch.ToString()),
            new MemberModel((int)Team.Spoose, "Spoose Goose", Team.Spoose.ToString()),
        };
    }

    public enum Team
    {
        TBD = 0,

        Test = 100,
        Kombo = 200,

        Yes = 300,
        No = 400,

        Maybe = 500,
        Jojo = 600,

        Jiko = 700,
        Flod = 800,

        Wongde = 900,
        Tokolo = 1000,

        Ditch = 1100,
        Spoose = 1200
    }
}
