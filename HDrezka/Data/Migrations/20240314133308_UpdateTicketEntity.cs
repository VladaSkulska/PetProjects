using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HDrezka.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTicketEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_MovieSchedule_MovieScheduleId",
                table: "Seat");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_MovieSchedule_MovieScheduleId",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_MovieScheduleId",
                table: "Ticket");

            migrationBuilder.RenameTable(
                name: "Ticket",
                newName: "Tickets");

            migrationBuilder.RenameColumn(
                name: "MovieScheduleId",
                table: "Seat",
                newName: "CinemaRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Seat_MovieScheduleId",
                table: "Seat",
                newName: "IX_Seat_CinemaRoomId");

            migrationBuilder.RenameColumn(
                name: "SeatNumber",
                table: "Tickets",
                newName: "SeatId");

            migrationBuilder.AddColumn<int>(
                name: "CinemaRoomId",
                table: "MovieSchedule",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PurchaseTime",
                table: "Tickets",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationTime",
                table: "Tickets",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<int>(
                name: "CinemaRoomId",
                table: "Tickets",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CinemaRoom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MaxSeats = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaRoom", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieSchedule_CinemaRoomId",
                table: "MovieSchedule",
                column: "CinemaRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CinemaRoomId",
                table: "Tickets",
                column: "CinemaRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieSchedule_CinemaRoom_CinemaRoomId",
                table: "MovieSchedule",
                column: "CinemaRoomId",
                principalTable: "CinemaRoom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_CinemaRoom_CinemaRoomId",
                table: "Seat",
                column: "CinemaRoomId",
                principalTable: "CinemaRoom",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_CinemaRoom_CinemaRoomId",
                table: "Tickets",
                column: "CinemaRoomId",
                principalTable: "CinemaRoom",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieSchedule_CinemaRoom_CinemaRoomId",
                table: "MovieSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Seat_CinemaRoom_CinemaRoomId",
                table: "Seat");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_CinemaRoom_CinemaRoomId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "CinemaRoom");

            migrationBuilder.DropIndex(
                name: "IX_MovieSchedule_CinemaRoomId",
                table: "MovieSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CinemaRoomId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CinemaRoomId",
                table: "MovieSchedule");

            migrationBuilder.DropColumn(
                name: "CinemaRoomId",
                table: "Tickets");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "Ticket");

            migrationBuilder.RenameColumn(
                name: "CinemaRoomId",
                table: "Seat",
                newName: "MovieScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Seat_CinemaRoomId",
                table: "Seat",
                newName: "IX_Seat_MovieScheduleId");

            migrationBuilder.RenameColumn(
                name: "SeatId",
                table: "Ticket",
                newName: "SeatNumber");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PurchaseTime",
                table: "Ticket",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationTime",
                table: "Ticket",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_MovieScheduleId",
                table: "Ticket",
                column: "MovieScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_MovieSchedule_MovieScheduleId",
                table: "Seat",
                column: "MovieScheduleId",
                principalTable: "MovieSchedule",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_MovieSchedule_MovieScheduleId",
                table: "Ticket",
                column: "MovieScheduleId",
                principalTable: "MovieSchedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
