using Microsoft.AspNetCore.Components;

namespace BracketHubWeb.Pages.Games.Tournament
{
    public class TournamentPage : GamePage
    {
        [Parameter]
        public string? Id { get; set; }
    }
}
