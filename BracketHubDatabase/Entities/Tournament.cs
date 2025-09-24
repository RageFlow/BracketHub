using BracketHubShared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BracketHubDatabase.Entities
{
    public class Tournament
    {
        public int Id { get; set; }
        public required string Type { get; set; } // Game Type
        public Status Status { get; set; }
        public required string Name { get; set; }
        public string? Banner { get; set; }
        public DateTime? Date { get; set; }
        public bool IsPublic { get; set; } = true;

        public string? Description { get; set; }

        // Links/Refs
        public List<Match>? Matches { get; set; }
        public List<Member>? Members { get; set; }
    }

    public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Status)
                .HasConversion(e => e.ToString(), e => Enum.Parse<Status>(e));

            builder.HasMany(e => e.Matches)
                .WithOne(e => e.Tournament);

            builder.HasMany(e => e.Members)
                .WithMany(e => e.Tournaments);
        }
    }
}
