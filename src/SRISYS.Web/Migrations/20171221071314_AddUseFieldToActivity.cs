namespace Srisys.Web.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <summary>
    /// Migration to add use field to activity
    /// </summary>
    public partial class AddUseFieldToActivity : Migration
    {
        /// <summary>
        /// Ups the migration
        /// </summary>
        /// <param name="migrationBuilder">The migration builder</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Use",
                table: "Activities",
                nullable: true);
        }

        /// <summary>
        /// Downs the migration
        /// </summary>
        /// <param name="migrationBuilder">The migration builder</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Use",
                table: "Activities");
        }
    }
}
