namespace Srisys.Web.DTO
{
    using System;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// View model for the SaveActivityRequest entity.
    /// </summary>
    public class ActivityRequest : DTOBase
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
        /// Gets or sets the quantity.
        /// </summary>
        public double Quantity { get; set; }

        /// <summary>
        /// Gets or sets the borrowed by property.
        /// </summary>
        public int BorrowedById { get; set; }

        /// <summary>
        /// Gets or sets the released by property.
        /// </summary>
        public int? ReleasedById { get; set; }

        /// <summary>
        /// Gets or sets the returned by property.
        /// </summary>
        public int? ReturnedById { get; set; }

        /// <summary>
        /// Gets or sets the received by property.
        /// </summary>
        public int? ReceivedById { get; set; }

        /// <summary>
        /// Gets or sets the use property.
        /// </summary>
        public string Use { get; set; }
    }
}
