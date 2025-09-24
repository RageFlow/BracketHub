using BracketHubShared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BracketHubDatabase.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public int? Round { get; set; }
        public int? MatchNumber { get; set; }

        public int? Winner { get; set; }

        // Links/Refs
        public List<Member>? Members { get; set; }
        public List<Match>? ParentMatches { get; set; }
        public Match? ChildMatch { get; set; }
        public Tournament? Tournament { get; set; }
    }

    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Status)
                .HasConversion(e => e.ToString(), e => Enum.Parse<Status>(e));

            builder.HasMany(e => e.Members)
                .WithMany(e => e.Matches);

            builder.HasMany(e => e.ParentMatches)
                .WithOne(e => e.ChildMatch);
            
            builder.HasOne(e => e.ChildMatch)
                .WithMany(e => e.ParentMatches);

            builder.HasOne(e => e.Tournament)
                .WithMany(e => e.Matches);
        }
    }
}
