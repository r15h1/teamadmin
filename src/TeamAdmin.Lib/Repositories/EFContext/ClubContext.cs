using Microsoft.EntityFrameworkCore;

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
        public DbSet<Team> Teams { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetUpClubs(modelBuilder);            
        }

        private void SetUpClubs(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Club>().ToTable("Clubs");
        }
    }
}