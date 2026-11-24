using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataServer.API.Data.Migrations;

/// <inheritdoc />
public partial class _20261124111203_Init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Servers",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Servers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", nullable: false),
                Salt = table.Column<string>(type: "TEXT", nullable: false),
                Password = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Entries",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                date = table.Column<string>(type: "TEXT", nullable: false),
                ServerId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Entries", x => x.Id);
                table.ForeignKey(
                    name: "FK_Entries_Servers_ServerId",
                    column: x => x.ServerId,
                    principalTable: "Servers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Runs",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                overall = table.Column<decimal>(type: "TEXT", nullable: true),
                Cpu = table.Column<decimal>(type: "TEXT", nullable: true),
                Memory = table.Column<decimal>(type: "TEXT", nullable: true),
                Io = table.Column<decimal>(type: "TEXT", nullable: true),
                System = table.Column<decimal>(type: "TEXT", nullable: true),
                Mt = table.Column<decimal>(type: "TEXT", nullable: true),
                St = table.Column<decimal>(type: "TEXT", nullable: true),
                Os = table.Column<decimal>(type: "TEXT", nullable: true),
                Sys = table.Column<decimal>(type: "TEXT", nullable: true),
                EntryId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Runs", x => x.Id);
                table.ForeignKey(
                    name: "FK_Runs_Entries_EntryId",
                    column: x => x.EntryId,
                    principalTable: "Entries",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Entries_ServerId",
            table: "Entries",
            column: "ServerId");

        migrationBuilder.CreateIndex(
            name: "IX_Runs_EntryId",
            table: "Runs",
            column: "EntryId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Runs");

        migrationBuilder.DropTable(
            name: "Users");

        migrationBuilder.DropTable(
            name: "Entries");

        migrationBuilder.DropTable(
            name: "Servers");
    }
}
