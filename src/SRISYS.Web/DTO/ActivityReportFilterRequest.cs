namespace Srisys.Web.DTO
{
    using System;

    /// <summary>
    /// View model for the ActivityReportFilterRequest entity.
    /// </summary>
    public class ActivityReportFilterRequest
    {
        /// <summary>
        /// Gets or sets the report date property.
        /// </summary>
        public DateTime? ReportDate { get; set; }

        /// <summary>
        /// Gets or sets the material type property.
        /// </summary>
        public int MaterialTypeId { get; set; }
    }
}
