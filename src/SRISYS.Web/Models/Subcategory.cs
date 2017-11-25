namespace Srisys.Web.Models
{
    using System;
    using Srisys.Web.Common;

    /// <summary>
    /// View model for the Subcategory entity.
    /// </summary>
    public class Subcategory : ModelBase
    {
        /// <summary>
        /// Gets or sets the Subcategory name property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the parent category name property.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the Category navigation property.
        /// </summary>
        public Category Category { get; set; }
    }
}
