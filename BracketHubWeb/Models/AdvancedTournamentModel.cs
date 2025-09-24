using BracketHubWeb.Extensions;

namespace BracketHubWeb.Models
{
    public class AdvancedTournamentModel : TournamentModel
    {
        public string? Description { get; set; }
        public List<MatchModel>? Matches { get; set; }
        public List<MemberModel>? Members { get; set; }
    }

    // temp
    public static class AdvancedTournamentModelConversion
    {
        public static AdvancedTournamentModel? Convert(this TournamentModel? model)
        {
            if (!model.IsNotNull())
                return null;

            return new AdvancedTournamentModel
            {
                Id = model.Id,
                Name = model.Name,
                IsPublic = model.IsPublic,
                GameType = model.GameType,
                Banner = model.Banner,
                Status = model.Status,
                Date = model.Date,

                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean nec mauris lacus. Donec rutrum lacus vel ullamcorper fringilla. Vivamus aliquet augue sit amet ante lacinia, id vulputate mauris condimentum. Pellentesque vel malesuada elit. Nunc consequat luctus arcu vel elementum. Nullam pulvinar lectus quis eros congue consequat quis vitae est. Ut quis finibus nunc, quis aliquet nunc. Integer nunc sem, ornare vel suscipit at, dictum non risus. Quisque euismod luctus placerat. Praesent interdum hendrerit sapien et aliquet. Cras pretium nulla libero, sit amet tincidunt dui elementum et. Suspendisse vitae pellentesque lorem, id suscipit eros. Etiam enim sapien, tincidunt condimentum eros non, scelerisque pharetra velit. Aliquam tincidunt non lectus sit amet aliquet."
            };
        }
    }
}
