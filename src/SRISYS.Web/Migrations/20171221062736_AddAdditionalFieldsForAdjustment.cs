namespace Srisys.Web.Migrations
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <summary>
    /// Migration to add fields to adjustment
    /// </summary>
    public partial class AddAdditionalFieldsForAdjustment : Migration
    {
        /// <summary>
        /// Ups the migration
        /// </summary>
        /// <param name="migrationBuilder">The migration builder</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Adjustments",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptNumber",
                table: "Adjustments",
                nullable: true);
        }

        /// <summary>
        /// Downs the migration
        /// </summary>
        /// <param name="migrationBuilder">The migration builder</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Adjustments");

            migrationBuilder.DropColumn(
                name: "ReceiptNumber",
                table: "Adjustments");
        }
    }
}
