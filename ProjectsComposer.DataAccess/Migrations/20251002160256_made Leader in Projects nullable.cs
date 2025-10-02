using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsComposer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class madeLeaderinProjectsnullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Employees_LeaderId",
                table: "Projects");

            migrationBuilder.AlterColumn<Guid>(
                name: "LeaderId",
                table: "Projects",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Employees_LeaderId",
                table: "Projects",
                column: "LeaderId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Employees_LeaderId",
                table: "Projects");

            migrationBuilder.AlterColumn<Guid>(
                name: "LeaderId",
                table: "Projects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Employees_LeaderId",
                table: "Projects",
                column: "LeaderId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
