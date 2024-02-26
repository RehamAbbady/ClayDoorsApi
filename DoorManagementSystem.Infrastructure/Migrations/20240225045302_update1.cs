using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DoorManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoorAccess",
                schema: "door_user");

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                schema: "door_user",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                schema: "door_user",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_email",
                schema: "door_user",
                table: "Users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doors_location",
                schema: "door_user",
                table: "Doors",
                column: "location");

            migrationBuilder.CreateIndex(
                name: "IX_DoorLog_access_date_time",
                schema: "door_user",
                table: "DoorLog",
                column: "access_date_time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_email",
                schema: "door_user",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Doors_location",
                schema: "door_user",
                table: "Doors");

            migrationBuilder.DropIndex(
                name: "IX_DoorLog_access_date_time",
                schema: "door_user",
                table: "DoorLog");

            migrationBuilder.DropColumn(
                name: "first_name",
                schema: "door_user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "last_name",
                schema: "door_user",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "DoorAccess",
                schema: "door_user",
                columns: table => new
                {
                    access_role_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    door_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorAccess", x => x.access_role_id);
                    table.ForeignKey(
                        name: "FK_DoorAccess_Doors_door_id",
                        column: x => x.door_id,
                        principalSchema: "door_user",
                        principalTable: "Doors",
                        principalColumn: "door_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoorAccess_Users_user_id",
                        column: x => x.user_id,
                        principalSchema: "door_user",
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoorAccess_door_id",
                schema: "door_user",
                table: "DoorAccess",
                column: "door_id");

            migrationBuilder.CreateIndex(
                name: "IX_DoorAccess_user_id",
                schema: "door_user",
                table: "DoorAccess",
                column: "user_id");
        }
    }
}
