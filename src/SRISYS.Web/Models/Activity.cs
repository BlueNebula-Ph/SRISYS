﻿namespace Srisys.Web.Models
{
    using System;
    using Srisys.Web.Common;

    /// <summary>
    /// View model for the Activity entity.
    /// </summary>
    public class Activity : ModelBase
    {
        /// <summary>
        /// Gets or sets the activity date property.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the material id of the item borrowed.
        /// </summary>
        public int MaterialId { get; set; }

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
        public ActivityStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the material navigation property.
        /// </summary>
        public Material Material { get; set; }
    }
}