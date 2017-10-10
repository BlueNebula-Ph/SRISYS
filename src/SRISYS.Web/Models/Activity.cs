namespace Srisys.Web.Models
{
    using System;
    using System.Collections.Generic;

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
        public ICollection<ActivityDetail> ActivityDetails { get; set; }
    }
}
