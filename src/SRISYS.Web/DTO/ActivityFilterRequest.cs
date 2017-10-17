namespace Srisys.Web.DTO
{
    using System;
    using BlueNebula.Common.DTOs;
    using Srisys.Web.Common;

    /// <summary>
    /// View model for the ActivityFilterRequest entity.
    /// </summary>
    public class ActivityFilterRequest : FilterRequestBase
    {
        /// <summary>
        /// Gets or sets the activity date from property.
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Gets or sets the activity date to property.
        /// </summary>
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Gets or sets the borrowed by property.
        /// </summary>
        public string BorrowedBy { get; set; }

        /// <summary>
        /// Gets or sets the released by property.
        /// </summary>
        public string ReleasedBy { get; set; }

        /// <summary>
        /// Gets or sets the material id property.
        /// </summary>
        public int MaterialId { get; set; }

        /// <summary>
        /// Gets or sets the activity status property.
        /// </summary>
        public ActivityStatus? ActivityStatus { get; set; }
    }
}
