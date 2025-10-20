using DiaryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DiaryApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DiaryEntry>().HasData
                (
                    new DiaryEntry
                    {
                        Id = 1,
                        Title = "Restaurant with my Friends",
                        Content = "Went to the restaurant to eat with my friends. We ate hamburguers, pizzas, only junk food",
                        Created = new DateTime(2025, 10, 15)
                    },
                    new DiaryEntry
                    {
                        Id = 2,
                        Title = "Learning back-end",
                        Content = "Today, I learned many things about the back-end development, mainly how does MVC work",
                        Created = new DateTime(2025, 10, 16)
                    },
                    new DiaryEntry
                    {
                        Id = 3,
                        Title = "Developing some projects",
                        Content = "I've started practicing my knowledge at coding, designing some projects on the front-end. Then, I'm willing to create the back-end logic, finally putting my knowledge on practice",
                        Created = new DateTime(2025, 10, 17)
                    }
                );
        }

        public DbSet<DiaryEntry> DiaryEntries { get; set; }
    }
}
