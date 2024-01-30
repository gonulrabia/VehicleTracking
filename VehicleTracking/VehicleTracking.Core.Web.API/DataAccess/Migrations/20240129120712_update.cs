using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleTracking.Core.Web.API.DataAccess.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstApproval",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "SecondApproval",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "WeighingProcess",
                table: "Vehicles",
                newName: "Approval");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Approval",
                table: "Vehicles",
                newName: "WeighingProcess");

            migrationBuilder.AddColumn<int>(
                name: "FirstApproval",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SecondApproval",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
