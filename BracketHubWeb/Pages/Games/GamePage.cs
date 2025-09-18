using Microsoft.AspNetCore.Components;

namespace BracketHubWeb.Pages.Games
{
    public class GamePage : ComponentBase
    {
        [Parameter]
        public string? GameType { get; set; }

        // @page "/game/{GameType}
    }
}
