using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Initital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstitutionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstitutionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Institutions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityId = table.Column<int>(nullable: false),
                    InstitutionTypeId = table.Column<int>(nullable: false),
                    Password = table.Column<string>(maxLength: 300, nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    Login = table.Column<string>(nullable: false),
                    OpeningHours = table.Column<string>(maxLength: 20, nullable: false),
                    Website = table.Column<string>(maxLength: 50, nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Institutions_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Institutions_InstitutionTypes_InstitutionTypeId",
                        column: x => x.InstitutionTypeId,
                        principalTable: "InstitutionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Losts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 30, nullable: true),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: true),
                    Clothes = table.Column<string>(maxLength: 200, nullable: false),
                    BodyType = table.Column<string>(nullable: false),
                    EyeColor = table.Column<string>(nullable: true),
                    HairColor = table.Column<string>(nullable: true),
                    Signs = table.Column<string>(maxLength: 300, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    AgeBegin = table.Column<int>(nullable: false),
                    AgeEnd = table.Column<int>(nullable: false),
                    IsFound = table.Column<bool>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(maxLength: 228, nullable: true),
                    Comment = table.Column<string>(maxLength: 300, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    DetectionDescription = table.Column<string>(maxLength: 300, nullable: true),
                    DetectionTime = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    InstitutionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Losts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Losts_Institutions_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Baku" });

            migrationBuilder.InsertData(
                table: "InstitutionTypes",
                columns: new[] { "Id", "Type" },
                values: new object[] { 1, "Medical" });

            migrationBuilder.InsertData(
                table: "Institutions",
                columns: new[] { "Id", "Address", "CityId", "InstitutionTypeId", "IsAdmin", "Latitude", "Login", "Longitude", "Name", "OpeningHours", "Password", "Phone", "Website" },
                values: new object[] { 1, "Underground", 1, 1, true, 40.409264, "admin", 49.867092, "admin", "24/7", "SIzsvsMpIVd0wR9vTjY7A/1Wth1tcMTx0OK3A2V9BI00UjTv", "+994-55-148-82-28", "www.admin.com" });

            migrationBuilder.CreateIndex(
                name: "IX_Institutions_CityId",
                table: "Institutions",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Institutions_InstitutionTypeId",
                table: "Institutions",
                column: "InstitutionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Losts_InstitutionId",
                table: "Losts",
                column: "InstitutionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Losts");

            migrationBuilder.DropTable(
                name: "Institutions");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "InstitutionTypes");
        }
    }
}
