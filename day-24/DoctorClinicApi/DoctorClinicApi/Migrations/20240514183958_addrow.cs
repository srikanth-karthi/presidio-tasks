using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorClinicApi.Migrations
{
    /// <inheritdoc />
    public partial class addrow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Doctorid", "Doctorname", "Experience", "Specification" },
                values: new object[] { 3, "Sugan", 2f, "Mbbs" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Doctorid",
                keyValue: 3);
        }
    }
}
