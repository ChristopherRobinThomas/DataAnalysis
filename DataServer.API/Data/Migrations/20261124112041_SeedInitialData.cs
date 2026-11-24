using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataServer.API.Data.Migrations;

/// <inheritdoc />
public partial class _20261124112041_SeedInitialData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Servers",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "oscip05" },
                { 2, "oslab15" }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "Servers",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "Servers",
            keyColumn: "Id",
            keyValue: 2);
    }
}
