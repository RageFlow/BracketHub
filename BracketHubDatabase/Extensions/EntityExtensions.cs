using BracketHubDatabase.Entities;
using BracketHubShared.Models;

namespace BracketHubDatabase.Extensions
{
    public static class EntityExtensions
    {
        public static MemberModel Convert(this Member member)
        {
            return new MemberModel(member.Id, member.Name, member.Nickname);
        }
        
        public static AdvancedTournamentModel Convert(this Tournament tournament)
        {
            return new AdvancedTournamentModel()
            {
                Id = tournament.Id,
                GameType = tournament.Type,
                Status = tournament.Status,
                Name = tournament.Name,
                Banner = tournament.Banner,
                Date = tournament.Date,
                IsPublic = tournament.IsPublic,
                Description = tournament.Description,
                Members = tournament.Members != null ? tournament.Members.Select(x => x.Convert()).ToList() : null,
                Matches = tournament.Matches != null ? tournament.Matches.Select(x => x.Convert()).ToList() : null,
            };
        }

        public static MatchModel Convert(this Match match)
        {
            return new MatchModel()
            {
                Id = match.Id,
                Status = match.Status,
                Round = match.Round,
                MatchNumber = match.MatchNumber,
                Winner = match.Winner,
                Members = match.Members != null ? match.Members.Select(mem => mem.Id).ToArray() : null,
                ParentMatches = match.ParentMatches != null ? match.ParentMatches.Select(pm => pm.Id).ToArray() : null,
                ChildMatch = match.ChildMatch?.Id,
                Tournament = match.Tournament?.Id
            };
        }
    }
}
