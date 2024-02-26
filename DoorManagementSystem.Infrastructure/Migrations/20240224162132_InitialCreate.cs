using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DoorManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "door_user");

            migrationBuilder.CreateTable(
                name: "Doors",
                schema: "door_user",
                columns: table => new
                {
                    door_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    location = table.Column<string>(type: "text", nullable: false),
                    door_name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    remote_acccess_enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doors", x => x.door_id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "door_user",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_name = table.Column<string>(type: "text", nullable: false),
                    can_access_logs = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "door_user",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "RoleDoor",
                schema: "door_user",
                columns: table => new
                {
                    role_door_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    door_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleDoor", x => x.role_door_id);
                    table.ForeignKey(
                        name: "FK_RoleDoor_Doors_door_id",
                        column: x => x.door_id,
                        principalSchema: "door_user",
                        principalTable: "Doors",
                        principalColumn: "door_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleDoor_Roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "door_user",
                        principalTable: "Roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoorAccess",
                schema: "door_user",
                columns: table => new
                {
                    access_role_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    door_id = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "DoorLog",
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
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false)
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
                name: "IX_DoorAccess_door_id",
                schema: "door_user",
                table: "DoorAccess",
                column: "door_id");

            migrationBuilder.CreateIndex(
                name: "IX_DoorAccess_user_id",
                schema: "door_user",
                table: "DoorAccess",
                column: "user_id");

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
                name: "IX_RoleDoor_door_id",
                schema: "door_user",
                table: "RoleDoor",
                column: "door_id");

            migrationBuilder.CreateIndex(
                name: "IX_RoleDoor_role_id",
                schema: "door_user",
                table: "RoleDoor",
                column: "role_id");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoorAccess",
                schema: "door_user");

            migrationBuilder.DropTable(
                name: "DoorLog",
                schema: "door_user");

            migrationBuilder.DropTable(
                name: "RoleDoor",
                schema: "door_user");

            migrationBuilder.DropTable(
                name: "user_tole",
                schema: "door_user");

            migrationBuilder.DropTable(
                name: "UserTag",
                schema: "door_user");

            migrationBuilder.DropTable(
                name: "Doors",
                schema: "door_user");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "door_user");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "door_user");
        }
    }
}
