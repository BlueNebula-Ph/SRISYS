using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Srisys.Web.Migrations
{
    [DbContext(typeof(SrisysDbContext))]
    [Migration("20171125132714_InitialCreate")]
    partial class InitialCreate
    {
        /// <summary>
        /// Builds the target model.
        /// </summary>
        /// <param name="modelBuilder">The model builder</param>
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OpenIddict.Models.OpenIddictApplication", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientId")
                        .IsRequired();

                    b.Property<string>("ClientSecret");

                    b.Property<string>("ConcurrencyToken")
                        .IsConcurrencyToken();

                    b.Property<string>("DisplayName");

                    b.Property<string>("PostLogoutRedirectUris");

                    b.Property<string>("RedirectUris");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("OpenIddictApplications");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictAuthorization", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationId");

                    b.Property<string>("ConcurrencyToken")
                        .IsConcurrencyToken();

                    b.Property<string>("Scopes");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<string>("Subject")
                        .IsRequired();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("OpenIddictAuthorizations");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictScope", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyToken")
                        .IsConcurrencyToken();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("OpenIddictScopes");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationId");

                    b.Property<string>("AuthorizationId");

                    b.Property<string>("Ciphertext");

                    b.Property<string>("ConcurrencyToken")
                        .IsConcurrencyToken();

                    b.Property<DateTimeOffset?>("CreationDate");

                    b.Property<DateTimeOffset?>("ExpirationDate");

                    b.Property<string>("Hash");

                    b.Property<string>("Status");

                    b.Property<string>("Subject")
                        .IsRequired();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("AuthorizationId");

                    b.HasIndex("Hash")
                        .IsUnique();

                    b.ToTable("OpenIddictTokens");
                });

            modelBuilder.Entity("Srisys.Web.Models.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BorrowedById");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("Date");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<DateTime>("LatestReturnDate");

                    b.Property<int>("MaterialId");

                    b.Property<double>("QuantityBorrowed");

                    b.Property<int>("ReceivedById");

                    b.Property<int>("ReleasedById");

                    b.Property<int>("ReturnedById");

                    b.Property<int>("Status");

                    b.Property<double>("TotalQuantityReturned");

                    b.HasKey("Id");

                    b.HasIndex("BorrowedById");

                    b.HasIndex("MaterialId");

                    b.HasIndex("ReceivedById");

                    b.HasIndex("ReleasedById");

                    b.HasIndex("ReturnedById");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("Srisys.Web.Models.Adjustment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdjustmentType")
                        .IsRequired();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("Date");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<int>("MaterialId");

                    b.Property<double>("Quantity");

                    b.Property<string>("Remarks");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.ToTable("Adjustments");
                });

            modelBuilder.Entity("Srisys.Web.Models.Borrower", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Borrowers");
                });

            modelBuilder.Entity("Srisys.Web.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Srisys.Web.Models.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Brand")
                        .IsRequired();

                    b.Property<int?>("CategoryId");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastPurchaseDate");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<string>("Location");

                    b.Property<double?>("MinimumStock");

                    b.Property<string>("Model")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal?>("Price");

                    b.Property<double>("Quantity");

                    b.Property<double>("RemainingQuantity");

                    b.Property<double?>("ReorderQuantity");

                    b.Property<string>("Size")
                        .IsRequired();

                    b.Property<int?>("SubCategoryId");

                    b.Property<int?>("SupplierId");

                    b.Property<int>("TypeId");

                    b.Property<string>("Unit")
                        .IsRequired();

                    b.Property<string>("Use");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SubCategoryId");

                    b.HasIndex("SupplierId");

                    b.HasIndex("TypeId");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("Srisys.Web.Models.Reference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<int?>("ParentReferenceId");

                    b.Property<int>("ReferenceTypeId");

                    b.HasKey("Id");

                    b.HasIndex("ParentReferenceId");

                    b.HasIndex("ReferenceTypeId");

                    b.ToTable("References");
                });

            modelBuilder.Entity("Srisys.Web.Models.ReferenceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("ReferenceTypes");
                });

            modelBuilder.Entity("Srisys.Web.Models.Subcategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Subcategories");
                });

            modelBuilder.Entity("Srisys.Web.Models.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("OtherDetails");

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Srisys.Web.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Firstname");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<string>("Lastname");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictAuthorization", b =>
                {
                    b.HasOne("OpenIddict.Models.OpenIddictApplication", "Application")
                        .WithMany("Authorizations")
                        .HasForeignKey("ApplicationId");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictToken", b =>
                {
                    b.HasOne("OpenIddict.Models.OpenIddictApplication", "Application")
                        .WithMany("Tokens")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OpenIddict.Models.OpenIddictAuthorization", "Authorization")
                        .WithMany("Tokens")
                        .HasForeignKey("AuthorizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Srisys.Web.Models.Activity", b =>
                {
                    b.HasOne("Srisys.Web.Models.Borrower", "BorrowedBy")
                        .WithMany()
                        .HasForeignKey("BorrowedById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Srisys.Web.Models.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Srisys.Web.Models.User", "ReceivedBy")
                        .WithMany()
                        .HasForeignKey("ReceivedById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Srisys.Web.Models.User", "ReleasedBy")
                        .WithMany()
                        .HasForeignKey("ReleasedById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Srisys.Web.Models.Borrower", "ReturnedBy")
                        .WithMany()
                        .HasForeignKey("ReturnedById")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Srisys.Web.Models.Adjustment", b =>
                {
                    b.HasOne("Srisys.Web.Models.Material", "Material")
                        .WithMany("Adjustments")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Srisys.Web.Models.Material", b =>
                {
                    b.HasOne("Srisys.Web.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("Srisys.Web.Models.Subcategory", "SubCategory")
                        .WithMany()
                        .HasForeignKey("SubCategoryId");

                    b.HasOne("Srisys.Web.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId");

                    b.HasOne("Srisys.Web.Models.Reference", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Srisys.Web.Models.Reference", b =>
                {
                    b.HasOne("Srisys.Web.Models.Reference", "ParentReference")
                        .WithMany()
                        .HasForeignKey("ParentReferenceId");

                    b.HasOne("Srisys.Web.Models.ReferenceType", "ReferenceType")
                        .WithMany()
                        .HasForeignKey("ReferenceTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Srisys.Web.Models.Subcategory", b =>
                {
                    b.HasOne("Srisys.Web.Models.Category", "Category")
                        .WithMany("Subcategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
