﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TripTrak.Migrations
{
    public partial class TripTrakTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    IconUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    TripId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trip",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trip_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subcategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    IconUrl = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    PlaceUrl = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    Favorite = table.Column<bool>(nullable: false),
                    SubcategoryId = table.Column<int>(nullable: false),
                    CityId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Place_Subcategory_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalTable: "Subcategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SavedPlace",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlaceId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    UserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedPlace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedPlace_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SavedPlace_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "DisplayName", "FirstName", "LastName" },
                values: new object[] { "860f7f70-cff0-4b4c-82a5-a881583b160f", 0, "c555d6dc-0912-4cfb-8a3b-d50c8747d7c3", "ApplicationUser", "megcruzen@gmail.com", true, false, null, "megcruzen@gmail.com", "MEGCRUZEN@GMAIL.COM", "AQAAAAEAACcQAAAAEGbfbJ9zHTFZDS8i2KsJKO2k8RAq7dqz0rsFbQpuVSoxnd4Iuqy6ShInvcDfaKXQXQ==", null, false, "c727ee8b-cc37-481f-8344-f5f5e90dd534", false, "megcruzen@gmail.com", "megcruzen", "Megan", "Cruzen" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "IconUrl", "Name" },
                values: new object[,]
                {
                    { 1, "", "Eat" },
                    { 2, "", "Stay" },
                    { 3, "", "Do" }
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "EndDate", "ImageUrl", "Name", "Notes", "StartDate", "TripId" },
                values: new object[,]
                {
                    { 1, new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Munich, Germany", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec quam nisi, laoreet nec laoreet id, faucibus in nisi. Aliquam varius tincidunt finibus. Cras pharetra, nulla mattis pulvinar lobortis, orci massa mattis justo, quis lobortis nunc nisi nec orci.", new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Lake Thun, Switzerland", "AMAZING!", new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Seattle, WA", "", new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "Subcategory",
                columns: new[] { "Id", "CategoryId", "IconUrl", "Name" },
                values: new object[,]
                {
                    { 7, 1, "", "Other" },
                    { 13, 3, "", "Nature/Outdoors" },
                    { 12, 3, "", "Arts/Culture" },
                    { 11, 2, "", "Other" },
                    { 10, 2, "", "Bed & Breakfast" },
                    { 9, 2, "", "Airbnb" },
                    { 8, 2, "", "Hotel" },
                    { 14, 3, "", "Special Events" },
                    { 15, 3, "", "Other" },
                    { 5, 1, "", "Bakery" },
                    { 4, 1, "", "Bar" },
                    { 3, 1, "", "Cafe" },
                    { 2, 1, "", "Quick Eats" },
                    { 1, 1, "", "Restaurant" },
                    { 6, 1, "", "Dessert" }
                });

            migrationBuilder.InsertData(
                table: "Trip",
                columns: new[] { "Id", "EndDate", "Name", "Notes", "StartDate", "Summary", "UserId" },
                values: new object[,]
                {
                    { 2, new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alaskan Cruise + Oregon Coast", "Holland America Eurodam, Amanda's Wedding", new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alaskan Cruise (Juneau, Glacier Bay, Sitka, Ketchikan), Oregon Coast, Seattle", "860f7f70-cff0-4b4c-82a5-a881583b160f" },
                    { 1, new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Honeymoon", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec quam nisi, laoreet nec laoreet id, faucibus in nisi. Aliquam varius tincidunt finibus. Cras pharetra, nulla mattis pulvinar lobortis, orci massa mattis justo, quis lobortis nunc nisi nec orci.", new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Munich, Lake Thun, Venice, Cinque Terre, Rome", "860f7f70-cff0-4b4c-82a5-a881583b160f" }
                });

            migrationBuilder.InsertData(
                table: "Place",
                columns: new[] { "Id", "CityId", "EndDate", "Favorite", "Location", "Name", "Notes", "PlaceUrl", "StartDate", "SubcategoryId", "UserId" },
                values: new object[,]
                {
                    { 4, 2, new DateTime(2017, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Marktgasse 2, 3600 Thun", "Trans Asien Restaurant", "Way too expensive!", "", new DateTime(2017, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "860f7f70-cff0-4b4c-82a5-a881583b160f" },
                    { 1, 2, new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Schlossberg 1, 3600 Thun", "Thun Castle", "Absolutely fabulous.", "https://schlossthun.ch/?lang=en", new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "860f7f70-cff0-4b4c-82a5-a881583b160f" },
                    { 2, 2, new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Schloss 4, 3653 Oberhofen am Thunersee", "Oberhofen Castle", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec quam nisi, laoreet nec laoreet id, faucibus in nisi. Aliquam varius tincidunt finibus. Cras pharetra, nulla mattis pulvinar lobortis, orci massa mattis justo, quis lobortis nunc nisi nec orci.", "https://www.schlossoberhofen.ch/en/home", new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, "860f7f70-cff0-4b4c-82a5-a881583b160f" },
                    { 3, 2, new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Seestrasse 974, 3800 Sundlauenen", "St. Beatus Caves", "Cost: CHF 18", "http://www.beatushoehlen.swiss/en/", new DateTime(2017, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, "860f7f70-cff0-4b4c-82a5-a881583b160f" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Place_SubcategoryId",
                table: "Place",
                column: "SubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedPlace_PlaceId",
                table: "SavedPlace",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedPlace_UserId1",
                table: "SavedPlace",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategory_CategoryId",
                table: "Subcategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_UserId",
                table: "Trip",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "SavedPlace");

            migrationBuilder.DropTable(
                name: "Trip");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Subcategory");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
