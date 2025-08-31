using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quarky.Migrations
{
    /// <inheritdoc />
    public partial class AddConceptDependency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Concepts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Concepts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ConceptDependencies",
                columns: table => new
                {
                    ConceptId = table.Column<int>(type: "integer", nullable: false),
                    DependenciesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptDependencies", x => new { x.ConceptId, x.DependenciesId });
                    table.ForeignKey(
                        name: "FK_ConceptDependencies_Concepts_ConceptId",
                        column: x => x.ConceptId,
                        principalTable: "Concepts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConceptDependencies_Concepts_DependenciesId",
                        column: x => x.DependenciesId,
                        principalTable: "Concepts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConceptDependencies_DependenciesId",
                table: "ConceptDependencies",
                column: "DependenciesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConceptDependencies");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Concepts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Concepts");
        }
    }
}
