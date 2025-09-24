using BracketHubWeb.Services;
using Microsoft.AspNetCore.Components;

namespace BracketHubWeb.Pages.Games.Tournament
{
    public class TournamentPage : GamePage
    {
        [Parameter]
        public int? TournamentId { get; set; }

        protected override string? BackgroundUrlOverride => GameService.Tournament?.Banner;
    }
}
