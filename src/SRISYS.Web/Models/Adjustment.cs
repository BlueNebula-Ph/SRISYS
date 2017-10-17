namespace Srisys.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Viewmodel for the adjustment entity.
    /// </summary>
    public class Adjustment : ModelBase
    {
        /// <summary>
        /// Gets or sets the material id to adjust.
        /// </summary>
        [Required]
        public int MaterialId { get; set; }

        /// <summary>
        /// Gets or sets the adjustment date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the quantity adjusted.
        /// </summary>
        [Required]
        public double Quantity { get; set; }

        /// <summary>
        /// Gets or sets the type of adjustment made.
        /// 1 = Add, 2 = Deduct
        /// </summary>
        [Required]
        public string AdjustmentType { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the material navigation property
        /// </summary>
        public Material Material { get; set; }
    }
}