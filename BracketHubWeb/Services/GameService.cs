using BracketHubWeb.Extensions;
using Microsoft.AspNetCore.Components;

namespace BracketHubWeb.Services
{
    public class GameService
    {
        public GameService()
        {
        }

        public string? GameType { get; private set; }
        public event EventHandler<string?>? GameChanged;

        public void CheckRoute(RouteData routedata)
        {
            if (routedata.RouteValues.TryGetValue(nameof(GameType), out object? value))
            {
                string? stringValue = null;

                if (value.IsNotNull() && value is string stringvalue)
                    stringValue = stringvalue;
                else
                    stringValue = null;

                if (stringValue != GameType && GameChanged.IsNotNull())
                    GameChanged.Invoke(this, stringValue);

                GameType = stringValue;
            }
        }
    }
}
