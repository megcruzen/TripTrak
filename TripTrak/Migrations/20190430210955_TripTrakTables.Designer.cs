﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TripTrak.Data;

namespace TripTrak.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190430210955_TripTrakTables")]
    partial class TripTrakTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TripTrak.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IconUrl");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IconUrl = "",
                            Name = "Eat"
                        },
                        new
                        {
                            Id = 2,
                            IconUrl = "",
                            Name = "Stay"
                        },
                        new
                        {
                            Id = 3,
                            IconUrl = "",
                            Name = "Do"
                        });
                });

            modelBuilder.Entity("TripTrak.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EndDate");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Notes");

                    b.Property<DateTime?>("StartDate");

                    b.Property<int>("TripId");

                    b.HasKey("Id");

                    b.ToTable("City");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EndDate = new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageUrl = "",
                            Name = "Munich, Germany",
                            Notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec quam nisi, laoreet nec laoreet id, faucibus in nisi. Aliquam varius tincidunt finibus. Cras pharetra, nulla mattis pulvinar lobortis, orci massa mattis justo, quis lobortis nunc nisi nec orci.",
                            StartDate = new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TripId = 1
                        },
                        new
                        {
                            Id = 2,
                            EndDate = new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageUrl = "",
                            Name = "Lake Thun, Switzerland",
                            Notes = "AMAZING!",
                            StartDate = new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TripId = 1
                        },
                        new
                        {
                            Id = 3,
                            EndDate = new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageUrl = "",
                            Name = "Seattle, WA",
                            Notes = "",
                            StartDate = new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TripId = 2
                        });
                });

            modelBuilder.Entity("TripTrak.Models.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId");

                    b.Property<DateTime?>("EndDate");

                    b.Property<bool>("Favorite");

                    b.Property<string>("Location");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Notes");

                    b.Property<string>("PlaceUrl");

                    b.Property<DateTime?>("StartDate");

                    b.Property<int>("SubcategoryId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("SubcategoryId");

                    b.ToTable("Place");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = 2,
                            EndDate = new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Favorite = false,
                            Location = "Schlossberg 1, 3600 Thun",
                            Name = "Thun Castle",
                            Notes = "Absolutely fabulous.",
                            PlaceUrl = "https://schlossthun.ch/?lang=en",
                            StartDate = new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SubcategoryId = 8,
                            UserId = "860f7f70-cff0-4b4c-82a5-a881583b160f"
                        },
                        new
                        {
                            Id = 2,
                            CityId = 2,
                            EndDate = new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Favorite = false,
                            Location = "Schloss 4, 3653 Oberhofen am Thunersee",
                            Name = "Oberhofen Castle",
                            Notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec quam nisi, laoreet nec laoreet id, faucibus in nisi. Aliquam varius tincidunt finibus. Cras pharetra, nulla mattis pulvinar lobortis, orci massa mattis justo, quis lobortis nunc nisi nec orci.",
                            PlaceUrl = "https://www.schlossoberhofen.ch/en/home",
                            StartDate = new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SubcategoryId = 12,
                            UserId = "860f7f70-cff0-4b4c-82a5-a881583b160f"
                        },
                        new
                        {
                            Id = 3,
                            CityId = 2,
                            EndDate = new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Favorite = false,
                            Location = "Seestrasse 974, 3800 Sundlauenen",
                            Name = "St. Beatus Caves",
                            Notes = "Cost: CHF 18",
                            PlaceUrl = "http://www.beatushoehlen.swiss/en/",
                            StartDate = new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SubcategoryId = 14,
                            UserId = "860f7f70-cff0-4b4c-82a5-a881583b160f"
                        },
                        new
                        {
                            Id = 4,
                            CityId = 2,
                            EndDate = new DateTime(2017, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Favorite = false,
                            Location = "Marktgasse 2, 3600 Thun",
                            Name = "Trans Asien Restaurant",
                            Notes = "Way too expensive!",
                            PlaceUrl = "",
                            StartDate = new DateTime(2017, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SubcategoryId = 1,
                            UserId = "860f7f70-cff0-4b4c-82a5-a881583b160f"
                        });
                });

            modelBuilder.Entity("TripTrak.Models.SavedPlace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PlaceId");

                    b.Property<int>("UserId");

                    b.Property<string>("UserId1");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.HasIndex("UserId1");

                    b.ToTable("SavedPlace");
                });

            modelBuilder.Entity("TripTrak.Models.Subcategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<string>("IconUrl");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Subcategory");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            IconUrl = "",
                            Name = "Restaurant"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            IconUrl = "",
                            Name = "Quick Eats"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 1,
                            IconUrl = "",
                            Name = "Cafe"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 1,
                            IconUrl = "",
                            Name = "Bar"
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 1,
                            IconUrl = "",
                            Name = "Bakery"
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 1,
                            IconUrl = "",
                            Name = "Dessert"
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 1,
                            IconUrl = "",
                            Name = "Other"
                        },
                        new
                        {
                            Id = 8,
                            CategoryId = 2,
                            IconUrl = "",
                            Name = "Hotel"
                        },
                        new
                        {
                            Id = 9,
                            CategoryId = 2,
                            IconUrl = "",
                            Name = "Airbnb"
                        },
                        new
                        {
                            Id = 10,
                            CategoryId = 2,
                            IconUrl = "",
                            Name = "Bed & Breakfast"
                        },
                        new
                        {
                            Id = 11,
                            CategoryId = 2,
                            IconUrl = "",
                            Name = "Other"
                        },
                        new
                        {
                            Id = 12,
                            CategoryId = 3,
                            IconUrl = "",
                            Name = "Arts/Culture"
                        },
                        new
                        {
                            Id = 13,
                            CategoryId = 3,
                            IconUrl = "",
                            Name = "Nature/Outdoors"
                        },
                        new
                        {
                            Id = 14,
                            CategoryId = 3,
                            IconUrl = "",
                            Name = "Special Events"
                        },
                        new
                        {
                            Id = 15,
                            CategoryId = 3,
                            IconUrl = "",
                            Name = "Other"
                        });
                });

            modelBuilder.Entity("TripTrak.Models.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EndDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Notes");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("Summary");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Trip");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EndDate = new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Honeymoon",
                            Notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec quam nisi, laoreet nec laoreet id, faucibus in nisi. Aliquam varius tincidunt finibus. Cras pharetra, nulla mattis pulvinar lobortis, orci massa mattis justo, quis lobortis nunc nisi nec orci.",
                            StartDate = new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Summary = "Munich, Lake Thun, Venice, Cinque Terre, Rome",
                            UserId = "860f7f70-cff0-4b4c-82a5-a881583b160f"
                        },
                        new
                        {
                            Id = 2,
                            EndDate = new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Alaskan Cruise + Oregon Coast",
                            Notes = "Holland America Eurodam, Amanda's Wedding",
                            StartDate = new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Summary = "Alaskan Cruise (Juneau, Glacier Bay, Sitka, Ketchikan), Oregon Coast, Seattle",
                            UserId = "860f7f70-cff0-4b4c-82a5-a881583b160f"
                        });
                });

            modelBuilder.Entity("TripTrak.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("DisplayName")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasDiscriminator().HasValue("ApplicationUser");

                    b.HasData(
                        new
                        {
                            Id = "860f7f70-cff0-4b4c-82a5-a881583b160f",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "c555d6dc-0912-4cfb-8a3b-d50c8747d7c3",
                            Email = "megcruzen@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "megcruzen@gmail.com",
                            NormalizedUserName = "MEGCRUZEN@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEGbfbJ9zHTFZDS8i2KsJKO2k8RAq7dqz0rsFbQpuVSoxnd4Iuqy6ShInvcDfaKXQXQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "c727ee8b-cc37-481f-8344-f5f5e90dd534",
                            TwoFactorEnabled = false,
                            UserName = "megcruzen@gmail.com",
                            DisplayName = "megcruzen",
                            FirstName = "Megan",
                            LastName = "Cruzen"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TripTrak.Models.Place", b =>
                {
                    b.HasOne("TripTrak.Models.Subcategory", "Subcategory")
                        .WithMany()
                        .HasForeignKey("SubcategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TripTrak.Models.SavedPlace", b =>
                {
                    b.HasOne("TripTrak.Models.Place", "Place")
                        .WithMany("SavedPlaces")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TripTrak.Models.ApplicationUser", "User")
                        .WithMany("SavedPlaces")
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("TripTrak.Models.Subcategory", b =>
                {
                    b.HasOne("TripTrak.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TripTrak.Models.Trip", b =>
                {
                    b.HasOne("TripTrak.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}