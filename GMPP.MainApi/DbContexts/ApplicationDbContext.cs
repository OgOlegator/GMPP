using GMPP.MainApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GMPP.MainApi.DbContexts
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Vacancy> Vacancies { get; set; }

        public DbSet<JobResponse> JobPostings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Project>()
                .Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusProject)Enum.Parse(typeof(StatusProject), v));

            modelBuilder
                .Entity<Project>()
                .Property(e => e.Type)
                .HasConversion(
                    v => v.ToString(),
                    v => (TypeProject)Enum.Parse(typeof(TypeProject), v));

            modelBuilder
                .Entity<Project>()
                .Property(e => e.Level)
                .HasConversion(
                    v => v.ToString(),
                    v => (LevelProject)Enum.Parse(typeof(LevelProject), v));

            modelBuilder
                .Entity<Vacancy>()
                .Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusVacancy)Enum.Parse(typeof(StatusVacancy), v));

            modelBuilder
                .Entity<JobResponse>()
                .Property(e => e.State)
                .HasConversion(
                    v => v.ToString(),
                    v => (StateJobPosting)Enum.Parse(typeof(StateJobPosting), v));
        }

    }
}
