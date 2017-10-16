namespace Srisys.Web.DTO
{
    using System;
    using System.Collections.Generic;
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
        /// Gets or sets the borrowed by property.
        /// </summary>
        public string BorrowedBy { get; set; }

        /// <summary>
        /// Gets or sets the released by property.
        /// </summary>
        public string ReleasedBy { get; set; }

        /// <summary>
        /// Gets or sets the activity details collection.
        /// </summary>
        public IEnumerable<ActivityDetailSummary> ActivityDetails { get; set; }
    }
}
