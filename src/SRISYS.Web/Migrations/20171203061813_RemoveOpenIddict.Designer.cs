using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Srisys.Web;
using Srisys.Web.Common;

namespace Srisys.Web.Migrations
{
    [DbContext(typeof(SrisysDbContext))]
    [Migration("20171203061813_RemoveOpenIddict")]
    partial class RemoveOpenIddict
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Srisys.Web.Models.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BorrowedById");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("Date");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<DateTime>("LatestReturnDate");

                    b.Property<int>("MaterialId");

                    b.Property<double>("QuantityBorrowed");

                    b.Property<int?>("ReceivedById");

                    b.Property<int?>("ReleasedById");

                    b.Property<int?>("ReturnedById");

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

            modelBuilder.Entity("Srisys.Web.Models.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccessRights");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Firstname");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<string>("Lastname");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
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

            modelBuilder.Entity("Srisys.Web.Models.Activity", b =>
                {
                    b.HasOne("Srisys.Web.Models.Borrower", "BorrowedBy")
                        .WithMany()
                        .HasForeignKey("BorrowedById");

                    b.HasOne("Srisys.Web.Models.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Srisys.Web.Models.ApplicationUser", "ReceivedBy")
                        .WithMany()
                        .HasForeignKey("ReceivedById");

                    b.HasOne("Srisys.Web.Models.ApplicationUser", "ReleasedBy")
                        .WithMany()
                        .HasForeignKey("ReleasedById");

                    b.HasOne("Srisys.Web.Models.Borrower", "ReturnedBy")
                        .WithMany()
                        .HasForeignKey("ReturnedById");
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
