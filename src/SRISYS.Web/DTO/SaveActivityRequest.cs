namespace Srisys.Web.DTO
{
    using System;
    using System.Collections.Generic;
    using BlueNebula.Common.DTOs;
    using Srisys.Web.Common;

    /// <summary>
    /// View model for the SaveActivityRequest entity.
    /// </summary>
    public class SaveActivityRequest : DTOBase
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
        public IEnumerable<SaveActivityDetailRequest> ActivityDetails { get; set; }
    }
}
