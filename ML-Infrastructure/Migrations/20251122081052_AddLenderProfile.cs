using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ML_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLenderProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lenders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MobileNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    BusinessName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    LicenseNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Jurisdiction = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    BankName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    AccountNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IfscCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Branch = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lenders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lenders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lenders_UserId",
                table: "Lenders",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lenders");
        }
    }
}
