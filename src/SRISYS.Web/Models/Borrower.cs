namespace Srisys.Web.Models
{
    using System;
    using Srisys.Web.Common;

    /// <summary>
    /// View model for the Borrower entity.
    /// </summary>
    public class Borrower : ModelBase
    {
        /// <summary>
        /// Gets or sets the borrower name property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
