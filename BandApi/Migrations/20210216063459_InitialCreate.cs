using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BandApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bands",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Founded = table.Column<DateTime>(nullable: false),
                    MainGenre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "albums",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 400, nullable: true),
                    BandId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_albums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_albums_bands_BandId",
                        column: x => x.BandId,
                        principalTable: "bands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "bands",
                columns: new[] { "Id", "Founded", "MainGenre", "Name" },
                values: new object[,]
                {
                    { new Guid("f2e4cffc-fcc7-11ea-adc1-0242ac120002"), new DateTime(1988, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "HeavyMetal", "Metallica" },
                    { new Guid("f2e4d240-fcc7-11ea-adc1-0242ac120002"), new DateTime(1985, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rock", "Guns N Roses" },
                    { new Guid("f2e4d344-fcc7-11ea-adc1-0242ac120002"), new DateTime(1965, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Disco", "ABBA" },
                    { new Guid("f2e4d416-fcc7-11ea-adc1-0242ac120002"), new DateTime(1985, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alternative", "Oasis" },
                    { new Guid("f2e4d4de-fcc7-11ea-adc1-0242ac120002"), new DateTime(1981, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pop", "A-ha" }
                });

            migrationBuilder.InsertData(
                table: "albums",
                columns: new[] { "Id", "BandId", "Description", "Title" },
                values: new object[,]
                {
                    { new Guid("7547c210-fcc8-11ea-adc1-0242ac120002"), new Guid("f2e4cffc-fcc7-11ea-adc1-0242ac120002"), "one of the best bands ever", "master of puppets" },
                    { new Guid("7547c45e-fcc8-11ea-adc1-0242ac120002"), new Guid("f2e4cffc-fcc7-11ea-adc1-0242ac120002"), "amazing rock albumn with raw sound", "appetite for destrcution" },
                    { new Guid("7547c558-fcc8-11ea-adc1-0242ac120002"), new Guid("f2e4cffc-fcc7-11ea-adc1-0242ac120002"), "very gloomy albumn", "waterloo" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_albums_BandId",
                table: "albums",
                column: "BandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "albums");

            migrationBuilder.DropTable(
                name: "bands");
        }
    }
}
