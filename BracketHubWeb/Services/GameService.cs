using BracketHubShared.Extensions;
using BracketHubShared.Models;
using BracketHubWeb.Pages.Games;
using Microsoft.AspNetCore.Components;

namespace BracketHubWeb.Services
{
    public class GameService
    {
        protected NavigationManager NavigationManager { get; set; }
        protected APIClient APIClient { get; set; }
        public GameService(NavigationManager navigationManager, APIClient aPIClient)
        {
            NavigationManager = navigationManager;
            APIClient = aPIClient;
        }

        public string? Type { get; private set; }
        public int? TournamentId { get; private set; }
        public event EventHandler<string?>? ValuesChanged;
        
        public static GameModel? Game { get; private set; }
        public static List<GameModel>? Games { get; private set; }
        public static AdvancedTournamentModel? Tournament { get; private set; }

        public async Task CheckRoute(RouteData routedata)
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
#if DEBUG
                Game = Type.IsNotNull() ? GameModelStatics.GameList.FirstOrDefault(x => x.Type.Equals(Type, StringComparison.CurrentCultureIgnoreCase)) : null;
#else
                Game = Type.IsNotNull() ? await APIClient.GetGame(Type) : null;
#endif
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
#if DEBUG
                Tournament = TournamentId.IsNotNull() ? BaseTournamentModelStatics.TournamentList.FirstOrDefault(x => x.Id.IsNotNull() && x.Id == TournamentId)?.Convert() : null;
#else
                Tournament = TournamentId.IsNotNull() ? await APIClient.GetTournament(TournamentId.Value) : null;
#endif

#if DEBUG
                if (Tournament.IsNotNull())
                {
                    Tournament.Matches = TestMatchModelStatics.MatchListTest;
                    Tournament.Members = MemberModelStatics.MemberList.OrderBy(x => x.Nickname).ToList();
                }
#endif
            }

            if (ValuesChanged.IsNotNull())
                ValuesChanged.Invoke(this, type);

            await Task.CompletedTask;
        }

        public void Search(string args)
        {
            if (!string.IsNullOrEmpty(args))
            {
#if DEBUG
                var game = GameModelStatics.GameList.FirstOrDefault(x => x.Type.Contains(args, StringComparison.CurrentCultureIgnoreCase) || x.Name != null && x.Name.Contains(args, StringComparison.CurrentCultureIgnoreCase));
#else
                var game = Games.IsNotNull() ? Games.FirstOrDefault(x => x.Type.Contains(args, StringComparison.CurrentCultureIgnoreCase) || x.Name != null && x.Name.Contains(args, StringComparison.CurrentCultureIgnoreCase)) : null;
#endif
                if (game.IsNotNull())
                {
                    NavigationManager.NavigateTo(TournamentList.NavigateMe(game.Type));
                }
            }
        }
    }
}
