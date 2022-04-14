using IMDbMovies.Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataBase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WatchItem> Wathlist { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
  
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<WatchItem>(b =>
            {
                b.HasKey(x => x.Id);
                b.HasOne(x => x.User)
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<WatchItem>(b =>
            {
                b.HasOne(x => x.Movie)
                    .WithMany()
                    .HasForeignKey(x => x.MovieId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
