using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mark.Migrations
{
    /// <inheritdoc />
    public partial class AddShortDescToToy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "shortDesc",
                table: "toys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "shortDesc",
                table: "toys");
        }
    }
}
