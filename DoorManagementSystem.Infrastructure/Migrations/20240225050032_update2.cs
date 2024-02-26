using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DoorManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleDoor_Doors_door_id",
                schema: "door_user",
                table: "RoleDoor");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleDoor_Roles_role_id",
                schema: "door_user",
                table: "RoleDoor");

            migrationBuilder.DropTable(
                name: "DoorLog",
                schema: "door_user");

            migrationBuilder.DropTable(
                name: "user_tole",
                schema: "door_user");

            migrationBuilder.DropTable(
                name: "UserTag",
                schema: "door_user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                schema: "door_user",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                schema: "door_user",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doors",
                schema: "door_user",
                table: "Doors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleDoor",
                schema: "door_user",
                table: "RoleDoor");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "door_user",
                newName: "users",
                newSchema: "door_user");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "door_user",
                newName: "roles",
                newSchema: "door_user");

            migrationBuilder.RenameTable(
                name: "Doors",
                schema: "door_user",
                newName: "doors",
                newSchema: "door_user");

            migrationBuilder.RenameTable(
                name: "RoleDoor",
                schema: "door_user",
                newName: "role_door",
                newSchema: "door_user");

            migrationBuilder.RenameIndex(
                name: "IX_Users_email",
                schema: "door_user",
                table: "users",
                newName: "IX_users_email");

            migrationBuilder.RenameIndex(
                name: "IX_Doors_location",
                schema: "door_user",
                table: "doors",
                newName: "IX_doors_location");

            migrationBuilder.RenameIndex(
                name: "IX_RoleDoor_role_id",
                schema: "door_user",
                table: "role_door",
                newName: "IX_role_door_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_RoleDoor_door_id",
                schema: "door_user",
                table: "role_door",
                newName: "IX_role_door_door_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                schema: "door_user",
                table: "users",
                column: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roles",
                schema: "door_user",
                table: "roles",
                column: "role_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_doors",
                schema: "door_user",
                table: "doors",
                column: "door_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role_door",
                schema: "door_user",
                table: "role_door",
                column: "role_door_id");

            migrationBuilder.CreateTable(
                name: "door_logs",
                schema: "door_user",
                columns: table => new
                {
                    log_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    access_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    success = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    door_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_door_logs", x => x.log_id);
                    table.ForeignKey(
                        name: "FK_door_logs_doors_door_id",
                        column: x => x.door_id,
                        principalSchema: "door_user",
                        principalTable: "doors",
                        principalColumn: "door_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_door_logs_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "door_user",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                schema: "door_user",
                columns: table => new
                {
                    user_role_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => x.user_role_id);
                    table.ForeignKey(
                        name: "FK_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "door_user",
                        principalTable: "roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "door_user",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_tags",
                schema: "door_user",
                columns: table => new
                {
                    tag_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    tag_code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_tags", x => x.tag_id);
                    table.ForeignKey(
                        name: "FK_user_tags_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "door_user",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_door_logs_access_date_time",
                schema: "door_user",
                table: "door_logs",
                column: "access_date_time");

            migrationBuilder.CreateIndex(
                name: "IX_door_logs_door_id",
                schema: "door_user",
                table: "door_logs",
                column: "door_id");

            migrationBuilder.CreateIndex(
                name: "IX_door_logs_user_id",
                schema: "door_user",
                table: "door_logs",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_role_id",
                schema: "door_user",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_user_id",
                schema: "door_user",
                table: "user_roles",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_tags_user_id",
                schema: "door_user",
                table: "user_tags",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_role_door_doors_door_id",
                schema: "door_user",
                table: "role_door",
                column: "door_id",
                principalSchema: "door_user",
                principalTable: "doors",
                principalColumn: "door_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_role_door_roles_role_id",
                schema: "door_user",
                table: "role_door",
                column: "role_id",
                principalSchema: "door_user",
                principalTable: "roles",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_door_doors_door_id",
                schema: "door_user",
                table: "role_door");

            migrationBuilder.DropForeignKey(
                name: "FK_role_door_roles_role_id",
                schema: "door_user",
                table: "role_door");

            migrationBuilder.DropTable(
                name: "door_logs",
                schema: "door_user");

            migrationBuilder.DropTable(
                name: "user_roles",
                schema: "door_user");

            migrationBuilder.DropTable(
                name: "user_tags",
                schema: "door_user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                schema: "door_user",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roles",
                schema: "door_user",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_doors",
                schema: "door_user",
                table: "doors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role_door",
                schema: "door_user",
                table: "role_door");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "door_user",
                newName: "Users",
                newSchema: "door_user");

            migrationBuilder.RenameTable(
                name: "roles",
                schema: "door_user",
                newName: "Roles",
                newSchema: "door_user");

            migrationBuilder.RenameTable(
                name: "doors",
                schema: "door_user",
                newName: "Doors",
                newSchema: "door_user");

            migrationBuilder.RenameTable(
                name: "role_door",
                schema: "door_user",
                newName: "RoleDoor",
                newSchema: "door_user");

            migrationBuilder.RenameIndex(
                name: "IX_users_email",
                schema: "door_user",
                table: "Users",
                newName: "IX_Users_email");

            migrationBuilder.RenameIndex(
                name: "IX_doors_location",
                schema: "door_user",
                table: "Doors",
                newName: "IX_Doors_location");

            migrationBuilder.RenameIndex(
                name: "IX_role_door_role_id",
                schema: "door_user",
                table: "RoleDoor",
                newName: "IX_RoleDoor_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_role_door_door_id",
                schema: "door_user",
                table: "RoleDoor",
                newName: "IX_RoleDoor_door_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                schema: "door_user",
                table: "Users",
                column: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                schema: "door_user",
                table: "Roles",
                column: "role_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doors",
                schema: "door_user",
                table: "Doors",
                column: "door_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleDoor",
                schema: "door_user",
                table: "RoleDoor",
                column: "role_door_id");

            migrationBuilder.CreateTable(
                name: "DoorLog",
                schema: "door_user",
                columns: table => new
                {
                    log_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    door_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    access_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    success = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorLog", x => x.log_id);
                    table.ForeignKey(
                        name: "FK_DoorLog_Doors_door_id",
                        column: x => x.door_id,
                        principalSchema: "door_user",
                        principalTable: "Doors",
                        principalColumn: "door_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoorLog_Users_user_id",
                        column: x => x.user_id,
                        principalSchema: "door_user",
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_tole",
                schema: "door_user",
                columns: table => new
                {
                    user_role_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_tole", x => x.user_role_id);
                    table.ForeignKey(
                        name: "FK_user_tole_Roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "door_user",
                        principalTable: "Roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_tole_Users_user_id",
                        column: x => x.user_id,
                        principalSchema: "door_user",
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTag",
                schema: "door_user",
                columns: table => new
                {
                    tag_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    tag_code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTag", x => x.tag_id);
                    table.ForeignKey(
                        name: "FK_UserTag_Users_user_id",
                        column: x => x.user_id,
                        principalSchema: "door_user",
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoorLog_access_date_time",
                schema: "door_user",
                table: "DoorLog",
                column: "access_date_time");

            migrationBuilder.CreateIndex(
                name: "IX_DoorLog_door_id",
                schema: "door_user",
                table: "DoorLog",
                column: "door_id");

            migrationBuilder.CreateIndex(
                name: "IX_DoorLog_user_id",
                schema: "door_user",
                table: "DoorLog",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_tole_role_id",
                schema: "door_user",
                table: "user_tole",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_tole_user_id",
                schema: "door_user",
                table: "user_tole",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserTag_user_id",
                schema: "door_user",
                table: "UserTag",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleDoor_Doors_door_id",
                schema: "door_user",
                table: "RoleDoor",
                column: "door_id",
                principalSchema: "door_user",
                principalTable: "Doors",
                principalColumn: "door_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleDoor_Roles_role_id",
                schema: "door_user",
                table: "RoleDoor",
                column: "role_id",
                principalSchema: "door_user",
                principalTable: "Roles",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
