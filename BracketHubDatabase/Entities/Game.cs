namespace BracketHubDatabase.Entities
{
    public class Game
    {
        public required string Name { get; set; }
        public required string Type { get; set; }
        public string? Description { get; set; }
    }
}
