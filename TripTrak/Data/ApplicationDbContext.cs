using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TripTrak.Models;

namespace TripTrak.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Place> Place { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Subcategory> Subcategory { get; set; }
        public DbSet<SavedPlace> SavedPlace { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Restrict deletion of related place when SavedPlace entry is removed
            modelBuilder.Entity<Place>()
                .HasMany(p => p.SavedPlaces)
                .WithOne(l => l.Place)
                .OnDelete(DeleteBehavior.Restrict);

            // Restrict deletion of related User when SavedPlace entry is removed
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.SavedPlaces)
                .WithOne(l => l.User)
                .OnDelete(DeleteBehavior.Restrict);

            ApplicationUser user = new ApplicationUser
            {
                FirstName = "Megan",
                LastName = "Cruzen",
                DisplayName = "megcruzen",
                UserName = "megcruzen@gmail.com",
                NormalizedUserName = "MEGCRUZEN@GMAIL.COM",
                Email = "megcruzen@gmail.com",
                NormalizedEmail = "megcruzen@gmail.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Password1!");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            modelBuilder.Entity<Trip>().HasData(
                new Trip()
                {
                    Id = 1,
                    Name = "Honeymoon",
                    StartDate = new DateTime(2017, 06, 09),
                    EndDate = new DateTime(2017, 06, 09),
                    Summary = "Munich, Lake Thun, Venice, Cinque Terre, Rome",
                    Notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec quam nisi, laoreet nec laoreet id, faucibus in nisi. Aliquam varius tincidunt finibus. Cras pharetra, nulla mattis pulvinar lobortis, orci massa mattis justo, quis lobortis nunc nisi nec orci.",
                    UserId = user.Id
                },
                new Trip()
                {
                    Id = 2,
                    Name = "Alaskan Cruise + Oregon Coast",
                    StartDate = new DateTime(2017, 06, 09),
                    EndDate = new DateTime(2017, 06, 09),
                    Summary = "Alaskan Cruise (Juneau, Glacier Bay, Sitka, Ketchikan), Oregon Coast, Seattle",
                    Notes = "Holland America Eurodam, Amanda's Wedding",
                    UserId = user.Id
                }
            );

            modelBuilder.Entity<City>().HasData(
                new City()
                {
                    Id = 1,
                    Name = "Munich, Germany",
                    StartDate = new DateTime(2017, 06, 09),
                    EndDate = new DateTime(2017, 06, 09),
                    ImageUrl = "",
                    Notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec quam nisi, laoreet nec laoreet id, faucibus in nisi. Aliquam varius tincidunt finibus. Cras pharetra, nulla mattis pulvinar lobortis, orci massa mattis justo, quis lobortis nunc nisi nec orci.",
                    TripId = 1
                },
                new City()
                {
                    Id = 2,
                    Name = "Lake Thun, Switzerland",
                    StartDate = new DateTime(2017, 06, 09),
                    EndDate = new DateTime(2017, 06, 09),
                    ImageUrl = "",
                    Notes = "AMAZING!",
                    TripId = 1
                },
                new City()
                {
                    Id = 3,
                    Name = "Seattle, WA",
                    StartDate = new DateTime(2017, 06, 09),
                    EndDate = new DateTime(2017, 06, 09),
                    ImageUrl = "",
                    Notes = "",
                    TripId = 2
                }
            );

            modelBuilder.Entity<Place>().HasData(
                new Place()
                {
                    Id = 1,
                    Name = "Thun Castle",
                    StartDate = new DateTime(2017, 06, 09),
                    EndDate = new DateTime(2017, 06, 09),
                    Location = "Schlossberg 1, 3600 Thun",
                    PlaceUrl = "https://schlossthun.ch/?lang=en",
                    Notes = "Absolutely fabulous.",
                    CityId = 2,
                    SubcategoryId = 8,
                    UserId = user.Id
                },
                new Place()
                {
                Id = 2,
                    Name = "Oberhofen Castle",
                    StartDate = new DateTime(2017, 06, 09),
                    EndDate = new DateTime(2017, 06, 09),
                    Location = "Schloss 4, 3653 Oberhofen am Thunersee",
                    PlaceUrl = "https://www.schlossoberhofen.ch/en/home",
                    Notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec quam nisi, laoreet nec laoreet id, faucibus in nisi. Aliquam varius tincidunt finibus. Cras pharetra, nulla mattis pulvinar lobortis, orci massa mattis justo, quis lobortis nunc nisi nec orci.",
                    CityId = 2,
                    SubcategoryId = 12,
                    UserId = user.Id
                },
                new Place()
                {
                    Id = 3,
                    Name = "St. Beatus Caves",
                    StartDate = new DateTime(2017, 06, 09),
                    EndDate = new DateTime(2017, 06, 09),
                    Location = "Seestrasse 974, 3800 Sundlauenen",
                    PlaceUrl = "http://www.beatushoehlen.swiss/en/",
                    Notes = "Cost: CHF 18",
                    CityId = 2,
                    SubcategoryId = 14,
                    UserId = user.Id
                },
                new Place()
                {
                    Id = 4,
                    Name = "Trans Asien Restaurant",
                    StartDate = new DateTime(2017, 06, 07),
                    EndDate = new DateTime(2017, 06, 07),
                    Location = "Marktgasse 2, 3600 Thun",
                    PlaceUrl = "",
                    Notes = "Way too expensive!",
                    CityId = 2,
                    SubcategoryId = 1,
                    UserId = user.Id
                }
                );

            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    Name = "Eat",
                    IconUrl = ""
                },
                new Category()
                {
                    Id = 2,
                    Name = "Stay",
                    IconUrl = ""
                },
                new Category()
                {
                    Id = 3,
                    Name = "Do",
                    IconUrl = ""
                }
            );

            modelBuilder.Entity<Subcategory>().HasData(
                new Subcategory()
                {
                    Id = 1,
                    Name = "Restaurant",
                    CategoryId = 1,
                    IconUrl = ""
                },
                new Subcategory()
                {
                    Id = 2,
                    Name = "Quick Eats",
                    CategoryId = 1,
                    IconUrl = ""
                },
                new Subcategory()
                {
                    Id = 3,
                    Name = "Cafe",
                    CategoryId = 1,
                    IconUrl = ""
                },
                new Subcategory()
                {
                    Id = 4,
                    Name = "Bar",
                    CategoryId = 1,
                    IconUrl = ""
                },
                new Subcategory()
                {
                    Id = 5,
                    Name = "Bakery",
                    CategoryId = 1,
                    IconUrl = ""
                },
                new Subcategory()
                {
                    Id = 6,
                    Name = "Dessert",
                    CategoryId = 1,
                    IconUrl = ""
                },
                new Subcategory()
                {
                    Id = 7,
                    Name = "Other",
                    CategoryId = 1,
                    IconUrl = ""
                },
                new Subcategory()
                {
                    Id = 8,
                    Name = "Hotel",
                    CategoryId = 2,
                    IconUrl = ""
                },
                new Subcategory()
                {
                    Id = 9,
                    Name = "Airbnb",
                    CategoryId = 2,
                    IconUrl = ""
                },
                new Subcategory()
                {
                    Id = 10,
                    Name = "Bed & Breakfast",
                    CategoryId = 2,
                    IconUrl = ""
                },
                new Subcategory()
                {
                    Id = 11,
                    Name = "Other",
                    CategoryId = 2,
                    IconUrl = ""
                },
                new Subcategory()
                {
                    Id = 12,
                    Name = "Arts/Culture",
                    CategoryId = 3,
                    IconUrl = ""
                },
                new Subcategory()
                {
                    Id = 13,
                    Name = "Nature/Outdoors",
                    CategoryId = 3,
                    IconUrl = ""
                },
                new Subcategory()
                {
                    Id = 14,
                    Name = "Special Events",
                    CategoryId = 3,
                    IconUrl = ""
                },
                new Subcategory()
                {
                    Id = 15,
                    Name = "Other",
                    CategoryId = 3,
                    IconUrl = ""
                }
            );
        }
    }
}
