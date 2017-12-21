namespace Srisys.Web.DTO
{
    using System;

    /// <summary>
    /// Summary view of the adjustments for materials
    /// </summary>
    public class AdjustmentSummary
    {
        /// <summary>
        /// Gets or sets the date property.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the quantity adjusted.
        /// </summary>
        public double Quantity { get; set; }

        /// <summary>
        /// Gets or sets the adjustment type.
        /// </summary>
        public string AdjustmentType { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the receipt number.
        /// </summary>
        public string ReceiptNumber { get; set; }
    }
}
