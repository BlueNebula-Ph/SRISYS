namespace Srisys.Web.DTO
{
    using BlueNebula.Common.DTOs;
    using System;

    /// <summary>
    /// View model for the MaterialSummary entity.
    /// </summary>
    public class MaterialSummary : DTOBase
    {
        /// <summary>
        /// Gets or sets property Material Type.
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// Gets or sets property Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets property Item Unit.
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets property Quantity.
        /// </summary>
        public double Quantity { get; set; }

        /// <summary>
        /// Gets or sets property Size.
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// Gets or sets property Model.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets property Brand.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets property Location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets property Category Id.
        /// </summary>
        public string Categoryname { get; set; }

        /// <summary>
        /// Gets or sets property Subcategory Id.
        /// </summary>
        public string SubCategoryName { get; set; }

        /// <summary>
        /// Gets or sets property IsUsed.
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// Gets or sets property Price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets property SupplierId.
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// Gets or sets property LastPurchaseDate.
        /// </summary>
        public DateTime LastPurchaseDate { get; set; }

        /// <summary>
        /// Gets or sets property MinimumStock.
        /// </summary>
        public double MinimumStock { get; set; }
    }
}
