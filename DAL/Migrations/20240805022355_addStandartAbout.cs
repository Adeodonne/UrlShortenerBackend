using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class addStandartAbout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Constants",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[] { "about_text", "about", "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "admin_user",
                column: "Password",
                value: "$2a$11$YArmf2i3TliC4XxrBWHY3.KHkk0BMWHj.sutbjXBRNRQOZphYJzO6");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Constants",
                keyColumn: "Id",
                keyValue: "about_text");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "admin_user",
                column: "Password",
                value: "$2a$11$bRUZpjMEWz8TBO3mTpgpJO.7eVJh6tUTnKD32O.jKEV7ZPbrswEFe");
        }
    }
}
