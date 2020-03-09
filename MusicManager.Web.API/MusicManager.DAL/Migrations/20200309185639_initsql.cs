using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicManager.DAL.Migrations
{
    public partial class initsql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    GenreId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 300, nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 3, 9, 20, 56, 37, 976, DateTimeKind.Local).AddTicks(4611)),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "ImageRef",
                columns: table => new
                {
                    ImageRefId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    URI = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 3, 9, 20, 56, 37, 957, DateTimeKind.Local).AddTicks(9902)),
                    DateDeleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageRef", x => x.ImageRefId);
                });

            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    ArtistId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageRefId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 300, nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 3, 9, 20, 56, 37, 989, DateTimeKind.Local).AddTicks(3704)),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.ArtistId);
                    table.ForeignKey(
                        name: "FK_Artist_ImageRef_ImageRefId",
                        column: x => x.ImageRefId,
                        principalTable: "ImageRef",
                        principalColumn: "ImageRefId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Album",
                columns: table => new
                {
                    AlbumId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistId = table.Column<int>(nullable: false),
                    ImageRefId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    Summary = table.Column<string>(maxLength: 2000, nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 3, 9, 20, 56, 37, 996, DateTimeKind.Local).AddTicks(8149)),
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
                    table.ForeignKey(
                        name: "FK_Album_ImageRef_ImageRefId",
                        column: x => x.ImageRefId,
                        principalTable: "ImageRef",
                        principalColumn: "ImageRefId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlbumGenre",
                columns: table => new
                {
                    AlbumId = table.Column<int>(nullable: false),
                    GenreId = table.Column<int>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 3, 9, 20, 56, 38, 24, DateTimeKind.Local).AddTicks(8435)),
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
                    DateAdded = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 3, 9, 20, 56, 38, 7, DateTimeKind.Local).AddTicks(1491)),
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
                table: "Genre",
                columns: new[] { "GenreId", "DateDeleted", "DateModified", "Name" },
                values: new object[,]
                {
                    { 1, null, null, "Rock" },
                    { 2, null, null, "Metal" },
                    { 3, null, null, "Pop" },
                    { 4, null, null, "Symphonic" },
                    { 5, null, null, "Progressive" },
                    { 6, null, null, "Acoustic" },
                    { 7, null, null, "Folk" }
                });

            migrationBuilder.InsertData(
                table: "ImageRef",
                columns: new[] { "ImageRefId", "DateDeleted", "URI" },
                values: new object[,]
                {
                    { 1, null, "images/20200309_Born_to_Run_(Front_Cover).jpg" },
                    { 2, null, "images/20200309_Bruce_Springsteen_-_Nebraska.jpg" },
                    { 3, null, "images/20200309_bruce-springsteen-260-260.jpg" },
                    { 4, null, "images/20200309_In_the_Passing_Light_of_Day_Cover.jpg" },
                    { 5, null, "images/20200309_NFO.jpg" },
                    { 6, null, "images/20200309_PainOfSalvation.jpg" },
                    { 7, null, "images/20200309-NFO_Internal_Affairs.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Artist",
                columns: new[] { "ArtistId", "DateDeleted", "DateModified", "ImageRefId", "Name" },
                values: new object[] { 3, null, null, 3, "Bruce Springsteen" });

            migrationBuilder.InsertData(
                table: "Artist",
                columns: new[] { "ArtistId", "DateDeleted", "DateModified", "ImageRefId", "Name" },
                values: new object[] { 1, null, null, 5, "The Night Flight Orchestra" });

            migrationBuilder.InsertData(
                table: "Artist",
                columns: new[] { "ArtistId", "DateDeleted", "DateModified", "ImageRefId", "Name" },
                values: new object[] { 2, null, null, 6, "Pain Of Salvation" });

            migrationBuilder.InsertData(
                table: "Album",
                columns: new[] { "AlbumId", "ArtistId", "DateDeleted", "DateModified", "ImageRefId", "Name", "ReleaseDate", "Summary" },
                values: new object[,]
                {
                    { 2, 3, null, null, 2, "Nebraska", new DateTime(1982, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nebraska is the sixth studio album by Bruce Springsteen, released on September 30, 1982, by Columbia Records." },
                    { 4, 3, null, null, 1, "Born To Run", new DateTime(1975, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Born to Run is the third studio album by American singer-songwriter Bruce Springsteen." },
                    { 1, 1, null, null, 7, "Internal Affairs", new DateTime(2012, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Internal Affairs is the first studio album by Swedish classic rock/AOR band The Night Flight Orchestra, released on 18 June 2012 via Coroner Records." },
                    { 3, 2, null, null, 4, "In The Passing Light Of Day", new DateTime(2017, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "In the Passing Light of Day is the tenth studio album by Swedish band Pain of Salvation and was released on 13 January 2017 by InsideOut." }
                });

            migrationBuilder.InsertData(
                table: "AlbumGenre",
                columns: new[] { "AlbumId", "GenreId", "DateDeleted" },
                values: new object[,]
                {
                    { 2, 6, null },
                    { 1, 3, null },
                    { 1, 1, null },
                    { 3, 1, null },
                    { 3, 2, null },
                    { 3, 5, null },
                    { 4, 1, null },
                    { 2, 7, null }
                });

            migrationBuilder.InsertData(
                table: "Track",
                columns: new[] { "TrackId", "AlbumId", "DateDeleted", "DateModified", "Duration", "Name", "Position" },
                values: new object[,]
                {
                    { 24, 3, null, null, 287, "Meaningless", 3 },
                    { 29, 3, null, null, 393, "The Taming of a Beast", 8 },
                    { 5, 1, null, null, 501, "Transatlantic Blues", 5 },
                    { 6, 1, null, null, 229, "Miami 5:02", 6 },
                    { 7, 1, null, null, 298, "Internal Affairs", 7 },
                    { 8, 1, null, null, 297, "1998", 8 },
                    { 9, 1, null, null, 267, "Stella Ain't No Dove", 9 },
                    { 10, 1, null, null, 281, "Montreal Midnight Supply", 10 },
                    { 11, 1, null, null, 248, "Green Hills of Glumslöv", 11 },
                    { 28, 3, null, null, 384, "Angels of Broken Things", 7 },
                    { 4, 1, null, null, 402, "West Ruth Ave", 4 },
                    { 27, 3, null, null, 285, "Reasons", 6 },
                    { 26, 3, null, null, 545, "Full Throttle Tribe", 5 },
                    { 22, 3, null, null, 622, "On a Tuesday", 1 },
                    { 23, 3, null, null, 293, "Tongue of God", 2 },
                    { 25, 3, null, null, 203, "Silent Gold", 4 },
                    { 3, 1, null, null, 237, "Glowing City Madness", 3 },
                    { 1, 1, null, null, 363, "Siberian Queen", 1 },
                    { 30, 3, null, null, 303, "If This Is the End", 9 },
                    { 12, 2, null, null, 272, "Nebraska", 1 },
                    { 13, 2, null, null, 240, "Atlantic City", 2 },
                    { 14, 2, null, null, 248, "Mansion on the Hill", 3 },
                    { 15, 2, null, null, 224, "Johnny 99", 4 },
                    { 16, 2, null, null, 340, "Highway Patrolman", 5 },
                    { 17, 2, null, null, 197, "State Trooper", 6 },
                    { 18, 2, null, null, 191, "Used Cars", 7 },
                    { 19, 2, null, null, 178, "Open All Night", 8 },
                    { 2, 1, null, null, 337, "California Morning", 2 },
                    { 20, 2, null, null, 307, "My Father's Hous", 9 },
                    { 32, 4, null, null, 289, "Thunder Road", 1 },
                    { 33, 4, null, null, 191, "Tenth Avenue Freeze-Out", 2 },
                    { 34, 4, null, null, 180, "Night", 3 },
                    { 35, 4, null, null, 330, "Backstreets", 4 },
                    { 36, 4, null, null, 281, "Born to Run", 5 },
                    { 37, 4, null, null, 280, "She's the One", 6 },
                    { 38, 4, null, null, 191, "Meeting Across the River", 7 },
                    { 39, 4, null, null, 554, "Jungleland", 8 },
                    { 21, 2, null, null, 251, "Reason to Believe", 10 },
                    { 31, 3, null, null, 931, "The Passing Light of Day", 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Album_ArtistId",
                table: "Album",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Album_ImageRefId",
                table: "Album",
                column: "ImageRefId");

            migrationBuilder.CreateIndex(
                name: "IX_AlbumGenre_GenreId",
                table: "AlbumGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Artist_ImageRefId",
                table: "Artist",
                column: "ImageRefId");

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

            migrationBuilder.DropTable(
                name: "ImageRef");
        }
    }
}
