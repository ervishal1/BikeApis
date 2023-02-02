using Microsoft.EntityFrameworkCore.Migrations;

namespace BikeApis.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BikeComments_AspNetUsers_UserId1",
                table: "BikeComments");

            migrationBuilder.DropForeignKey(
                name: "FK_BikeLikes_AspNetUsers_UserId1",
                table: "BikeLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Bikes_AspNetUsers_UserId1",
                table: "Bikes");

            migrationBuilder.DropIndex(
                name: "IX_Bikes_UserId1",
                table: "Bikes");

            migrationBuilder.DropIndex(
                name: "IX_BikeLikes_UserId1",
                table: "BikeLikes");

            migrationBuilder.DropIndex(
                name: "IX_BikeComments_UserId1",
                table: "BikeComments");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Bikes");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "BikeLikes");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "BikeComments");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Bikes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BikeLikes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BikeComments",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Bikes_UserId",
                table: "Bikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BikeLikes_UserId",
                table: "BikeLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BikeComments_UserId",
                table: "BikeComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BikeComments_AspNetUsers_UserId",
                table: "BikeComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BikeLikes_AspNetUsers_UserId",
                table: "BikeLikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bikes_AspNetUsers_UserId",
                table: "Bikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BikeComments_AspNetUsers_UserId",
                table: "BikeComments");

            migrationBuilder.DropForeignKey(
                name: "FK_BikeLikes_AspNetUsers_UserId",
                table: "BikeLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Bikes_AspNetUsers_UserId",
                table: "Bikes");

            migrationBuilder.DropIndex(
                name: "IX_Bikes_UserId",
                table: "Bikes");

            migrationBuilder.DropIndex(
                name: "IX_BikeLikes_UserId",
                table: "BikeLikes");

            migrationBuilder.DropIndex(
                name: "IX_BikeComments_UserId",
                table: "BikeComments");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Bikes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Bikes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "BikeLikes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "BikeLikes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "BikeComments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "BikeComments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bikes_UserId1",
                table: "Bikes",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_BikeLikes_UserId1",
                table: "BikeLikes",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_BikeComments_UserId1",
                table: "BikeComments",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BikeComments_AspNetUsers_UserId1",
                table: "BikeComments",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BikeLikes_AspNetUsers_UserId1",
                table: "BikeLikes",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bikes_AspNetUsers_UserId1",
                table: "Bikes",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
