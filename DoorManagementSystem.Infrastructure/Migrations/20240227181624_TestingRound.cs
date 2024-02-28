using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoorManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TestingRound : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "can_access_logs",
                schema: "door_user",
                table: "roles");

            migrationBuilder.AddColumn<string>(
                name: "pin_hash",
                schema: "door_user",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "tag_code",
                schema: "door_user",
                table: "user_tags",
                type: "character varying(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "admin_role",
                schema: "door_user",
                table: "user_roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_remote_access_requested",
                schema: "door_user",
                table: "door_logs",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pin_hash",
                schema: "door_user",
                table: "users");

            migrationBuilder.DropColumn(
                name: "admin_role",
                schema: "door_user",
                table: "user_roles");

            migrationBuilder.DropColumn(
                name: "is_remote_access_requested",
                schema: "door_user",
                table: "door_logs");

            migrationBuilder.AlterColumn<string>(
                name: "tag_code",
                schema: "door_user",
                table: "user_tags",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(12)",
                oldMaxLength: 12);

            migrationBuilder.AddColumn<bool>(
                name: "can_access_logs",
                schema: "door_user",
                table: "roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
