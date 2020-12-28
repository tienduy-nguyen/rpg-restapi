using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Rpg_Restapi.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Damage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    Role = table.Column<string>(nullable: false, defaultValue: "Player")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Damage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.Uuid);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    HitPoints = table.Column<int>(nullable: false),
                    Strength = table.Column<int>(nullable: false),
                    Defense = table.Column<int>(nullable: false),
                    Intelligence = table.Column<int>(nullable: false),
                    Class = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    WeaponUuid = table.Column<Guid>(nullable: true),
                    Fights = table.Column<int>(nullable: false),
                    Victories = table.Column<int>(nullable: false),
                    Defeats = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_Weapons_WeaponUuid",
                        column: x => x.WeaponUuid,
                        principalTable: "Weapons",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CharacterSkills",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    SkillId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSkills", x => new { x.CharacterId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_CharacterSkills_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Damage", "Name" },
                values: new object[,]
                {
                    { 1, 30, "Fireball" },
                    { 2, 20, "Frenzy" },
                    { 3, 50, "Blizzard" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "PasswordSalt", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new byte[] { 53, 249, 119, 31, 10, 93, 77, 146, 65, 134, 27, 97, 133, 8, 250, 252, 13, 79, 85, 101, 252, 235, 93, 246, 71, 59, 26, 44, 221, 92, 17, 147, 142, 222, 171, 249, 138, 157, 175, 150, 74, 103, 14, 149, 206, 218, 89, 110, 65, 252, 124, 196, 188, 189, 121, 124, 77, 168, 31, 208, 74, 238, 134, 162 }, new byte[] { 123, 124, 171, 65, 255, 120, 178, 10, 11, 189, 30, 249, 191, 15, 78, 57, 220, 0, 230, 116, 91, 159, 144, 150, 33, 39, 55, 227, 185, 214, 171, 241, 29, 172, 228, 3, 168, 163, 37, 204, 138, 118, 188, 222, 82, 13, 213, 76, 167, 99, 70, 198, 181, 75, 202, 249, 238, 74, 157, 62, 71, 2, 246, 29, 24, 191, 95, 225, 105, 106, 120, 156, 140, 153, 57, 92, 211, 105, 201, 97, 76, 163, 233, 245, 32, 253, 176, 84, 22, 81, 225, 146, 172, 91, 95, 144, 11, 255, 213, 135, 37, 12, 132, 54, 188, 239, 206, 171, 160, 134, 104, 237, 71, 224, 190, 44, 166, 227, 94, 194, 148, 120, 121, 74, 224, 138, 195, 226 }, "Player", "user1" },
                    { 2, new byte[] { 53, 249, 119, 31, 10, 93, 77, 146, 65, 134, 27, 97, 133, 8, 250, 252, 13, 79, 85, 101, 252, 235, 93, 246, 71, 59, 26, 44, 221, 92, 17, 147, 142, 222, 171, 249, 138, 157, 175, 150, 74, 103, 14, 149, 206, 218, 89, 110, 65, 252, 124, 196, 188, 189, 121, 124, 77, 168, 31, 208, 74, 238, 134, 162 }, new byte[] { 123, 124, 171, 65, 255, 120, 178, 10, 11, 189, 30, 249, 191, 15, 78, 57, 220, 0, 230, 116, 91, 159, 144, 150, 33, 39, 55, 227, 185, 214, 171, 241, 29, 172, 228, 3, 168, 163, 37, 204, 138, 118, 188, 222, 82, 13, 213, 76, 167, 99, 70, 198, 181, 75, 202, 249, 238, 74, 157, 62, 71, 2, 246, 29, 24, 191, 95, 225, 105, 106, 120, 156, 140, 153, 57, 92, 211, 105, 201, 97, 76, 163, 233, 245, 32, 253, 176, 84, 22, 81, 225, 146, 172, 91, 95, 144, 11, 255, 213, 135, 37, 12, 132, 54, 188, 239, 206, 171, 160, 134, 104, 237, 71, 224, 190, 44, 166, 227, 94, 194, 148, 120, 121, 74, 224, 138, 195, 226 }, "Player", "user2" }
                });

            migrationBuilder.InsertData(
                table: "Weapons",
                columns: new[] { "Uuid", "Damage", "Name" },
                values: new object[,]
                {
                    { new Guid("e1d0072e-104f-4a5d-9747-8ef374ab308c"), 20, "The Master Sword" },
                    { new Guid("0d876ed4-456f-405d-b31a-9f5dc61bba85"), 5, "Crystal Wand" }
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Class", "Defeats", "Defense", "Fights", "HitPoints", "Intelligence", "Name", "Strength", "UserId", "Victories", "WeaponUuid" },
                values: new object[,]
                {
                    { 1, 1, 0, 10, 0, 100, 10, "Frodo", 15, 1, 0, null },
                    { 2, 2, 0, 5, 0, 100, 20, "Raistlin", 5, 2, 0, null }
                });

            migrationBuilder.InsertData(
                table: "CharacterSkills",
                columns: new[] { "CharacterId", "SkillId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 1 },
                    { 2, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_UserId",
                table: "Characters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_WeaponUuid",
                table: "Characters",
                column: "WeaponUuid");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkills_SkillId",
                table: "CharacterSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterSkills");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Weapons");
        }
    }
}
