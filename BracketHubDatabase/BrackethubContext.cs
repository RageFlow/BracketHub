using BracketHubDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BracketHubDatabase
{
    public class BrackethubContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }

        protected BrackethubContext() : base()
        {
        }

        protected BrackethubContext(DbContextOptions<BrackethubContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
