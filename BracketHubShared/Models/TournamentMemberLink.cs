namespace BracketHubShared.Models
{
    public class TournamentMemberLink
    {
        public required int TournamentId { get; set; }
        public required int MemberId { get; set; }
    }
}
