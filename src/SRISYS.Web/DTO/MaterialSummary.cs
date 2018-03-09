namespace Srisys.Web.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// View model for the MaterialSummary entity.
    /// </summary>
    public class MaterialSummary : DTOBase
    {
        private IEnumerable<AdjustmentSummary> adjustments;
        private IEnumberable<ActivitySummary> activities;

        /// <summary>
        /// Gets or sets the material type id.
        /// </summary>
        public int TypeId { get; set; }

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
        /// Gets or sets the category id property.
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets property Category Id.
        /// </summary>
        public string Categoryname { get; set; }

        /// <summary>
        /// Gets or sets the subcategory id property.
        /// </summary>
        public int? SubCategoryId { get; set; }

        /// <summary>
        /// Gets or sets property Subcategory Id.
        /// </summary>
        public string SubCategoryName { get; set; }

        /// <summary>
        /// Gets or sets the use property.
        /// Used to indicate which machine the material is used.
        /// </summary>
        public string Use { get; set; }

        /// <summary>
        /// Gets or sets property Price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the supplier id property.
        /// </summary>
        public int? SupplierId { get; set; }

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

        /// <summary>
        /// Gets or sets the remaining quantity property.
        /// </summary>
        public double RemainingQuantity { get; set; }

        /// <summary>
        /// Gets or sets the reorder quantity.
        /// Used for setting up a reorder report.
        /// Applicable to Consumable type of material.
        /// </summary>
        public double ReorderQuantity { get; set; }

        /// <summary>
        /// Gets or sets the adjustments made on this material.
        /// </summary>
        public IEnumerable<AdjustmentSummary> Adjustments
        {
            get { return this.adjustments.OrderByDescending(a => a.Date); }
            set { this.adjustments = value; }
        }

        /// <summary>
        /// Gets or sets the activities of this material.
        /// </summary>
        public IEnumerable<ActivitySummary> Activities
        {
            get { return this.activities.OrderByDescending(a => a.Date); }
            set { this.activities = value; }
        }
    }
}
