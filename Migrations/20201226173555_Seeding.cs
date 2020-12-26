using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rpg_Restapi.Migrations
{
    public partial class Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "PasswordSalt", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new byte[] { 82, 208, 159, 140, 109, 73, 41, 127, 23, 55, 206, 4, 151, 14, 182, 66, 39, 56, 93, 6, 124, 153, 247, 137, 117, 17, 57, 237, 94, 253, 4, 65, 158, 9, 28, 254, 147, 52, 116, 109, 169, 231, 14, 55, 11, 126, 220, 184, 64, 247, 31, 231, 120, 187, 182, 131, 129, 10, 63, 4, 134, 169, 91, 12 }, new byte[] { 228, 70, 203, 61, 64, 148, 221, 31, 69, 161, 75, 234, 178, 93, 20, 159, 118, 222, 192, 249, 33, 131, 66, 202, 161, 197, 131, 136, 167, 97, 252, 60, 70, 143, 63, 124, 235, 79, 174, 38, 232, 34, 75, 40, 171, 20, 124, 249, 8, 77, 39, 20, 15, 211, 93, 226, 124, 44, 121, 205, 154, 130, 135, 140, 172, 168, 35, 151, 174, 130, 134, 246, 41, 40, 107, 167, 130, 225, 30, 152, 167, 94, 132, 118, 251, 72, 56, 43, 60, 94, 7, 15, 100, 57, 178, 24, 233, 45, 11, 138, 74, 188, 66, 214, 72, 102, 171, 81, 107, 141, 186, 40, 62, 7, 102, 59, 56, 88, 163, 152, 33, 186, 225, 62, 202, 6, 10, 52 }, "Player", "user1" },
                    { 2, new byte[] { 82, 208, 159, 140, 109, 73, 41, 127, 23, 55, 206, 4, 151, 14, 182, 66, 39, 56, 93, 6, 124, 153, 247, 137, 117, 17, 57, 237, 94, 253, 4, 65, 158, 9, 28, 254, 147, 52, 116, 109, 169, 231, 14, 55, 11, 126, 220, 184, 64, 247, 31, 231, 120, 187, 182, 131, 129, 10, 63, 4, 134, 169, 91, 12 }, new byte[] { 228, 70, 203, 61, 64, 148, 221, 31, 69, 161, 75, 234, 178, 93, 20, 159, 118, 222, 192, 249, 33, 131, 66, 202, 161, 197, 131, 136, 167, 97, 252, 60, 70, 143, 63, 124, 235, 79, 174, 38, 232, 34, 75, 40, 171, 20, 124, 249, 8, 77, 39, 20, 15, 211, 93, 226, 124, 44, 121, 205, 154, 130, 135, 140, 172, 168, 35, 151, 174, 130, 134, 246, 41, 40, 107, 167, 130, 225, 30, 152, 167, 94, 132, 118, 251, 72, 56, 43, 60, 94, 7, 15, 100, 57, 178, 24, 233, 45, 11, 138, 74, 188, 66, 214, 72, 102, 171, 81, 107, 141, 186, 40, 62, 7, 102, 59, 56, 88, 163, 152, 33, 186, 225, 62, 202, 6, 10, 52 }, "Player", "user2" }
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Class", "Defeats", "Defense", "Fights", "HitPoints", "Intelligence", "Name", "Strength", "UserId", "Victories" },
                values: new object[,]
                {
                    { 1, 1, 0, 10, 0, 100, 10, "Frodo", 15, 1, 0 },
                    { 2, 2, 0, 5, 0, 100, 20, "Raistlin", 5, 2, 0 }
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

            migrationBuilder.InsertData(
                table: "Weapons",
                columns: new[] { "Id", "CharacterId", "Damage", "Name" },
                values: new object[,]
                {
                    { 1, 1, 20, "The Master Sword" },
                    { 2, 2, 5, "Crystal Wand" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CharacterSkills",
                keyColumns: new[] { "CharacterId", "SkillId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "CharacterSkills",
                keyColumns: new[] { "CharacterId", "SkillId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "CharacterSkills",
                keyColumns: new[] { "CharacterId", "SkillId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
