using Microsoft.EntityFrameworkCore;
using WordApp.DataModel.Entities;

namespace WordApp.Repository
{
    public class TextDbContext : DbContext
    {
        public DbSet<Text> Text { get; set; }

        public TextDbContext(DbContextOptions<TextDbContext> options)
             : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Text>()
                .HasData(new Text
                {
                    Id = 1,
                    TextData = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo,"
                });

        }
    }
}
