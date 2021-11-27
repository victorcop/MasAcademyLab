using MasAcademyLab.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace MasAcademyLab.Data
{
    public class MasAcademyLabContext : DbContext
    {
        public MasAcademyLabContext(DbContextOptions<MasAcademyLabContext> options) : base(options)
        {
            
        }

        public DbSet<Training> Trainings { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Talk> Talks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder bldr)
        {
            bldr.Entity<Training>()
              .HasData(new
              {
                  TrainingId = 1,
                  Code = "MAS2021",
                  Name = "Mas Academy Code Training",
                  EventDate = new DateTime(2018, 10, 18),
                  LocationId = 1,
                  Length = 1
              });

            bldr.Entity<Location>()
              .HasData(new
              {
                  LocationId = 1,
                  VenueName = "",
                  Address1 = "Calle 5 #45-54",
                  CityTown = "Medellín, Colombia",
                  StateProvince = "ME",
                  PostalCode = "12345",
                  Country = "COL"
              });

            bldr.Entity<Talk>()
              .HasData(new
              {
                  TalkId = 1,
                  TrainingId = 1,
                  SpeakerId = 1,
                  Title = ".Net Core API",
                  Abstract = ".Net Core API in an hour.",
                  Level = 100
              },
              new
              {
                  TalkId = 2,
                  TrainingId = 1,
                  SpeakerId = 2,
                  Title = "Writing Sample Data Made Easy",
                  Abstract = "Thinking of good sample data examples is tiring.",
                  Level = 200
              });

            bldr.Entity<Speaker>()
              .HasData(new
              {
                  SpeakerId = 1,
                  FirstName = "Victor",
                  LastName = "Velasquez",
                  BlogUrl = "",
                  Company = "Mas Global Consulting",
                  CompanyUrl = "https://masglobalconsulting.com/",
                  GitHub = "https://github.com/victorcop",
                  Twitter = "@victorcop55"
              }, new
              {
                  SpeakerId = 2,
                  FirstName = "Francisco",
                  LastName = "Gutierrez",
                  BlogUrl = "",
                  Company = "Mas Global Consulting",
                  CompanyUrl = "https://masglobalconsulting.com/",
                  GitHub = "",
                  Twitter = ""
              });

            bldr.Entity<User>()
              .HasData(new
              {
                  Id = Guid.Parse("ff6f1dd8-680f-4e09-a555-a67343266b38"),
                  Name = "victorcop",
                  Password = "victorcoptest*",
                  Role = "Developer"
              },
              new
              {
                  Id = Guid.Parse("5d7ffb6f-1f8b-4a42-91cd-dce929f159c0"),
                  Name = "victorcop55",
                  Password = "victorcop123*",
                  Role = "Manager"
              });
        }
    }
}
