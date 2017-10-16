namespace Srisys.Web.DTO
{
    using BlueNebula.Common.DTOs;
    using Srisys.Web.Common;

    /// <summary>
    /// View model for the SaveActivityDetailRequest entity.
    /// </summary>
    public class SaveActivityDetailRequest : DTOBase
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
        /// Gets or sets the activity type.
        /// </summary>
        public ActivityType ActivityType { get; set; }
    }
}
