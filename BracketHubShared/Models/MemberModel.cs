using BracketHubShared.Enums;

namespace BracketHubShared.Models
{
    public class MemberModel
    {
        public int Id { get; set; } = 0;
        public string Name {  get; set; }
        public string Nickname { get; set; }
        
        public MemberModel(int id, string name, string nickname)
        {
            Id = id;
            Name = name;
            Nickname = nickname;
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
}
