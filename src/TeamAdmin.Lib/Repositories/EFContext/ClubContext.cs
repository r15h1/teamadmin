﻿using Microsoft.EntityFrameworkCore;

namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal class ClubContext:DbContext
    {
        public ClubContext(DbContextOptions<ClubContext> options) : base(options) {}

        /// <summary>
        /// using TeamAdmin.Lib.Repositories.EFContext.Club as the type instead of TeamAdmin.Core.Club because 
        /// EF does not know how to map inner class properties (at the time of writing) to column names
        /// for e.g. Club.Address.Street to Street column when OnModelCreating is fired
        /// </summary>
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<ClubMedia> ClubMedia { get; set; }
        public DbSet<TeamMedia> TeamMedia { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Core.Message> Messages { get; set; }
        public DbSet<Core.Notification> Notifications { get; set; }
        public DbSet<Core.Opponent> Opponents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetUp(modelBuilder);            
        }

        private void SetUp(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClubMedia>().ForSqlServerToTable("ClubMedia")
                .Property(b => b.MediaType).HasColumnName("MediaTypeId");

            modelBuilder.Entity<TeamMedia>().ForSqlServerToTable("TeamMedia")
                .Property(b => b.MediaType).HasColumnName("MediaTypeId");

            modelBuilder.Entity<Player>().ForSqlServerToTable("Players")
                .HasOne(p => p.Team).WithMany(p => p.Players).HasForeignKey(p => p.TeamId);

            modelBuilder.Entity<TeamMedia>().HasOne(p => p.Team)
                .WithMany(p => p.TeamMedia).HasForeignKey(f => f.TeamId);

            modelBuilder.Entity<Core.Message>().ForSqlServerToTable("Messages")
                .Property(b => b.MessageType).HasColumnName("MessageTypeId");

            modelBuilder.Entity<Core.Notification>().ForSqlServerToTable("Notifications").HasKey("NotificationId");
            modelBuilder.Entity<Core.Opponent>().ForSqlServerToTable("Opponents").HasKey("OpponentId");
        }
    }
}