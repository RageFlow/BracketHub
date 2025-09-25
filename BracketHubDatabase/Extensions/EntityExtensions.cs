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
            };
        }
    }
}
