namespace Srisys.Web.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Borrowers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrowers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    OtherDetails = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Firstname = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    Lastname = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
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
                name: "Subcategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "References",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    ParentReferenceId = table.Column<int>(nullable: true),
                    ReferenceTypeId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_References", x => x.Id);
                    table.ForeignKey(
                        name: "FK_References_References_ParentReferenceId",
                        column: x => x.ParentReferenceId,
                        principalTable: "References",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_References_ReferenceTypes_ReferenceTypeId",
                        column: x => x.ReferenceTypeId,
                        principalTable: "ReferenceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Brand = table.Column<string>(nullable: false),
                    CategoryId = table.Column<int>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastPurchaseDate = table.Column<DateTime>(nullable: true),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    MinimumStock = table.Column<double>(nullable: true),
                    Model = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    RemainingQuantity = table.Column<double>(nullable: false),
                    ReorderQuantity = table.Column<double>(nullable: true),
                    Size = table.Column<string>(nullable: false),
                    SubCategoryId = table.Column<int>(nullable: true),
                    SupplierId = table.Column<int>(nullable: true),
                    TypeId = table.Column<int>(nullable: false),
                    Unit = table.Column<string>(nullable: false),
                    Use = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_Subcategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "Subcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_References_TypeId",
                        column: x => x.TypeId,
                        principalTable: "References",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BorrowedById = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    LatestReturnDate = table.Column<DateTime>(nullable: false),
                    MaterialId = table.Column<int>(nullable: false),
                    QuantityBorrowed = table.Column<double>(nullable: false),
                    ReceivedById = table.Column<int>(nullable: false),
                    ReleasedById = table.Column<int>(nullable: false),
                    ReturnedById = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    TotalQuantityReturned = table.Column<double>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_Borrowers_BorrowedById",
                        column: x => x.BorrowedById,
                        principalTable: "Borrowers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Activities_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activities_Users_ReceivedById",
                        column: x => x.ReceivedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Activities_Users_ReleasedById",
                        column: x => x.ReleasedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Activities_Borrowers_ReturnedById",
                        column: x => x.ReturnedById,
                        principalTable: "Borrowers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Adjustments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdjustmentType = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    MaterialId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adjustments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adjustments_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
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

            migrationBuilder.CreateIndex(
                name: "IX_Activities_BorrowedById",
                table: "Activities",
                column: "BorrowedById");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_MaterialId",
                table: "Activities",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ReceivedById",
                table: "Activities",
                column: "ReceivedById");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ReleasedById",
                table: "Activities",
                column: "ReleasedById");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ReturnedById",
                table: "Activities",
                column: "ReturnedById");

            migrationBuilder.CreateIndex(
                name: "IX_Adjustments_MaterialId",
                table: "Adjustments",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_CategoryId",
                table: "Materials",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_SubCategoryId",
                table: "Materials",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_SupplierId",
                table: "Materials",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_TypeId",
                table: "Materials",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_References_ParentReferenceId",
                table: "References",
                column: "ParentReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_References_ReferenceTypeId",
                table: "References",
                column: "ReferenceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_CategoryId",
                table: "Subcategories",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpenIddictScopes");

            migrationBuilder.DropTable(
                name: "OpenIddictTokens");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Adjustments");

            migrationBuilder.DropTable(
                name: "OpenIddictAuthorizations");

            migrationBuilder.DropTable(
                name: "Borrowers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "OpenIddictApplications");

            migrationBuilder.DropTable(
                name: "Subcategories");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "References");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ReferenceTypes");
        }
    }
}
