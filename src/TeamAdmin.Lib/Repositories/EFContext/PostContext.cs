using Microsoft.EntityFrameworkCore;

namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal class PostContext : DbContext
    {
        public PostContext(DbContextOptions<PostContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetUp(modelBuilder);
        }

        private void SetUp(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().ForSqlServerToTable("Posts")
                .Property(b => b.PostStatus).HasColumnName("PostStatusId");

            modelBuilder.Entity<PostMedia>().Property(b => b.MediaType).HasColumnName("MediaTypeId");

            modelBuilder.Entity<PostMedia>().HasOne(p => p.Post).WithMany(p => p.PostMedia).HasForeignKey(f => f.PostId);

        }
    }
}
