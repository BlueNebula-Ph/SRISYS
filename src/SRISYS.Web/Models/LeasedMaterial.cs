namespace Srisys.Web.Models
{
    using System;

    /// <summary>
    /// View model for the LeasedMaterial entity.
    /// </summary>
    public class LeasedMaterial : ModelBase
    {
        /// <summary>
        /// Gets or sets property MaterialId.
        /// </summary>
        public int MaterialId { get; set; }

        /// <summary>
        /// Gets or sets property BorrowedBy.
        /// </summary>
        public string BorrowedBy { get; set; }

        /// <summary>
        /// Gets or sets property DateBorrowed.
        /// </summary>
        public DateTime? DateBorrowed { get; set; }

        /// <summary>
        /// Gets or sets property ReturnedBy.
        /// </summary>
        public string ReturnedBy { get; set; }

        /// <summary>
        /// Gets or sets property DateReturned.
        /// </summary>
        public DateTime? DateReturned { get; set; }

        /// <summary>
        /// Gets or sets property Quantity.
        /// </summary>
        public double Quantity { get; set; }


    }
}
