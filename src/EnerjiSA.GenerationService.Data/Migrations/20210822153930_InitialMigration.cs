using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EnerjiSA.GenerationService.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PowerPlant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WebId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerPlant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PowerPlantData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PowerPlantId = table.Column<int>(type: "integer", nullable: false),
                    Good = table.Column<bool>(type: "boolean", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerPlantData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PowerPlantData_PowerPlant_PowerPlantId",
                        column: x => x.PowerPlantId,
                        principalTable: "PowerPlant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WEBID",
                table: "PowerPlant",
                column: "WebId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PowerPlantData_PowerPlantId",
                table: "PowerPlantData",
                column: "PowerPlantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PowerPlantData");

            migrationBuilder.DropTable(
                name: "PowerPlant");
        }
    }
}
