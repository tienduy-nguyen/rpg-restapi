using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rpg_Restapi.Migrations
{
    public partial class FixRelationWeaponCharacter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_Characters_CharacterId",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_Weapons_CharacterId",
                table: "Weapons");

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: new Guid("246bc21b-d579-4364-bc08-9a6ad8e03a73"));

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: new Guid("6eaf3ce5-b80b-422a-9437-96b08ab8d469"));

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Weapons");

            migrationBuilder.AddColumn<Guid>(
                name: "WeaponId",
                table: "Characters",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateIndex(
                name: "IX_Characters_WeaponId",
                table: "Characters",
                column: "WeaponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Weapons_WeaponId",
                table: "Characters",
                column: "WeaponId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Weapons_WeaponId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_WeaponId",
                table: "Characters");

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: new Guid("6f5a9354-b679-4c56-b4e5-fc43ea24bc9e"));

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: new Guid("cbc9cca1-982c-4c49-9847-7e19cf64610d"));

            migrationBuilder.DropColumn(
                name: "WeaponId",
                table: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "Weapons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_CharacterId",
                table: "Weapons",
                column: "CharacterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Weapons_Characters_CharacterId",
                table: "Weapons",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
