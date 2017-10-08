﻿namespace Srisys.Web.DTO
{
    using BlueNebula.Common.DTOs;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// View model for the SaveSupplierRequest entity.
    /// </summary>
    public class SaveSupplierRequest : DTOBase
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
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets property OtherDetails.
        /// </summary>
        public string OtherDetails { get; set; }

        /// <summary>
        /// Gets or sets property Tag.
        /// </summary>
        public string Tag { get; set; }
    }
}