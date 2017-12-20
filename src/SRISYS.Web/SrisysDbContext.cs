namespace Srisys.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Srisys.Web.Common;
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
        /// Gets or sets the adjustments db set.
        /// </summary>
        public DbSet<Adjustment> Adjustments { get; set; }

        /// <summary>
        /// Gets or sets the borrowers db set.
        /// </summary>
        public DbSet<Borrower> Borrowers { get; set; }

        /// <summary>
        /// Gets or sets the categories db set.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the subcategories db set.
        /// </summary>
        public DbSet<Subcategory> Subcategories { get; set; }

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
        /// Gets or sets the users db set.
        /// </summary>
        public DbSet<ApplicationUser> Users { get; set; }

        /// <summary>
        /// Seeds the database with test data.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static void Seed(IApplicationBuilder app)
        {
            using (var context = app.ApplicationServices.GetRequiredService<SrisysDbContext>())
            {
                // context.Database.EnsureDeleted();
                context.Database.Migrate();

                SeedReferences(context);
                SeedCategories(context);
                SeedSuppliers(context);
                context.SaveChanges();

                SeedMaterials(context);
                SeedBorrowers(context);
                SeedUsers(context);
                context.SaveChanges();

                SeedActivities(context);
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

            // Categories
            modelBuilder.Entity<Subcategory>()
                .HasOne(a => a.Category)
                .WithMany(a => a.Subcategories)
                .HasForeignKey(a => a.CategoryId);
        }

        private static void SeedReferences(SrisysDbContext context)
        {
            if (!context.ReferenceTypes.Any())
            {
                var referenceType = new ReferenceType { Code = "MaterialType" };

                var references = new List<Reference>
                {
                    new Reference { ReferenceType = referenceType, Code = Constants.Tool },
                    new Reference { ReferenceType = referenceType, Code = Constants.Consumable },
                };
                context.References.AddRange(references);
            }
        }

        private static void SeedCategories(SrisysDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category
                    {
                        Name = "CAT-001",
                        Subcategories = new List<Subcategory>
                        {
                            new Subcategory { Name = "SUBCAT-001" },
                            new Subcategory { Name = "SUBCAT-021" },
                        },
                    },
                    new Category
                    {
                        Name = "CAT-002",
                        Subcategories = new List<Subcategory>
                        {
                            new Subcategory { Name = "SUBCAT-003" },
                        },
                    },
                    new Category
                    {
                        Name = "CAT-003",
                        Subcategories = new List<Subcategory>
                        {
                            new Subcategory { Name = "SUBCAT-004" },
                        },
                    },
                };
                context.Categories.AddRange(categories);
            }
        }

        private static void SeedMaterials(SrisysDbContext context)
        {
            if (!context.Materials.Any())
            {
                var materials = new List<Material>
                {
                    new Material { TypeId = 2, Name = "ITEM-001", Unit = "PC", Quantity = 100, RemainingQuantity = 100, Size = "1", Model = "Model 1", Brand = "B-001", Location = "North", CategoryId = 1, SubCategoryId = 1, Price = 2, LastPurchaseDate = DateTime.Now, MinimumStock = 50 },
                    new Material { TypeId = 2, Name = "ITEM-002", Unit = "PC", Quantity = 200, RemainingQuantity = 200, Size = "5", Model = "Model 3", Brand = "B-001", Location = "North", CategoryId = 2, SubCategoryId = 3, Price = 7, LastPurchaseDate = DateTime.Now, MinimumStock = 10 },
                    new Material { TypeId = 1, Name = "ITEM-003", Unit = "PC", Quantity = 150, RemainingQuantity = 150, Size = "1", Model = "Model 5", Brand = "B-001", CategoryId = 1, SubCategoryId = 1 },
                };
                context.Materials.AddRange(materials);
            }
        }

        private static void SeedSuppliers(SrisysDbContext context)
        {
            if (!context.Suppliers.Any())
            {
                var suppliers = new List<Supplier>
                    {
                        new Supplier { Name = "Dave Grohl", Email = "dave@ff.com", Address = "Foo street", PhoneNumber = "339403", OtherDetails = "Foo" },
                        new Supplier { Name = "Lucky Sun", Email = "lucky@sun.com", Address = "Cavite", PhoneNumber = "3494039", OtherDetails = "Discount" },
                    };
                context.Suppliers.AddRange(suppliers);
            }
        }

        private static void SeedBorrowers(SrisysDbContext context)
        {
            if (!context.Borrowers.Any())
            {
                var borrowers = new List<Borrower>
                {
                    new Borrower { Name = "Charterstone" },
                    new Borrower { Name = "Geddy" },
                };
                context.Borrowers.AddRange(borrowers);
            }
        }

        private static void SeedActivities(SrisysDbContext context)
        {
            if (!context.Activities.Any())
            {
                var activities = new List<Activity>
                {
                    new Activity { Date = DateTime.Now, MaterialId = 3, BorrowedById = 1, QuantityBorrowed = 10, Status = Common.ActivityStatus.Pending },
                    new Activity { Date = DateTime.Now, MaterialId = 2, BorrowedById = 2, ReturnedById = 2, QuantityBorrowed = 15, TotalQuantityReturned = 15, Status = Common.ActivityStatus.Complete },
                };
                context.Activities.AddRange(activities);
            }
        }

        private static void SeedUsers(SrisysDbContext context)
        {
            if (!context.Users.Any())
            {
                var newUser = new ApplicationUser { Username = "TestUser", Firstname = "First", Lastname = "LastName", AccessRights = "admin,canView" };
                var password = new PasswordHasher<ApplicationUser>().HashPassword(newUser, "Test");
                newUser.PasswordHash = password;

                context.Users.Add(newUser);
            }
        }
    }
}
