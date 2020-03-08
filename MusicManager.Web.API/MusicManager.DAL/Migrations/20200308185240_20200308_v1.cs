using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicManager.DAL.Migrations
{
    public partial class _20200308_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    ArtistId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 300, nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 3, 8, 20, 52, 38, 379, DateTimeKind.Local).AddTicks(2470)),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.ArtistId);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    GenreId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 300, nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 3, 8, 20, 52, 38, 352, DateTimeKind.Local).AddTicks(8444)),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Album",
                columns: table => new
                {
                    AlbumId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    Summary = table.Column<string>(maxLength: 2000, nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 3, 8, 20, 52, 38, 398, DateTimeKind.Local).AddTicks(6621)),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => x.AlbumId);
                    table.ForeignKey(
                        name: "FK_Album_Artist_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artist",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlbumGenre",
                columns: table => new
                {
                    AlbumId = table.Column<int>(nullable: false),
                    GenreId = table.Column<int>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 3, 8, 20, 52, 38, 437, DateTimeKind.Local).AddTicks(4456)),
                    DateDeleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumGenre", x => new { x.AlbumId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_AlbumGenre_Album_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Album",
                        principalColumn: "AlbumId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumGenre_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Track",
                columns: table => new
                {
                    TrackId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlbumId = table.Column<int>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 300, nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 3, 8, 20, 52, 38, 408, DateTimeKind.Local).AddTicks(5713)),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Track", x => x.TrackId);
                    table.ForeignKey(
                        name: "FK_Track_Album_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Album",
                        principalColumn: "AlbumId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Artist",
                columns: new[] { "ArtistId", "DateDeleted", "DateModified", "Name" },
                values: new object[,]
                {
                    { 1, null, null, "The Night Flight Orchestra" },
                    { 2, null, null, "Abba" },
                    { 3, null, null, "Bruce Springsteen" }
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "GenreId", "DateDeleted", "DateModified", "Name" },
                values: new object[,]
                {
                    { 1, null, null, "Rock" },
                    { 2, null, null, "Metal" },
                    { 3, null, null, "Pop" },
                    { 4, null, null, "Symphonic" }
                });

            migrationBuilder.InsertData(
                table: "Album",
                columns: new[] { "AlbumId", "ArtistId", "DateDeleted", "DateModified", "Name", "ReleaseDate", "Summary" },
                values: new object[] { 1, 1, null, null, "Internal Affairs", new DateTime(2012, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Internal Affairs is the first studio album by Swedish classic rock/AOR band The Night Flight Orchestra, released on 18 June 2012 via Coroner Records." });

            migrationBuilder.InsertData(
                table: "AlbumGenre",
                columns: new[] { "AlbumId", "GenreId", "DateDeleted" },
                values: new object[,]
                {
                    { 1, 1, null },
                    { 1, 3, null }
                });

            migrationBuilder.InsertData(
                table: "Track",
                columns: new[] { "TrackId", "AlbumId", "DateDeleted", "DateModified", "Duration", "Name", "Position" },
                values: new object[,]
                {
                    { 1, 1, null, null, 363, "Siberian Queen", 1 },
                    { 2, 1, null, null, 337, "California Morning", 2 },
                    { 3, 1, null, null, 237, "Glowing City Madness", 3 },
                    { 4, 1, null, null, 402, "West Ruth Ave", 4 },
                    { 5, 1, null, null, 501, "Transatlantic Blues", 5 },
                    { 6, 1, null, null, 229, "Miami 5:02", 6 },
                    { 7, 1, null, null, 298, "Internal Affairs", 7 },
                    { 8, 1, null, null, 297, "1998", 8 },
                    { 9, 1, null, null, 267, "Stella Ain't No Dove", 9 },
                    { 10, 1, null, null, 281, "Montreal Midnight Supply", 10 },
                    { 11, 1, null, null, 248, "Green Hills of Glumslöv", 11 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Album_ArtistId",
                table: "Album",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_AlbumGenre_GenreId",
                table: "AlbumGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Track_AlbumId",
                table: "Track",
                column: "AlbumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumGenre");

            migrationBuilder.DropTable(
                name: "Track");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Album");

            migrationBuilder.DropTable(
                name: "Artist");
        }
    }
}
