using BracketHubWeb.Extensions;
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
                
        protected virtual string? BackgroundUrlOverride { get; set; }
        protected string BackgroundUrl => BackgroundUrlOverride.IsNotNull() ? BackgroundUrlOverride : GameModelStatics.BackgroundUrl(Type);
        protected string BackgroundUrlStyle => $"height: 60vh; opacity: 0.4; background-image: url({BackgroundUrl}), url(images/CS2-Capture.PNG);";
    }
}
