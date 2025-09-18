using BracketHubWeb.Extensions;
using BracketHubWeb.Models;
using BracketHubWeb.Pages.Games;
using Microsoft.AspNetCore.Components;

namespace BracketHubWeb.Services
{
    public class GameService
    {
        protected NavigationManager NavigationManager { get; set; }
        public GameService(NavigationManager navigationManager)
        {
            NavigationManager = navigationManager;
        }

        public string? Type { get; private set; }
        public event EventHandler<string?>? GameChanged;
        public string? Name => Game?.Name;

        public GameModel? Game { get; private set; }
        public void CheckRoute(RouteData routedata)
        {
            string? type = null;
            if (routedata.RouteValues.TryGetValue(nameof(Type), out object? value))
            {
                if (value.IsNotNull() && value is string stringvalue)
                    type = stringvalue;
                else
                    type = null;
            }

            if (type != Type)
            {
                if (GameChanged.IsNotNull())
                    GameChanged.Invoke(this, type);
                
                Type = type;
                Game = type.IsNotNull() ? GameModelStatics.GameList.FirstOrDefault(x => x.Type.ToLower() == type.ToLower()) : null;
            }
        }

        public void Search(string args)
        {
            if (!string.IsNullOrEmpty(args))
            {
                var game = GameModelStatics.GameList.FirstOrDefault(x => x.Type.ToLower().Contains(args.ToLower()) || x.Name != null && x.Name.ToLower().Contains(args.ToLower()));
                if (game.IsNotNull())
                {
                    NavigationManager.NavigateTo(Dashboard.NavigateMe(game.Type));
                }
            }
        }
    }
}
