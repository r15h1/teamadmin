using Microsoft.EntityFrameworkCore;
using System;

namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal class zzContext : DbContext
    {
        public zzContext(DbContextOptions<zzContext> options) : base(options) { }

        public DbSet<zzFormData> FormData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetUp(modelBuilder);
        }

        private void SetUp(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<zzFormData>().ForSqlServerToTable("zzFormData").HasKey("FormDataId");
        }
    }

    internal class TryOut
    {
        public int FormDataId { get; set; }
        public int FormId { get; set; }
        public string Data { get; set; }
        public DateTime DateCreated { get; set; }
        public bool? Viewed { get; set; }
    }
}
