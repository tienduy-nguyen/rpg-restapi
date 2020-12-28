using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Rpg_Restapi.Migrations
{
    public partial class UsingGuidForWeapon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Weapons",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt", "Role" },
                values: new object[] { new byte[] { 23, 45, 239, 179, 72, 177, 206, 100, 58, 99, 162, 212, 76, 150, 0, 251, 13, 90, 25, 208, 76, 35, 84, 55, 240, 130, 27, 171, 172, 15, 118, 157, 81, 153, 162, 102, 7, 35, 68, 57, 199, 88, 83, 125, 26, 45, 209, 44, 112, 172, 180, 211, 176, 32, 12, 73, 114, 55, 175, 28, 198, 61, 45, 212 }, new byte[] { 6, 219, 126, 79, 160, 141, 1, 68, 90, 43, 175, 160, 138, 233, 168, 201, 124, 183, 215, 126, 69, 85, 211, 44, 218, 255, 102, 85, 17, 85, 176, 140, 2, 219, 248, 69, 30, 6, 175, 151, 203, 9, 53, 213, 46, 131, 176, 80, 121, 76, 255, 25, 193, 54, 232, 184, 23, 201, 113, 166, 84, 112, 56, 68, 30, 181, 188, 222, 151, 37, 229, 246, 188, 81, 153, 188, 23, 204, 203, 42, 144, 6, 158, 103, 71, 209, 76, 156, 84, 170, 179, 71, 202, 71, 102, 29, 126, 148, 10, 131, 177, 183, 108, 253, 144, 187, 189, 176, 72, 220, 92, 108, 160, 26, 128, 229, 55, 110, 51, 228, 72, 74, 50, 22, 133, 245, 147, 132 }, "Player" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt", "Role" },
                values: new object[] { new byte[] { 23, 45, 239, 179, 72, 177, 206, 100, 58, 99, 162, 212, 76, 150, 0, 251, 13, 90, 25, 208, 76, 35, 84, 55, 240, 130, 27, 171, 172, 15, 118, 157, 81, 153, 162, 102, 7, 35, 68, 57, 199, 88, 83, 125, 26, 45, 209, 44, 112, 172, 180, 211, 176, 32, 12, 73, 114, 55, 175, 28, 198, 61, 45, 212 }, new byte[] { 6, 219, 126, 79, 160, 141, 1, 68, 90, 43, 175, 160, 138, 233, 168, 201, 124, 183, 215, 126, 69, 85, 211, 44, 218, 255, 102, 85, 17, 85, 176, 140, 2, 219, 248, 69, 30, 6, 175, 151, 203, 9, 53, 213, 46, 131, 176, 80, 121, 76, 255, 25, 193, 54, 232, 184, 23, 201, 113, 166, 84, 112, 56, 68, 30, 181, 188, 222, 151, 37, 229, 246, 188, 81, 153, 188, 23, 204, 203, 42, 144, 6, 158, 103, 71, 209, 76, 156, 84, 170, 179, 71, 202, 71, 102, 29, 126, 148, 10, 131, 177, 183, 108, 253, 144, 187, 189, 176, 72, 220, 92, 108, 160, 26, 128, 229, 55, 110, 51, 228, 72, 74, 50, 22, 133, 245, 147, 132 }, "Player" });

            migrationBuilder.InsertData(
                table: "Weapons",
                columns: new[] { "Id", "CharacterId", "Damage", "Name" },
                values: new object[,]
                {
                    { new Guid("246bc21b-d579-4364-bc08-9a6ad8e03a73"), 1, 20, "The Master Sword" },
                    { new Guid("6eaf3ce5-b80b-422a-9437-96b08ab8d469"), 2, 5, "Crystal Wand" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: new Guid("246bc21b-d579-4364-bc08-9a6ad8e03a73"));

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: new Guid("6eaf3ce5-b80b-422a-9437-96b08ab8d469"));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Weapons",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt", "Role" },
                values: new object[] { new byte[] { 53, 76, 51, 8, 188, 13, 52, 44, 250, 181, 127, 229, 58, 176, 108, 7, 9, 187, 170, 0, 209, 212, 32, 202, 147, 75, 41, 207, 54, 227, 191, 144, 149, 221, 37, 22, 119, 171, 33, 239, 198, 187, 230, 90, 73, 187, 225, 240, 218, 151, 127, 235, 108, 112, 246, 27, 94, 41, 95, 91, 111, 98, 116, 26 }, new byte[] { 66, 4, 92, 164, 84, 246, 237, 168, 73, 194, 253, 253, 208, 34, 223, 242, 230, 37, 3, 149, 146, 144, 231, 225, 45, 254, 23, 231, 121, 37, 81, 185, 116, 97, 134, 99, 118, 202, 204, 145, 197, 132, 149, 154, 160, 221, 34, 184, 120, 1, 201, 7, 120, 5, 109, 232, 48, 220, 77, 227, 17, 211, 221, 143, 135, 8, 84, 220, 41, 167, 205, 17, 219, 164, 7, 10, 121, 157, 51, 241, 181, 165, 60, 101, 191, 107, 255, 125, 203, 79, 140, 56, 17, 133, 37, 179, 21, 60, 29, 129, 179, 6, 52, 108, 178, 176, 31, 25, 120, 198, 99, 187, 21, 132, 207, 128, 182, 240, 165, 246, 35, 1, 183, 233, 21, 47, 244, 88 }, "Player" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt", "Role" },
                values: new object[] { new byte[] { 53, 76, 51, 8, 188, 13, 52, 44, 250, 181, 127, 229, 58, 176, 108, 7, 9, 187, 170, 0, 209, 212, 32, 202, 147, 75, 41, 207, 54, 227, 191, 144, 149, 221, 37, 22, 119, 171, 33, 239, 198, 187, 230, 90, 73, 187, 225, 240, 218, 151, 127, 235, 108, 112, 246, 27, 94, 41, 95, 91, 111, 98, 116, 26 }, new byte[] { 66, 4, 92, 164, 84, 246, 237, 168, 73, 194, 253, 253, 208, 34, 223, 242, 230, 37, 3, 149, 146, 144, 231, 225, 45, 254, 23, 231, 121, 37, 81, 185, 116, 97, 134, 99, 118, 202, 204, 145, 197, 132, 149, 154, 160, 221, 34, 184, 120, 1, 201, 7, 120, 5, 109, 232, 48, 220, 77, 227, 17, 211, 221, 143, 135, 8, 84, 220, 41, 167, 205, 17, 219, 164, 7, 10, 121, 157, 51, 241, 181, 165, 60, 101, 191, 107, 255, 125, 203, 79, 140, 56, 17, 133, 37, 179, 21, 60, 29, 129, 179, 6, 52, 108, 178, 176, 31, 25, 120, 198, 99, 187, 21, 132, 207, 128, 182, 240, 165, 246, 35, 1, 183, 233, 21, 47, 244, 88 }, "Player" });

            migrationBuilder.InsertData(
                table: "Weapons",
                columns: new[] { "Id", "CharacterId", "Damage", "Name" },
                values: new object[,]
                {
                    { 1, 1, 20, "The Master Sword" },
                    { 2, 2, 5, "Crystal Wand" }
                });
        }
    }
}
