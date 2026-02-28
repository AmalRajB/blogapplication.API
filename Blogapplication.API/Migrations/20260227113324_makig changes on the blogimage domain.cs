using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogapplication.API.Migrations
{
    /// <inheritdoc />
    public partial class makigchangesontheblogimagedomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Blogimages",
                newName: "FileName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Blogimages",
                newName: "FirstName");
        }
    }
}
