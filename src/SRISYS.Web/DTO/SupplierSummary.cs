namespace Srisys.Web.DTO
{
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// View model for the SupplierSummary entity.
    /// </summary>
    public class SupplierSummary : DTOBase
    {
        /// <summary>
        /// Gets or sets property Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets property Address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets property PhoneNumber.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets property Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets property OtherDetails.
        /// </summary>
        public string OtherDetails { get; set; }
    }
}
