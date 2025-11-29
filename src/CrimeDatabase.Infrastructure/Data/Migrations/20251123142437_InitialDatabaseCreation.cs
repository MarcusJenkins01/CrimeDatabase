using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrimeDatabase.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabaseCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Crimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CrimeDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VictimName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CrimeType = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crimes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotesAudits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CrimeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalNotes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    UpdatedNotes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotesAudits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotesAudits_Crimes_CrimeId",
                        column: x => x.CrimeId,
                        principalTable: "Crimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotesAudits_CrimeId",
                table: "NotesAudits",
                column: "CrimeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotesAudits");

            migrationBuilder.DropTable(
                name: "Crimes");
        }
    }
}
