namespace Srisys.Web.DTO
{
    using System;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// View model for the ActivitySummary entity.
    /// </summary>
    public class ActivitySummary : DTOBase
    {
        /// <summary>
        /// Gets or sets the activity date property.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the material id property.
        /// </summary>
        public int MaterialId { get; set; }

        /// <summary>
        /// Gets or sets the material id of the item borrowed.
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// Gets or sets the unit property.
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets the brand property.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the model property.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the size property.
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// Gets or sets the use property.
        /// </summary>
        public string Use { get; set; }

        /// <summary>
        /// Gets or sets the quantity borrowed property.
        /// </summary>
        public double QuantityBorrowed { get; set; }

        /// <summary>
        /// Gets or sets the borrowed by property.
        /// </summary>
        public string BorrowedBy { get; set; }

        /// <summary>
        /// Gets or sets the released by property.
        /// </summary>
        public string ReleasedBy { get; set; }

        /// <summary>
        /// Gets or sets the latest return date property.
        /// </summary>
        public DateTime LatestReturnDate { get; set; }

        /// <summary>
        /// Gets or sets the total quantity returned property.
        /// </summary>
        public double TotalQuantityReturned { get; set; }

        /// <summary>
        /// Gets or sets the returned by property.
        /// </summary>
        public string ReturnedBy { get; set; }

        /// <summary>
        /// Gets or sets the received by property.
        /// </summary>
        public string ReceivedBy { get; set; }

        /// <summary>
        /// Gets or sets the status property.
        /// 1 = Pending - if 1 or more borrowed item has not been returned.
        /// 2 = Complete - if all items are returned.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the balance of the item borrowed.
        /// </summary>
        public double Balance { get; set; }

        /// <summary>
        /// Gets or sets the action that occurred.
        /// Possible values: borrowed, used
        /// </summary>
        public string ActionPerformed { get; set; }
    }
}
