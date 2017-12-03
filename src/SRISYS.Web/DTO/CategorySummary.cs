namespace Srisys.Web.DTO
{
    using System.Collections.Generic;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// View model for the CategorySummary entity.
    /// </summary>
    public class CategorySummary : DTOBase
    {
        /// <summary>
        /// Gets or sets the Category name property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets collection of subcategories property.
        /// </summary>
        public IEnumerable<SubcategorySummary> Subcategories { get; set; }
    }
}
