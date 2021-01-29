using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MailSender.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgeRanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    From = table.Column<int>(type: "int", nullable: false),
                    To = table.Column<int>(type: "int", nullable: false),
                    AgeRangeOrder = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeRanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PremiumCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PremiumTotalAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberTCNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberBirthDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberWorkType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberPremiumCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberPremiumType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberPremiumLimitType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberPremiumAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpouseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpouseTCNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpouseBirthDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpouseCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpouseWorkType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpousePremiumCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpousePremiumType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpousePremiumLimitType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpousePremiumAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_1_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_1_TCNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_1_BirthDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_1_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_1_PremiumCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_1_PremiumType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_1_PremiumLimitType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_1_PremiumAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_2_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_2_TCNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_2_BirthDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_2_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_2_PremiumCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_2_PremiumType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_2_PremiumLimitType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_2_PremiumAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_3_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_3_TCNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_3_BirthDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_3_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_3_PremiumCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_3_PremiumType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_3_PremiumLimitType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_3_PremiumAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_4_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_4_TCNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_4_BirthDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_4_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_4_PremiumCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_4_PremiumType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_4_PremiumLimitType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_4_PremiumAmount = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Premiums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    AgeRangeId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PremiumLimitType = table.Column<int>(type: "int", nullable: false),
                    PremiumType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premiums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Premiums_AgeRanges_AgeRangeId",
                        column: x => x.AgeRangeId,
                        principalTable: "AgeRanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Premiums_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Premiums_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SurveyUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TCNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    WorkType = table.Column<int>(type: "int", nullable: false),
                    PremiumId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyUsers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SurveyUsers_Premiums_PremiumId",
                        column: x => x.PremiumId,
                        principalTable: "Premiums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyUserId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_SurveyUsers_SurveyUserId",
                        column: x => x.SurveyUserId,
                        principalTable: "SurveyUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TCNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    SurveyUserId = table.Column<int>(type: "int", nullable: true),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    SubUserOrder = table.Column<int>(type: "int", nullable: false),
                    WorkType = table.Column<int>(type: "int", nullable: false),
                    PremiumId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubUsers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubUsers_Premiums_PremiumId",
                        column: x => x.PremiumId,
                        principalTable: "Premiums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubUsers_SurveyUsers_SurveyUserId",
                        column: x => x.SurveyUserId,
                        principalTable: "SurveyUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_SurveyUserId",
                table: "Contracts",
                column: "SurveyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Premiums_AgeRangeId",
                table: "Premiums",
                column: "AgeRangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Premiums_CityId",
                table: "Premiums",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Premiums_CompanyId",
                table: "Premiums",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SubUsers_CityId",
                table: "SubUsers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_SubUsers_PremiumId",
                table: "SubUsers",
                column: "PremiumId");

            migrationBuilder.CreateIndex(
                name: "IX_SubUsers_SurveyUserId",
                table: "SubUsers",
                column: "SurveyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyUsers_CityId",
                table: "SurveyUsers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyUsers_PremiumId",
                table: "SurveyUsers",
                column: "PremiumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "SubUsers");

            migrationBuilder.DropTable(
                name: "SurveyUsers");

            migrationBuilder.DropTable(
                name: "Premiums");

            migrationBuilder.DropTable(
                name: "AgeRanges");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
