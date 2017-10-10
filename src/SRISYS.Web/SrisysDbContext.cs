namespace Srisys.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Srisys.Web.Models;

    /// <summary>
    /// DBContenxt for Srisys.
    /// </summary>
    public class SrisysDbContext : DbContext, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SrisysDbContext"/> class.
        /// </summary>
        /// <param name="options">Context options</param>
        public SrisysDbContext(DbContextOptions<SrisysDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the materials db set.
        /// </summary>
        public DbSet<Material> Materials { get; set; }

        /// <summary>
        /// Gets or sets the activities db set.
        /// </summary>
        public DbSet<Activity> Activities { get; set; }

        /// <summary>
        /// Gets or sets the activity details db set.
        /// </summary>
        public DbSet<ActivityDetail> ActivityDetails { get; set; }

        /// <summary>
        /// Gets or sets the adjustments db set.
        /// </summary>
        public DbSet<Adjustment> Adjustments { get; set; }

        /// <summary>
        /// Gets or sets the suppliers db set.
        /// </summary>
        public DbSet<Supplier> Suppliers { get; set; }

        /// <summary>
        /// Gets or sets the references db set.
        /// </summary>
        public DbSet<Reference> References { get; set; }

        /// <summary>
        /// Gets or sets the referencetypes db set.
        /// </summary>
        public DbSet<ReferenceType> ReferenceTypes { get; set; }

        /// <summary>
        /// Seeds the database with test data.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static void Seed(IApplicationBuilder app)
        {
            using (var context = app.ApplicationServices.GetRequiredService<SrisysDbContext>())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                SeedReferenceTypes(context);
                SeedReferences(context);
                SeedMaterials(context);

                context.SaveChanges();
            }
        }

        /// <summary>
        /// This method sets up the foreign keys of entities.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Adjustments
            modelBuilder.Entity<Adjustment>()
                .HasOne(a => a.Material)
                .WithMany(a => a.Adjustments)
                .HasForeignKey(a => a.MaterialId);

            // Activity
            modelBuilder.Entity<ActivityDetail>()
                .HasOne(a => a.Activity)
                .WithMany(a => a.ActivityDetails)
                .HasForeignKey(a => a.ActivityId);
        }

        private static void SeedReferenceTypes(SrisysDbContext context)
        {
            if (!context.ReferenceTypes.Any())
            {
                var referenceTypes = new List<ReferenceType>
                {
                    new ReferenceType { Code = "Category" },
                    new ReferenceType { Code = "SubCategory" },
                    new ReferenceType { Code = "MaterialType" },
                };
                context.ReferenceTypes.AddRange(referenceTypes);
            }
        }

        private static void SeedReferences(SrisysDbContext context)
        {
            if (!context.ReferenceTypes.Any())
            {
                var references = new List<Reference>
                {
                    new Reference { ReferenceTypeId = 1, Code = "CAT-001" },
                    new Reference { ReferenceTypeId = 1, Code = "CAT-002" },
                    new Reference { ReferenceTypeId = 1, Code = "CAT-003" },
                    new Reference { ReferenceTypeId = 2, Code = "SUBCAT-001", ParentReferenceId = 1 },
                    new Reference { ReferenceTypeId = 2, Code = "SUBCAT-021", ParentReferenceId = 1 },
                    new Reference { ReferenceTypeId = 2, Code = "SUBCAT-003", ParentReferenceId = 1 },
                    new Reference { ReferenceTypeId = 2, Code = "SUBCAT-004", ParentReferenceId = 2 },
                    new Reference { ReferenceTypeId = 3, Code = "Consumable" },
                    new Reference { ReferenceTypeId = 3, Code = "Tool" },
                };
                context.References.AddRange(references);
            }
        }

        private static void SeedMaterials(SrisysDbContext context)
        {
            if (!context.Materials.Any())
            {
                var materials = new List<Material>
                {
                    new Material { TypeId = 8, Name = "ITEM-001", Unit = "PC", Quantity = 100, Size = "1", Model = "Model 1", Brand = "B-001", Location = "North", CategoryId = 1, SubCategoryId = 4, Price = 2, LastPurchaseDate = DateTime.Now, MinimumStock = 50 },
                    new Material { TypeId = 8, Name = "ITEM-002", Unit = "PC", Quantity = 200, Size = "5", Model = "Model 3", Brand = "B-001", Location = "North", CategoryId = 2, SubCategoryId = 4, Price = 7, LastPurchaseDate = DateTime.Now, MinimumStock = 10 },
                    new Material { TypeId = 9, Name = "ITEM-003", Unit = "PC", Quantity = 150, Size = "1", Model = "Model 5", Brand = "B-001" },
                };
                context.Materials.AddRange(materials);
            }
        }
    }
}
