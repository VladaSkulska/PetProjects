using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HDrezka.Migrations
{
    /// <inheritdoc />
    public partial class AddUserInstToTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserInstId",
                table: "Ticket",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UserInstId",
                table: "Ticket",
                column: "UserInstId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Users_UserInstId",
                table: "Ticket",
                column: "UserInstId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Users_UserInstId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_UserInstId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "UserInstId",
                table: "Ticket");
        }
    }
}
