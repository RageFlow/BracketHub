using BracketHubShared.CRUD;
using BracketHubShared.Enums;
using BracketHubShared.Extensions;
using BracketHubShared.Models;
using Newtonsoft.Json;
using System.Text;

namespace BracketHubWeb.Services
{
    public class APIClient
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public APIClient(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;

            UrlOffset = configuration.GetSection("CustomSettings").GetValue<string?>("APIOffset");
        }

        public static string? UrlOffset {get; set;}

        internal string GetUrlWithQuery(string url, string? queryName = null, object? query = null)
        {
            if (!string.IsNullOrEmpty(queryName) && query.IsNotNull())
                return $"{UrlOffset}/BracketHub/{url}" + ($"?{queryName}={query}"); // This is a bad way, but will do for Demonstration.
            else
                return $"{UrlOffset}/BracketHub/{url}";
        }

        //using HttpResponseMessage response = await httpClient.PostAsync("todos",jsonContent);

        public async Task<GameModel?> GetGame(string type, CancellationToken cancellationToken = default)
        {
#if DEBUG
            return type.IsNotNull() ? GameModelStatics.GameList.FirstOrDefault(x => x.Type.Equals(type, StringComparison.CurrentCultureIgnoreCase)) : null;
#else
            string url = GetUrlWithQuery(nameof(GetGame), nameof(GameModel.Type), type);
            using HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);

            return await CheckAndConvertResult<GameModel?>(response);
#endif
        }
        public async Task<List<GameModel>?> GetGames(CancellationToken cancellationToken = default)
        {
#if DEBUG
            return GameModelStatics.GameList;
#else
            string url = GetUrlWithQuery(nameof(GetGames));
            using HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);

            return await CheckAndConvertResult<List<GameModel>?>(response);
#endif
        }


        public async Task<AdvancedTournamentModel?> GetTournament(int id, CancellationToken cancellationToken = default)
        {
#if DEBUG
            var Tournament = id.IsNotNull() ? TournamentModelConversion.TournamentList.FirstOrDefault(x => x.Id.IsNotNull() && x.Id == id)?.Convert() : null;
            if (Tournament.IsNotNull())
            {
                Tournament.Matches = TestMatchModelStatics.MatchListTest;
                Tournament.Members = MemberModelStatics.MemberList.OrderBy(x => x.Nickname).ToList();
                //Tournament.Matches = null;
                //Tournament.Members = MemberModelStatics.MemberList.OrderBy(x => x.Nickname).Take(9).ToList();

                Tournament.Members = new()
                {
                    new MemberModel(1, "Julian", "Jugre"),
                    new MemberModel(2, "Dex", "Dex"),
                    new MemberModel(3, "Testing", "Teszt"),
                    new MemberModel(4, "Loo", "Lo"),
                    new MemberModel(5, "Wigg", "Jep"),
                    new MemberModel(6, "Trin", "CRTL"),
                    new MemberModel(7, "HC", "DaneOne"),
                    new MemberModel(8, "Marcus", "Ward"),
                };

                Tournament.Matches = new()
                {
                    new MatchModel(1, 1, 1, [1, 2], [], 7, 1),
                    new MatchModel(2, 1, 2, [3,4], [], 7, 3),

                    new MatchModel(7, 2, 1, [1,3], [1,2], null, 1)
                };
            }
            return Tournament;
#else
            string url = GetUrlWithQuery(nameof(GetTournament), nameof(AdvancedTournamentModel.Id), id);
            using HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);

            return await CheckAndConvertResult<AdvancedTournamentModel?>(response);
#endif
        }
        public async Task<List<TournamentModel>?> GetTournaments(CancellationToken cancellationToken = default)
        {
#if DEBUG
            return TournamentModelConversion.TournamentList;
#else
            string url = GetUrlWithQuery(nameof(GetTournaments));
            using HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);

            return await CheckAndConvertResult<List<TournamentModel>?>(response);
#endif
        }
        public async Task<AdvancedTournamentModel?> PutTournament(AdvancedTournamentModel tournament, CancellationToken cancellationToken = default)
        {
            string url = GetUrlWithQuery(nameof(PutTournament));
            using HttpResponseMessage response = await httpClient.PutAsync(url, SerializeModel(tournament), cancellationToken);

            return await CheckAndConvertResult<AdvancedTournamentModel?>(response);
        }
        public async Task<bool> AddTournamentMember(TournamentMemberLink tournamentMemberLink, CancellationToken cancellationToken = default)
        {
            string url = GetUrlWithQuery(nameof(AddTournamentMember));
            using HttpResponseMessage response = await httpClient.PutAsync(url, SerializeModel(tournamentMemberLink), cancellationToken);

            return response.IsSuccessStatusCode;
        }


        public async Task<MatchModel?> PutMatch(MatchModel match, CancellationToken cancellationToken = default)
        {
            string url = GetUrlWithQuery(nameof(PutMatch));
            using HttpResponseMessage response = await httpClient.PutAsync(url, SerializeModel(match), cancellationToken);

            return await CheckAndConvertResult<MatchModel?>(response);
        }


        public async Task<MemberModel?> MemberSignup(MemberCreateUpdateModel member, CancellationToken cancellationToken = default)
        {
            string url = GetUrlWithQuery(nameof(MemberSignup));
            using HttpResponseMessage response = await httpClient.PutAsync(url, SerializeModel(member), cancellationToken);

            return await CheckAndConvertResult<MemberModel?>(response);
        }
        public async Task<MemberModel?> MemberSignin(MemberReadModel member, CancellationToken cancellationToken = default)
        {
            string url = GetUrlWithQuery(nameof(MemberSignin));
            using HttpResponseMessage response = await httpClient.PostAsync(url, SerializeModel(member), cancellationToken);

            return await CheckAndConvertResult<MemberModel?>(response);
        }


        #region Internal Functions/Methods
        private StringContent SerializeModel(object model)
        {
            var json = JsonConvert.SerializeObject(model);

            StringContent jsonContent = new(json, Encoding.UTF8, "application/json");

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
