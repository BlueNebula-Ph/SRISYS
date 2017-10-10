namespace Srisys.Web.DTO
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// View model for the SaveMaterialRequest entity.
    /// </summary>
    public class SaveMaterialRequest : DTOBase
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
        /// Gets or sets a value indicating whether the material is used.
        /// Applicable to Consumable type of material
        /// </summary>
        [DefaultValue(false)]
        public bool IsUsed { get; set; }

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
        /// Gets or sets property Tag.
        /// </summary>
        public string Tag { get; set; }
    }
}
