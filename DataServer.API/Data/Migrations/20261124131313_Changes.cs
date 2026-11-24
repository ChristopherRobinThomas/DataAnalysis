using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataServer.API.Data.Migrations;

/// <inheritdoc />
public partial class _20261124131313_Changes : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<decimal>(
            name: "Sleep",
            table: "Runs",
            type: "TEXT",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "name",
            table: "Runs",
            type: "TEXT",
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Sleep",
            table: "Runs");

        migrationBuilder.DropColumn(
            name: "name",
            table: "Runs");
    }
}
