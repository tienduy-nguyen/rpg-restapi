using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rpg_Restapi.Migrations
{
    public partial class UsingUuidWeapon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Weapons_WeaponId",
                table: "Characters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Weapons",
                table: "Weapons");

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: new Guid("6f5a9354-b679-4c56-b4e5-fc43ea24bc9e"));

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: new Guid("cbc9cca1-982c-4c49-9847-7e19cf64610d"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Weapons");

            migrationBuilder.AddColumn<Guid>(
                name: "Uuid",
                table: "Weapons",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Weapons",
                table: "Weapons",
                column: "Uuid");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt", "Role" },
                values: new object[] { new byte[] { 243, 228, 117, 194, 52, 218, 170, 27, 243, 50, 179, 239, 64, 114, 107, 85, 52, 3, 211, 39, 254, 167, 91, 212, 123, 126, 90, 205, 187, 194, 61, 75, 156, 131, 142, 76, 0, 106, 181, 144, 62, 202, 138, 69, 6, 112, 251, 129, 193, 130, 4, 136, 17, 7, 146, 83, 138, 175, 21, 217, 52, 111, 206, 181 }, new byte[] { 184, 215, 194, 39, 3, 114, 210, 118, 249, 252, 152, 98, 225, 34, 244, 61, 153, 22, 248, 33, 136, 253, 24, 250, 170, 235, 56, 228, 172, 185, 79, 160, 239, 57, 74, 255, 14, 116, 84, 127, 125, 7, 197, 197, 172, 12, 39, 116, 29, 126, 153, 48, 224, 230, 133, 82, 139, 220, 93, 243, 164, 123, 145, 93, 242, 87, 211, 48, 42, 173, 149, 50, 101, 1, 236, 102, 85, 27, 76, 96, 46, 63, 23, 176, 130, 124, 60, 137, 114, 132, 78, 131, 219, 3, 21, 12, 135, 176, 187, 208, 143, 203, 74, 192, 180, 155, 133, 217, 40, 194, 139, 223, 31, 84, 93, 76, 21, 48, 218, 81, 70, 64, 69, 126, 130, 27, 255, 193 }, "Player" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt", "Role" },
                values: new object[] { new byte[] { 243, 228, 117, 194, 52, 218, 170, 27, 243, 50, 179, 239, 64, 114, 107, 85, 52, 3, 211, 39, 254, 167, 91, 212, 123, 126, 90, 205, 187, 194, 61, 75, 156, 131, 142, 76, 0, 106, 181, 144, 62, 202, 138, 69, 6, 112, 251, 129, 193, 130, 4, 136, 17, 7, 146, 83, 138, 175, 21, 217, 52, 111, 206, 181 }, new byte[] { 184, 215, 194, 39, 3, 114, 210, 118, 249, 252, 152, 98, 225, 34, 244, 61, 153, 22, 248, 33, 136, 253, 24, 250, 170, 235, 56, 228, 172, 185, 79, 160, 239, 57, 74, 255, 14, 116, 84, 127, 125, 7, 197, 197, 172, 12, 39, 116, 29, 126, 153, 48, 224, 230, 133, 82, 139, 220, 93, 243, 164, 123, 145, 93, 242, 87, 211, 48, 42, 173, 149, 50, 101, 1, 236, 102, 85, 27, 76, 96, 46, 63, 23, 176, 130, 124, 60, 137, 114, 132, 78, 131, 219, 3, 21, 12, 135, 176, 187, 208, 143, 203, 74, 192, 180, 155, 133, 217, 40, 194, 139, 223, 31, 84, 93, 76, 21, 48, 218, 81, 70, 64, 69, 126, 130, 27, 255, 193 }, "Player" });

            migrationBuilder.InsertData(
                table: "Weapons",
                columns: new[] { "Uuid", "Damage", "Name" },
                values: new object[,]
                {
                    { new Guid("64ea5ebb-e9b7-4d6a-9ca9-27b25bed2e46"), 20, "The Master Sword" },
                    { new Guid("ed7fb34b-da3a-437d-b53b-0eba6f8617c0"), 5, "Crystal Wand" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Weapons_WeaponId",
                table: "Characters",
                column: "WeaponId",
                principalTable: "Weapons",
                principalColumn: "Uuid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Weapons_WeaponId",
                table: "Characters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Weapons",
                table: "Weapons");

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Uuid",
                keyValue: new Guid("64ea5ebb-e9b7-4d6a-9ca9-27b25bed2e46"));

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Uuid",
                keyValue: new Guid("ed7fb34b-da3a-437d-b53b-0eba6f8617c0"));

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Weapons");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Weapons",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Weapons",
                table: "Weapons",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt", "Role" },
                values: new object[] { new byte[] { 150, 178, 103, 11, 75, 81, 100, 202, 117, 168, 147, 157, 131, 177, 227, 35, 185, 126, 199, 81, 70, 205, 85, 247, 13, 69, 100, 233, 214, 108, 3, 70, 13, 150, 42, 6, 212, 1, 184, 125, 28, 82, 238, 233, 18, 213, 124, 72, 255, 4, 106, 125, 216, 218, 168, 221, 180, 193, 5, 0, 135, 194, 140, 15 }, new byte[] { 169, 47, 248, 21, 228, 147, 219, 53, 104, 191, 15, 141, 146, 119, 205, 246, 240, 3, 224, 243, 180, 188, 2, 177, 179, 139, 121, 151, 240, 204, 18, 71, 93, 17, 222, 33, 91, 138, 128, 198, 159, 78, 122, 11, 255, 235, 69, 202, 75, 87, 10, 135, 8, 146, 207, 242, 156, 62, 45, 48, 229, 139, 250, 116, 113, 39, 249, 214, 155, 72, 13, 90, 118, 77, 21, 97, 134, 2, 211, 85, 4, 178, 67, 29, 179, 144, 227, 3, 89, 152, 146, 203, 63, 213, 253, 147, 134, 70, 196, 180, 207, 220, 242, 91, 77, 10, 68, 109, 51, 193, 227, 176, 40, 128, 234, 237, 31, 207, 142, 169, 38, 18, 231, 99, 213, 73, 10, 121 }, "Player" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt", "Role" },
                values: new object[] { new byte[] { 150, 178, 103, 11, 75, 81, 100, 202, 117, 168, 147, 157, 131, 177, 227, 35, 185, 126, 199, 81, 70, 205, 85, 247, 13, 69, 100, 233, 214, 108, 3, 70, 13, 150, 42, 6, 212, 1, 184, 125, 28, 82, 238, 233, 18, 213, 124, 72, 255, 4, 106, 125, 216, 218, 168, 221, 180, 193, 5, 0, 135, 194, 140, 15 }, new byte[] { 169, 47, 248, 21, 228, 147, 219, 53, 104, 191, 15, 141, 146, 119, 205, 246, 240, 3, 224, 243, 180, 188, 2, 177, 179, 139, 121, 151, 240, 204, 18, 71, 93, 17, 222, 33, 91, 138, 128, 198, 159, 78, 122, 11, 255, 235, 69, 202, 75, 87, 10, 135, 8, 146, 207, 242, 156, 62, 45, 48, 229, 139, 250, 116, 113, 39, 249, 214, 155, 72, 13, 90, 118, 77, 21, 97, 134, 2, 211, 85, 4, 178, 67, 29, 179, 144, 227, 3, 89, 152, 146, 203, 63, 213, 253, 147, 134, 70, 196, 180, 207, 220, 242, 91, 77, 10, 68, 109, 51, 193, 227, 176, 40, 128, 234, 237, 31, 207, 142, 169, 38, 18, 231, 99, 213, 73, 10, 121 }, "Player" });

            migrationBuilder.InsertData(
                table: "Weapons",
                columns: new[] { "Id", "Damage", "Name" },
                values: new object[,]
                {
                    { new Guid("cbc9cca1-982c-4c49-9847-7e19cf64610d"), 20, "The Master Sword" },
                    { new Guid("6f5a9354-b679-4c56-b4e5-fc43ea24bc9e"), 5, "Crystal Wand" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Weapons_WeaponId",
                table: "Characters",
                column: "WeaponId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
