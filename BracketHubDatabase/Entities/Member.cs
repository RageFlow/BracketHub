using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BracketHubDatabase.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Nickname { get; set; }

        // Link/Refs
        public List<Match>? Matches { get; set; }
        public List<Tournament>? Tournaments { get; set; }
    }

    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Matches)
                .WithMany(e => e.Members);

            builder.HasMany(e => e.Tournaments)
                .WithMany(e => e.Members);
        }
    }
}
