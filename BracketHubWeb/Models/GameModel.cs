using System.Security.AccessControl;

namespace BracketHubWeb.Models
{
    public class GameModel
    {
        public string? Name { get; set; }
        public string Type { get; set; } = string.Empty;
        public string? Description { get; set; }

        public GameModel(string? name, string type, string? description)
        {
            Name = name;
            Type = type;
            Description = description;
        }
    }

    public static class GameModelStatics
    {
        public static string CoverUrl(string? type) => $"images/covers/{type ?? "default" }-cover.jpg";
        public static string IconUrl(string? type) => $"images/icons/{type ?? "default"}-icon.jpg";
        public static string BackgroundUrl(string? type) => $"images/backgrounds/{type ?? "default"}-background.jpg";
        public static string BannerUrl(string? type) => $"images/banners/{type ?? "default"}-banner.jpg";
        public static string? Name(string? type) => GameList.FirstOrDefault(x => x.Type == type)?.Name;

        public static List<GameModel> GameList = new()
        {
            //Testing the desription of this item cuz why not?
            new GameModel("Other Games","OTHER", ""),
            new GameModel("Counter-Strike 2","CS2", ""),
            new GameModel("Counter-Strike: Source", "CSS", ""),
            new GameModel("Deadlock", "DEADLOCK", ""),
            new GameModel("Football Manager 2024", "FM24", ""),
            new GameModel("Rocket League", "RL", ""),
            new GameModel("Marvel Rivals", "MRIVALS", ""),
            new GameModel("League of Legends", "LOL", ""),
            new GameModel("Rainbox Six: Siege X", "R6S", ""),
            new GameModel("After-H", "AFTERH", ""),
            new GameModel("Apex Legends", "APEX", ""),
            new GameModel("Dota 2", "DOTA2", ""),
            new GameModel("Fortnite", "FORTNITE", ""),
            //Repeat
            new GameModel("Counter-Strike: Source", "CSS", ""),
            new GameModel("Deadlock", "DEADLOCK", ""),
            new GameModel("After-H", "AFTERH", ""),
            new GameModel("Football Manager 2024", "FM24", ""),
            new GameModel("League of Legends", "LOL", ""),
            new GameModel("Dota 2", "DOTA2", ""),
            new GameModel("Counter-Strike 2","CS2", ""),
            new GameModel("Fortnite", "FORTNITE", ""),
            new GameModel("Apex Legends", "APEX", ""),
            new GameModel("Rocket League", "RL", ""),
            new GameModel("Marvel Rivals", "MRIVALS", ""),
            new GameModel("Rainbox Six: Siege X", "R6S", ""),
        };
    }
}
