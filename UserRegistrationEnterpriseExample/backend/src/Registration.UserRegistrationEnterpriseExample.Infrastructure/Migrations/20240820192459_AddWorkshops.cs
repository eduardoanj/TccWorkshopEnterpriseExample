using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.Migrations
{
    public partial class AddWorkshops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_type = table.Column<int>(type: "integer", nullable: false),
                    document = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ultima_modificacao_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    origin_timestamp_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "workshops",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ultima_modificacao_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    origin_timestamp_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workshops", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_workshops",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkshopId = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ultima_modificacao_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    origin_timestamp_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_workshops", x => new { x.UserId, x.WorkshopId });
                    table.ForeignKey(
                        name: "FK_user_workshops_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_workshops_workshops_WorkshopId",
                        column: x => x.WorkshopId,
                        principalTable: "workshops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_workshops_WorkshopId",
                table: "user_workshops",
                column: "WorkshopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_workshops");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "workshops");
        }
    }
}
