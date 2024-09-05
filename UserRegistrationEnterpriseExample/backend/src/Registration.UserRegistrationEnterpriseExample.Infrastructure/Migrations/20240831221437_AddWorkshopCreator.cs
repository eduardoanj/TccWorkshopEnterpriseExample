using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.Migrations
{
    public partial class AddWorkshopCreator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "id_creator",
                table: "workshops",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "workshops",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "image_creator",
                table: "workshops",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "id_creator",
                table: "workshops");

            migrationBuilder.DropColumn(
                name: "image",
                table: "workshops");

            migrationBuilder.DropColumn(
                name: "image_creator",
                table: "workshops");
        }
    }
}
