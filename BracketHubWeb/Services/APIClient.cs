using BracketHubShared.Extensions;
using BracketHubShared.Models;
using Newtonsoft.Json;
using System.Text;

namespace BracketHubWeb.Services
{
    public class APIClient
    {
        private readonly HttpClient httpClient;

        public APIClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        internal string GetUrlWithQuery(string url, string? queryName = null, object? query = null)
        {
            if (!string.IsNullOrEmpty(queryName) && query.IsNotNull())
            {
                return $"BracketHub/{url}" + ($"?{queryName}={query}"); // This is a bad way, but will do for Demonstration.
            }
            else
            {
                return $"BracketHub/{url}";
            }
        }

        //using HttpResponseMessage response = await httpClient.PostAsync("todos",jsonContent);

        public async Task<GameModel?> GetGame(string type, CancellationToken cancellationToken = default)
        {
            string url = GetUrlWithQuery(nameof(GetGame), nameof(GameModel.Type), type);
            using HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);

            return await CheckAndConvertResult<GameModel?>(response);
        }
        public async Task<List<GameModel>?> GetGames(CancellationToken cancellationToken = default)
        {
            string url = GetUrlWithQuery(nameof(GetGames));
            using HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);

            return await CheckAndConvertResult<List<GameModel>?>(response);
        }


        public async Task<AdvancedTournamentModel?> GetTournament(int id, CancellationToken cancellationToken = default)
        {
            string url = GetUrlWithQuery(nameof(GetTournament), nameof(AdvancedTournamentModel.Id), id);
            using HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);

            return await CheckAndConvertResult<AdvancedTournamentModel?>(response);
        }
        public async Task<List<GameModel>?> GetTournaments(CancellationToken cancellationToken = default)
        {
            string url = GetUrlWithQuery(nameof(GetTournaments));
            using HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);

            return await CheckAndConvertResult<List<GameModel>?>(response);
        }
        public async Task<TournamentModel?> PutTournament(AdvancedTournamentModel tournament, CancellationToken cancellationToken = default)
        {
            string url = GetUrlWithQuery(nameof(PutTournament));
            using HttpResponseMessage response = await httpClient.PutAsync(url, SerializeModel(tournament), cancellationToken);

            return await CheckAndConvertResult<TournamentModel?>(response);
        }


        #region Internal Functions/Methods
        private StringContent SerializeModel(object model)
        {
            var json = JsonConvert.SerializeObject(model);

            using StringContent jsonContent = new(json, Encoding.UTF8, "application/json");

            return jsonContent;
        }
        private bool CheckResult(HttpResponseMessage response)
        {
            return response.IsSuccessStatusCode;
        }

        private async Task<T?> CheckAndConvertResult<T>(HttpResponseMessage response)
        {
            if (CheckResult(response))
            {
                string responseString = await response.Content.ReadAsStringAsync(); // Internal thought - Why Read as String and not Stream?

                T? result = JsonConvert.DeserializeObject<T?>(responseString);

                return result;
            }

            return default!;
        }
        #endregion
    }
}
