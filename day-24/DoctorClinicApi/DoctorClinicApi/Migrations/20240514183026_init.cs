using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DoctorClinicApi.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Doctorid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Doctorname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Doctorid);
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Doctorid", "Doctorname", "Experience", "Specification" },
                values: new object[,]
                {
                    { 1, "srikanth", 2f, "Mbbs" },
                    { 2, "Barath", 5f, "Mbbs,md" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctors");
        }
    }
}
