using BracketHubShared.Extensions;
using BracketHubShared.Models;
using BracketHubWeb.Pages.Games;
using Microsoft.AspNetCore.Components;
using System.Reflection;

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
            Game = Type.IsNotNull() && Games != null ? Games.FirstOrDefault(x => x.Type == Type) : null;

            if (Type.IsNotNull() && !Game.IsNotNull())
            {
                Game = await APIClient.GetGame(Type);
            }
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

        public async Task<bool> UpdateTournament(AdvancedTournamentModel model)
        {
            var tournament = await APIClient.PutTournament(model);
            if (tournament != null && (Tournament == null || Tournament.Id == tournament.Id))
            {
                Tournament = tournament;
            }

            return tournament.IsNotNull();
        }

        public async Task<bool> JoinTournament(TournamentMemberLink model)
        {
            if (model.MemberId.IsNotNull() && model.TournamentId.IsNotNull())
            {
                var tournament = await APIClient.AddTournamentMember(model);
                await GetTournament();
                return Tournament.IsNotNull();
            }
            return false;
        }

        public async Task<bool> PutMatch(MatchModel model)
        {
            var match = await APIClient.PutMatch(model);
            if (Tournament != null && match != null)
            {
                var existingMatch = Tournament.Matches?.FirstOrDefault(x => x.Id == match.Id);
                if (!existingMatch.IsNotNull())
                {
                    //Tournament.Matches ??= new();
                    //Tournament.Matches.Add(match);
                }
                else
                {
                    existingMatch.Status = match.Status;
                    existingMatch.Round = match.Round;
                    existingMatch.MatchNumber = match.MatchNumber;
                    existingMatch.Winner = match.Winner;
                    existingMatch.Members = match.Members;
                    existingMatch.ParentMatches = match.ParentMatches;
                    existingMatch.ChildMatch = match.ChildMatch;
                }
            }
            return match.IsNotNull();
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
