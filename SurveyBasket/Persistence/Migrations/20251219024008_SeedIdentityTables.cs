using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SurveyBasket.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "019b3459-1d08-7535-ac15-5772ce43ae2a", "019b3459-1d08-7535-ac15-577191a38d8e", true, false, "Member", "MEMBER" },
                    { "019b3459-1d08-7535-ac15-577491d6c0e7", "019b3459-1d08-7535-ac15-577371ef735c", false, false, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "019b3353-b2cd-7f04-9228-cdac9313912d", 0, "019b3353-b2cd-7f04-9228-cdad86bf1744", "admin@survey-basket.com", true, "Survey Basket", "Admin", false, null, "ADMIN@SURVEY-BASKET.COM", "ADMIN@SURVEY-BASKET.COM", "AQAAAAIAAYagAAAAEJWOvxVubtGcOcVJ9JrCzDN7jyoaSykdWnjeOW2cR41HtgtxHZO2xpGyHYqUVB2a7g==", null, false, "84849D33108B4241A84FDB94846593D6", false, "admin@survey-basket.com" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permissions", "polls:read", "019b3459-1d08-7535-ac15-577491d6c0e7" },
                    { 2, "permissions", "polls:add", "019b3459-1d08-7535-ac15-577491d6c0e7" },
                    { 3, "permissions", "polls:update", "019b3459-1d08-7535-ac15-577491d6c0e7" },
                    { 4, "permissions", "polls:delete", "019b3459-1d08-7535-ac15-577491d6c0e7" },
                    { 5, "permissions", "questions:read", "019b3459-1d08-7535-ac15-577491d6c0e7" },
                    { 6, "permissions", "questions:add", "019b3459-1d08-7535-ac15-577491d6c0e7" },
                    { 7, "permissions", "questions:update", "019b3459-1d08-7535-ac15-577491d6c0e7" },
                    { 8, "permissions", "users:read", "019b3459-1d08-7535-ac15-577491d6c0e7" },
                    { 9, "permissions", "users:add", "019b3459-1d08-7535-ac15-577491d6c0e7" },
                    { 10, "permissions", "users:update", "019b3459-1d08-7535-ac15-577491d6c0e7" },
                    { 11, "permissions", "roles:read", "019b3459-1d08-7535-ac15-577491d6c0e7" },
                    { 12, "permissions", "roles:add", "019b3459-1d08-7535-ac15-577491d6c0e7" },
                    { 13, "permissions", "roles:update", "019b3459-1d08-7535-ac15-577491d6c0e7" },
                    { 14, "permissions", "results:read", "019b3459-1d08-7535-ac15-577491d6c0e7" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "019b3459-1d08-7535-ac15-577491d6c0e7", "019b3353-b2cd-7f04-9228-cdac9313912d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019b3459-1d08-7535-ac15-5772ce43ae2a");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "019b3459-1d08-7535-ac15-577491d6c0e7", "019b3353-b2cd-7f04-9228-cdac9313912d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019b3459-1d08-7535-ac15-577491d6c0e7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "019b3353-b2cd-7f04-9228-cdac9313912d");
        }
    }
}
