namespace Srisys.Web.DTO
{
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// View model for the ActivityDetailSummary entity.
    /// </summary>
    public class ActivityDetailSummary : DTOBase
    {
        /// <summary>
        /// Gets or sets the parent activity id.
        /// </summary>
        public int ActivityId { get; set; }

        /// <summary>
        /// Gets or sets the material id of the material borrowed.
        /// </summary>
        public int MaterialId { get; set; }

        /// <summary>
        /// Gets or sets the quantity borrowed.
        /// </summary>
        public double Quantity { get; set; }

        /// <summary>
        /// Gets or sets the quantity returned.
        /// </summary>
        public double QuantityReturned { get; set; }

        /// <summary>
        /// Gets or sets the name of the returnee.
        /// </summary>
        public string ReturnedBy { get; set; }

        /// <summary>
        /// Gets or sets the name of the receiver of the returned material.
        /// </summary>
        public string ReceivedBy { get; set; }

        /// <summary>
        /// Gets or sets the activity type of the material.
        /// </summary>
        public string ActivityType { get; set; }
    }
}