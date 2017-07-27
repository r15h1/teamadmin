using Microsoft.EntityFrameworkCore;

namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal class EventContext : DbContext
    {
        public EventContext(DbContextOptions<EventContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<ClubTeamEvent> ClubTeamEvents { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Opponent> Opponents { get; set; }
        public DbSet<Competition> Competitions{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetUp(modelBuilder);
        }

        private void SetUp(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().ForSqlServerToTable("Events")
                .Property(b => b.EventType).HasColumnName("EventTypeId");

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Opponent)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.OpponentId);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Competition)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.CompetitionId);

            modelBuilder.Entity<ClubTeamEvent>()
                .HasOne(c => c.Event)
                .WithMany(e => e.ClubTeamEvents)
                .HasForeignKey(e => e.EventId).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);

            modelBuilder.Entity<ClubTeamEvent>()
                .HasOne(c => c.Team)
                .WithMany(e => e.ClubTeamEvents)
                .HasForeignKey(e => e.TeamId);

            modelBuilder.Entity<ClubTeamEvent>().ForSqlServerToTable("ClubTeamEvents").HasKey("ClubTeamEventId");
        }
    }
}
