namespace Srisys.Web.DTO
{
    using System.Collections.Generic;
    using BlueNebula.Common.DTOs;
    using Srisys.Web.Common;

    /// <summary>
    /// View model for the SaveActivityRequest entity.
    /// </summary>
    public class SaveActivityRequest
    {
        /// <summary>
        /// Gets or sets collection of activities to save
        /// </summary>
        public IEnumerable<ActivityRequest> Activities { get; set; }

        /// <summary>
        /// Gets or sets the type property.
        /// 1 = Borrow
        /// 2 = Return
        /// </summary>
        public ActivityType Type { get; set; }
    }
}
