using BracketHubShared.Enums;

namespace BracketHubShared.Models
{
    public class MatchModel
    {
        // Test
        public MatchModel(int? id, Status status, int round, int? matchNumber, int[]? members, int[]? parentMatches, int? childMatch, int? winner)
        {
            Id = id;
            Status = status;

            Round = round;
            MatchNumber = matchNumber;
            Winner = winner;

            // Links
            Members = members;
            ParentMatches = parentMatches;
            ChildMatch = childMatch;
        }
        public MatchModel()
        {
        }

        // Should have something to identify what Parent Round is connected to what Member...

        public int? Id { get; set; }
        public Status Status { get; set; }

        public int? Round { get; set; }
        public int? MatchNumber { get; set; }
        public int? Winner { get; set; }
        
        public int? Tournament { get; set; }

        public int[]? Members { get; set; }
        public int[]? ParentMatches { get; set; }
        public int? ChildMatch { get; set; }
    }

    public static class TestMatchModelStatics
    {
        public static List<MatchModel> MatchListTest = new()
        {
            new MatchModel(2001, Status.Ended, 1, 1, [(int)Team.Test, (int)Team.Kombo], null, 2007, (int)Team.Test),
            new MatchModel(2002, Status.Ended, 1, 2, [(int)Team.Yes, (int)Team.No], null, 2007, (int)Team.No),
            new MatchModel(2003, Status.Ended, 1, 3, [(int)Team.Maybe, (int)Team.Jojo], null, 2008, (int)Team.Maybe),
            new MatchModel(2004, Status.Ended, 1, 4, [(int)Team.Jiko, (int)Team.Flod], null, 2008, (int)Team.Jiko),
            new MatchModel(2005, Status.Ended, 1, 5, [(int)Team.Wongde, (int)Team.Tokolo], null, 2009, (int)Team.Tokolo),
            new MatchModel(2006, Status.Ended, 1, 6, [(int)Team.Ditch, (int)Team.Spoose], null, 2009, (int)Team.Spoose),

            new MatchModel(2007, Status.Running, 2, 7, [(int)Team.Test, (int)Team.No], [2001, 2002], 2010, (int)Team.No),
            new MatchModel(2008, Status.Running, 2, 8, [(int)Team.Maybe, (int)Team.Jiko], [2003, 2004], 2010, (int)Team.Jiko),
            new MatchModel(2009, Status.Ended, 2, 9, [(int)Team.Tokolo, (int)Team.Spoose], [2005, 2006], null, (int)Team.Tokolo),

            new MatchModel(2010, Status.Open, 3, 10, [(int)Team.No, (int)Team.Jiko], [2007, 2008], null, null),
        };
    }
}
