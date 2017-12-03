namespace Srisys.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// View model for the Material entity.
    /// </summary>
    public class Material : ModelBase
    {
        /// <summary>
        /// Gets or sets property Material Type.
        /// </summary>
        [Required]
        public int TypeId { get; set; }

        /// <summary>
        /// Gets or sets property Name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets property Item Unit.
        /// </summary>
        [Required]
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets property Quantity.
        /// </summary>
        public double Quantity { get; set; }

        /// <summary>
        /// Gets or sets the remaining quantity of a material.
        /// </summary>
        public double RemainingQuantity { get; set; }

        /// <summary>
        /// Gets or sets property Size.
        /// </summary>
        [Required]
        public string Size { get; set; }

        /// <summary>
        /// Gets or sets property Model.
        /// </summary>
        [Required]
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets property Brand.
        /// </summary>
        [Required]
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets property Location. Applicable to Consumable type of material
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets property Category Id. Applicable to Consumable type of material
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets property Subcategory Id. Applicable to Consumable type of material
        /// </summary>
        public int? SubCategoryId { get; set; }

        /// <summary>
        /// Gets or sets property Price. Applicable to Consumable type of material
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Gets or sets property SupplierId. Applicable to Consumable type of material
        /// </summary>
        public int? SupplierId { get; set; }

        /// <summary>
        /// Gets or sets property LastPurchaseDate. Applicable to Consumable type of material
        /// </summary>
        public DateTime? LastPurchaseDate { get; set; }

        /// <summary>
        /// Gets or sets property MinimumStock. Applicable to Consumable type of material
        /// </summary>
        public double? MinimumStock { get; set; }

        /// <summary>
        /// Gets or sets the use property.
        /// Used to indicate which machine the material is used.
        /// </summary>
        public string Use { get; set; }

        /// <summary>
        /// Gets or sets the reorder quantity.
        /// Used for setting up a reorder report.
        /// Applicable to Consumable type of material.
        /// </summary>
        public double? ReorderQuantity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets property IsDeleted.
        /// </summary>
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets property Material Type.
        /// </summary>
        public Reference Type { get; set; }

        /// <summary>
        /// Gets or sets property Category.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Gets or sets property SubCategory.
        /// </summary>
        public Subcategory SubCategory { get; set; }

        /// <summary>
        /// Gets or sets property Supplier.
        /// </summary>
        public Supplier Supplier { get; set; }

        /// <summary>
        /// Gets or sets the adjustments collection.
        /// </summary>
        public ICollection<Adjustment> Adjustments { get; set; }
    }
}
