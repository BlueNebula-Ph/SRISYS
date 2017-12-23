namespace Srisys.Web.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <summary>
    /// Migration to remove OpenIddict tables
    /// </summary>
    public partial class RemoveOpenIddict : Migration
    {
        /// <summary>
        /// Ups the migration
        /// </summary>
        /// <param name="migrationBuilder">The migration builder</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Borrowers_BorrowedById",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_ReceivedById",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_ReleasedById",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Borrowers_ReturnedById",
                table: "Activities");

            migrationBuilder.DropTable(
                name: "OpenIddictScopes");

            migrationBuilder.DropTable(
                name: "OpenIddictTokens");

            migrationBuilder.DropTable(
                name: "OpenIddictAuthorizations");

            migrationBuilder.DropTable(
                name: "OpenIddictApplications");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.AlterColumn<int>(
                name: "ReturnedById",
                table: "Activities",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ReleasedById",
                table: "Activities",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ReceivedById",
                table: "Activities",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "BorrowedById",
                table: "Activities",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "AccessRights",
                table: "Users",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Borrowers_BorrowedById",
                table: "Activities",
                column: "BorrowedById",
                principalTable: "Borrowers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_ReceivedById",
                table: "Activities",
                column: "ReceivedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_ReleasedById",
                table: "Activities",
                column: "ReleasedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Borrowers_ReturnedById",
                table: "Activities",
                column: "ReturnedById",
                principalTable: "Borrowers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <summary>
        /// Downs the migration
        /// </summary>
        /// <param name="migrationBuilder">The migration builder</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Borrowers_BorrowedById",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_ReceivedById",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_ReleasedById",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Borrowers_ReturnedById",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "AccessRights",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "Password");

            migrationBuilder.AlterColumn<int>(
                name: "ReturnedById",
                table: "Activities",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReleasedById",
                table: "Activities",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReceivedById",
                table: "Activities",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BorrowedById",
                table: "Activities",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "OpenIddictApplications",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ClientId = table.Column<string>(nullable: false),
                    ClientSecret = table.Column<string>(nullable: true),
                    ConcurrencyToken = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    PostLogoutRedirectUris = table.Column<string>(nullable: true),
                    RedirectUris = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictScopes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyToken = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictScopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictAuthorizations",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ApplicationId = table.Column<string>(nullable: true),
                    ConcurrencyToken = table.Column<string>(nullable: true),
                    Scopes = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    Subject = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictAuthorizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictAuthorizations_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictTokens",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ApplicationId = table.Column<string>(nullable: true),
                    AuthorizationId = table.Column<string>(nullable: true),
                    Ciphertext = table.Column<string>(nullable: true),
                    ConcurrencyToken = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTimeOffset>(nullable: true),
                    ExpirationDate = table.Column<DateTimeOffset>(nullable: true),
                    Hash = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId",
                        column: x => x.AuthorizationId,
                        principalTable: "OpenIddictAuthorizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictApplications_ClientId",
                table: "OpenIddictApplications",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictAuthorizations_ApplicationId",
                table: "OpenIddictAuthorizations",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ApplicationId",
                table: "OpenIddictTokens",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_AuthorizationId",
                table: "OpenIddictTokens",
                column: "AuthorizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_Hash",
                table: "OpenIddictTokens",
                column: "Hash",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Borrowers_BorrowedById",
                table: "Activities",
                column: "BorrowedById",
                principalTable: "Borrowers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_ReceivedById",
                table: "Activities",
                column: "ReceivedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_ReleasedById",
                table: "Activities",
                column: "ReleasedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Borrowers_ReturnedById",
                table: "Activities",
                column: "ReturnedById",
                principalTable: "Borrowers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
