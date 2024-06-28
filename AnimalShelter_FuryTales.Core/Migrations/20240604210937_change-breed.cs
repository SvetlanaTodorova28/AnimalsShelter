using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalShelter_FuryTales.Core.Migrations
{
    public partial class changebreed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DonationsTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Breeds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpeciesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Breeds_Species_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "Species",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpeciesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BreedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthlyFoodExpenses = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animals_Breeds_BreedId",
                        column: x => x.BreedId,
                        principalTable: "Breeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Animals_Species_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnimalUser",
                columns: table => new
                {
                    AnimalsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalUser", x => new { x.AnimalsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AnimalUser_Animals_AnimalsId",
                        column: x => x.AnimalsId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonationItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnimalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonationItems_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonationItems_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00000000-0000-0000-0000-000000000020", "b252b734-e3cc-46ed-9cea-3628e9f8e099", "Admin", "ADMIN" },
                    { "00000000-0000-0000-0000-000000000021", "5ba48354-8e44-468c-bf05-150c1fe14d6a", "Volunteer", "VOLUNTEER" },
                    { "00000000-0000-0000-0000-000000000027", "ef5572c6-17ce-4470-97cf-415371bc5327", "Adopter", "ADOPTER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "Ability", "AccessFailedCount", "ConcurrencyStamp", "DonationsTotal", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "00000000-0000-0000-0000-000000000014", "Healthcare course", 0, "YETANOTHERUNIQUESTRING", null, "mila@shelter.bg", true, "Mila", 1, "Nikolova", false, null, "MILA@SHELTER.BG", "MILA@SHELTER.BG", "AQAAAAEAACcQAAAAEMo/nM9GfJtQvce05jvutpBQPX1WvtEGzTtQUIIDfirHmYEamlFR/o/3QaftVXqgBw==", null, false, "https://api.dicebear.com/5.x/avataaars/svg?mouth=default&facialHairProbability=0&top%5B%5D=bigHair,bob,bun,curvy,longButNotTooLong,shaggy,shaggyMullet,shavedSides,straightAndStrand,straight01,straight02&seed=mila", "DIFFERENTUNIQUESTRING", false, "mila@shelter.bg" },
                    { "00000000-0000-0000-0000-000000000015", "Healthcare course", 0, "YETANOTHERUNIQUESTRING", null, "ivan@shelter.bg", true, "Ivan", 0, "Sybev", false, null, "IVAN@SHELTER.BG", "IVAN@SHELTER.BG", "AQAAAAEAACcQAAAAED4RsQiKJhzxUWS94cx4oOYx+uuWt4GMpH5t1a8yexJfE4HxtDGFFZ8owxgpN0Cqgg==", null, false, "https://api.dicebear.com/5.x/avataaars/svg?mouth=default&top%5B%5D=dreads01,dreads02,frizzle,shortCurly,shortFlat,shortRound,shortWaved,sides,theCaesar,theCaesarAndSidePart&seed=ivan", "DIFFERENTUNIQUESTRING", false, "ivan@shelter.bg" },
                    { "00000000-0000-0000-0000-000000000022", "Administration", 0, "4b277cc7-bcb0-4d91-8aab-08dc4b606f7a", null, "Admin@shelter.bg", true, "Admin", 1, null, false, null, "ADMIN@SHELTER.BG", "ADMIN@SHELTER.BG", "AQAAAAEAACcQAAAAEBVfxCGgjSanvz+5atai6XGiJ/u/DTU1LaR7RlIqH9Q3053zzIXNtWMAR3EGoNt/Yw==", null, false, "https://api.dicebear.com/5.x/avataaars/svg?mouth=default&top%5B%5D=dreads01,dreads02,frizzle,shortCurly,shortFlat,shortRound,shortWaved,sides,theCaesar,theCaesarAndSidePart&seed=Admin", "BABUNAPLANINAVHODCHETERI", false, "Admin@shelter.bg" },
                    { "00000000-0000-0000-0000-000000000023", "Healthcare course", 0, "YETANOTHERUNIQUESTRING", null, "penka@shelter.bg", true, "Penka", 1, "Petrova", false, null, "PENKA@SHELTER.BG", "PENKA@SHELTER.BG", "AQAAAAEAACcQAAAAENl2+LAEW2SCes/0+TLjMub7YYrE6YPIABdYtNF+spHFGD/rCU+OAbE5uaPa69wUKg==", null, false, "https://api.dicebear.com/5.x/avataaars/svg?mouth=default&facialHairProbability=0&top%5B%5D=bigHair,bob,bun,curly,curvy,dreads,longButNotTooLong,shaggy,shavedSides,straightAndStrand,straight01,straight02&seed=penka", "DIFFERENTUNIQUESTRING", false, "penka@shelter.bg" },
                    { "00000000-0000-0000-0000-000000000024", "", 0, "YETANOTHERUNIQUESTRING", null, "sarah@gmail.com", true, "Sarah", 1, "Vrout", false, null, "SARAH@GMAIL.COM", "SARAH@GMAIL.COM", "AQAAAAEAACcQAAAAEOVRBtlJwN1okC0ONqKdhPw35jwIpOQlJ9wvYS9mEmmdrdx8Kgy04hfYBPiH0FBUcA==", null, false, "https://api.dicebear.com/8.x/bottts/svg?seed=Sammy", "DIFFERENTUNIQUESTRING", false, "sarah@gmail.com" },
                    { "00000000-0000-0000-0000-000000000025", "", 0, "YETANOTHERUNIQUESTRING", null, "tom@gmail.com", true, "Tom", 0, "Calme", false, null, "TOM@GMAIL.COM", "TOM@GMAIL.COM", "AQAAAAEAACcQAAAAEDU2B05EIjkSKkNYO7VibxZEAOOKfbUyTt4wVe2fE1GKbPIEydGz2YQ+Ni3+j1KCUg==", null, false, "https://api.dicebear.com/8.x/bottts/svg?seed=Sammy", "DIFFERENTUNIQUESTRING", false, "tom@gmail.com" },
                    { "00000000-0000-0000-0000-000000000035", "Cleaning", 0, "YETANOTHERUNIQUESTRING", null, "petyr@shelter.bg", true, "Petyr", 0, "Stoqnov", false, null, "PETYR@SHELTER.BG", "PETYR@SHELTER.BG", "AQAAAAEAACcQAAAAEPqHW7KOJoki6I8+p483YsT5vNlRITbx93weYLFJdQrBIuK5dQAXOET5EiRbumN3hg==", null, false, "https://api.dicebear.com/5.x/avataaars/svg?mouth=default&top%5B%5D=dreads01,dreads02,frizzle,shortCurly,shortFlat,shortRound,shortWaved,sides,theCaesar,theCaesarAndSidePart&seed=petyr", "DIFFERENTUNIQUESTRING", false, "petyr@shelter.bg" }
                });

            migrationBuilder.InsertData(
                table: "Species",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Unknown" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Cat" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "Donkey" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "Bunny" },
                    { new Guid("00000000-0000-0000-0000-000000000033"), "Dog" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "00000000-0000-0000-0000-000000000021", "00000000-0000-0000-0000-000000000014" },
                    { "00000000-0000-0000-0000-000000000021", "00000000-0000-0000-0000-000000000015" },
                    { "00000000-0000-0000-0000-000000000020", "00000000-0000-0000-0000-000000000022" },
                    { "00000000-0000-0000-0000-000000000021", "00000000-0000-0000-0000-000000000023" },
                    { "00000000-0000-0000-0000-000000000027", "00000000-0000-0000-0000-000000000024" },
                    { "00000000-0000-0000-0000-000000000027", "00000000-0000-0000-0000-000000000025" }
                });

            migrationBuilder.InsertData(
                table: "Breeds",
                columns: new[] { "Id", "Name", "SpeciesId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Unknown", new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Stray Dog", new Guid("00000000-0000-0000-0000-000000000033") },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "Stray Cat", new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "Anatolian", new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "Hulstlander", new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000031"), "Labrador", new Guid("00000000-0000-0000-0000-000000000033") }
                });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "BreedId", "Description", "Gender", "Health", "Image", "MonthlyFoodExpenses", "Name", "SpeciesId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000005"), 3, new Guid("00000000-0000-0000-0000-000000000031"), "Friendly and energetic ", 0, 0, "Layko.jpg", 50.00m, "Layko", new Guid("00000000-0000-0000-0000-000000000033") },
                    { new Guid("00000000-0000-0000-0000-000000000006"), 2, new Guid("00000000-0000-0000-0000-000000000009"), "Quiet and curious stray", 1, 0, "Sony.jpg", 45.00m, "Sony", new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000012"), 2, new Guid("00000000-0000-0000-0000-000000000010"), "Gentle, Rescued, Burro", 1, 4, "Sonia.jpg", 45.00m, "Sonia", new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000013"), 3, new Guid("00000000-0000-0000-0000-000000000004"), "Friendly and energetic ", 0, 1, "Joro.jpg", 50.00m, "Joro", new Guid("00000000-0000-0000-0000-000000000033") },
                    { new Guid("00000000-0000-0000-0000-000000000045"), 1, new Guid("00000000-0000-0000-0000-000000000011"), "This gentle, curious rabbit is eagerly waiting for a loving forever home", 0, 0, "Uchcho.jpg", 50.00m, "Uchcho", new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000046"), 11, new Guid("00000000-0000-0000-0000-000000000004"), "This sweet, aging companion, despite his health challenges, still has plenty of love to give and is searching for a compassionate home to spend his golden years", 0, 2, "default.jpg", 150.00m, "Pencho", new Guid("00000000-0000-0000-0000-000000000033") },
                    { new Guid("00000000-0000-0000-0000-000000000047"), 4, new Guid("00000000-0000-0000-0000-000000000011"), "Meet this delightful little bunny! Ready to hop right into your heart and home, this friendly companion promises years of joy and friendship", 0, 0, "Skokcho.jpg", 50.00m, "Skokcho", new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000048"), 2, new Guid("00000000-0000-0000-0000-000000000009"), "This radiant ginger cat is a bundle of energy and affection. Eager for a warm lap to curl up on", 1, 0, "Lyvcho.jpg", 45.00m, "Lyvcho", new Guid("00000000-0000-0000-0000-000000000002") }
                });

            migrationBuilder.InsertData(
                table: "AnimalUser",
                columns: new[] { "AnimalsId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000005"), "00000000-0000-0000-0000-000000000014" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "00000000-0000-0000-0000-000000000023" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "00000000-0000-0000-0000-000000000015" },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "00000000-0000-0000-0000-000000000014" },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "00000000-0000-0000-0000-000000000023" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_BreedId",
                table: "Animals",
                column: "BreedId");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_SpeciesId",
                table: "Animals",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalUser_UsersId",
                table: "AnimalUser",
                column: "UsersId");

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
                name: "IX_Breeds_SpeciesId",
                table: "Breeds",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationItems_AnimalId",
                table: "DonationItems",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationItems_UserId1",
                table: "DonationItems",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalUser");

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
                name: "DonationItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Breeds");

            migrationBuilder.DropTable(
                name: "Species");
        }
    }
}
