using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipes_MediatR_API.Migrations
{
    /// <inheritdoc />
    public partial class ChangedVariableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Recipes",
                newName: "Createdby");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Createdby",
                table: "Recipes",
                newName: "CreatedBy");
        }
    }
}
