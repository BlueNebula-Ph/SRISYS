namespace Srisys.Web.DTO
{
    using System.ComponentModel.DataAnnotations;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// View model for the SaveSupplierRequest entity.
    /// </summary>
    public class SaveSupplierRequest : SaveRequestBase
    {
        /// <summary>
        /// Gets or sets property Name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets property Address.
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets property PhoneNumber.
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets property Email.
        /// </summary>
        [EmailAddress(ErrorMessage = "Please input a valid email address.")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets property OtherDetails.
        /// </summary>
        public string OtherDetails { get; set; }
    }
}
