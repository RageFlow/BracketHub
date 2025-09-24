using BracketHubShared.Extensions;
using BracketHubShared.Models;
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
        public int? TournamentId { get; private set; }
        public event EventHandler<string?>? ValuesChanged;
        
        public GameModel? Game { get; private set; }
        public static AdvancedTournamentModel? Tournament { get; private set; }

        public void CheckRoute(RouteData routedata)
        {
            string? type = null;
            if (routedata.RouteValues.TryGetValue(nameof(Type), out object? typeValue))
            {
                if (typeValue.IsNotNull() && typeValue is string stringTypeValue)
                    type = stringTypeValue;
                else
                    type = null;
            }
            if (type != Type)
            {                
                Type = type;
                Game = Type.IsNotNull() ? GameModelStatics.GameList.FirstOrDefault(x => x.Type.Equals(Type, StringComparison.CurrentCultureIgnoreCase)) : null;
            }

            int? tournamentId = null;
            if (routedata.RouteValues.TryGetValue(nameof(TournamentId), out object? tournamentValue))
            {
                if (tournamentValue.IsNotNull() && tournamentValue is int intTournamentValue)
                    tournamentId = intTournamentValue;
                else
                    tournamentId = null;
            }
            if (tournamentId != TournamentId)
            {
                TournamentId = tournamentId;
                Tournament = TournamentId.IsNotNull() ? BaseTournamentModelStatics.TournamentList.FirstOrDefault(x => x.Id.IsNotNull() && x.Id == TournamentId)?.Convert() : null;
                
                if (Tournament.IsNotNull())
                {
                    Tournament.Matches = TestMatchModelStatics.MatchListTest;
                    Tournament.Members = MemberModelStatics.MemberList.OrderBy(x => x.Nickname).ToList();
                }
            }

            if (ValuesChanged.IsNotNull())
                ValuesChanged.Invoke(this, type);
        }

        public void Search(string args)
        {
            if (!string.IsNullOrEmpty(args))
            {
                var game = GameModelStatics.GameList.FirstOrDefault(x => x.Type.Contains(args, StringComparison.CurrentCultureIgnoreCase) || x.Name != null && x.Name.Contains(args, StringComparison.CurrentCultureIgnoreCase));
                if (game.IsNotNull())
                {
                    NavigationManager.NavigateTo(TournamentList.NavigateMe(game.Type));
                }
            }
        }
    }
}
