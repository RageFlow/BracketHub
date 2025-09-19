using BracketHubWeb.Models;
using Microsoft.AspNetCore.Components;

namespace BracketHubWeb.Pages.Games
{
    public class GamePage : ComponentBase
    {
        [Parameter]
        public string? Type { get; set; }

        public static string GamePrefix => "/game/";
        // @page "/game/{Type:string}

        protected string BackgroundUrlStyle => $"height: 60vh; opacity: 0.6; background-image: url({GameModelStatics.BackgroundUrl(Type)}), url(images/CS2-Capture.PNG);";
    }
}
