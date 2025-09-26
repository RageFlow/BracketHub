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
        public static List<TournamentModel>? Tournaments { get; private set; }

        public Task? GameTask { get; private set; }
        public Task? TournamentTask { get; private set; }

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
                GameTask = GetGame();
                await GameTask;
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
                TournamentTask = GetTournament();
                await TournamentTask;
            }

            if (ValuesChanged.IsNotNull())
                ValuesChanged.Invoke(this, type);

            await Task.CompletedTask;
        }

        public async Task GetGame()
        {
            Game = Type.IsNotNull() ? await APIClient.GetGame(Type) : null;
        }

        public async Task GetGames()
        {
            if (!Games.IsNotNull() || Games.Count <= 0)
            {
                Games = await APIClient.GetGames();
            }
        }

        public async Task GetTournament()
        {
            Tournament = TournamentId.IsNotNull() ? await APIClient.GetTournament(TournamentId.Value) : null;
        }

        public async Task GetTournaments()
        {
            if (!Tournaments.IsNotNull() || Tournaments.Count <= 0)
            {
                Tournaments = await APIClient.GetTournaments();
            }
        }

        public void Search(string args)
        {
            if (!string.IsNullOrEmpty(args))
            {
                var game = Games.IsNotNull() ? Games.FirstOrDefault(x => x.Type.Contains(args, StringComparison.CurrentCultureIgnoreCase) || x.Name != null && x.Name.Contains(args, StringComparison.CurrentCultureIgnoreCase)) : null;

                if (game.IsNotNull())
                {
                    NavigationManager.NavigateTo(TournamentList.NavigateMe(game.Type));
                }
            }
        }
    }
}
