namespace Srisys.Web.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// View model for the Category entity.
    /// </summary>
    public class Category : ModelBase
    {
        /// <summary>
        /// Gets or sets the Category name property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets collection of subcategories property.
        /// </summary>
        public ICollection<Subcategory> Subcategories { get; set; }
    }
}
