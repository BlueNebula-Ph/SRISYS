namespace Srisys.Web.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Srisys.Web.Common;

    /// <summary>
    /// The request body for the saving adjustments.
    /// </summary>
    public class SaveAdjustmentRequest
    {
        /// <summary>
        /// Gets or sets the id of the material to be adjusted.
        /// </summary>
        [Required]
        public int MaterialId { get; set; }

        /// <summary>
        /// Gets or sets the type of adjustment performed.
        /// Possible values: Add, Deduct
        /// </summary>
        public AdjustmentType AdjustmentType { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the adjustment.
        /// </summary>
        [Required]
        public double Quantity { get; set; }

        /// <summary>
        /// Gets or sets the remarks of the adjustment.
        /// </summary>
        [Required]
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the adjustment is a result of a purchase.
        /// </summary>
        public bool IsPurchase { get; set; }

        /// <summary>
        /// Gets or sets the purchase date.
        /// Only used when the adjustment is a purchase.
        /// </summary>
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the price of a material should be updated.
        /// Only used when the adjustment is a purchase.
        /// </summary>
        public bool UpdatePrice { get; set; }

        /// <summary>
        /// Gets or sets the price of the purchase.
        /// Only used when the adjustment is a purchase.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the receipt number of a purchase.
        /// Only used when the adjustment is a purchase.
        /// </summary>
        public string ReceiptNumber { get; set; }
    }
}
