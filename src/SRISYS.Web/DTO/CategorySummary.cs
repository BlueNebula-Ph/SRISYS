namespace Srisys.Web.DTO
{
    using System.Collections.Generic;
    using System.Linq;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// View model for the CategorySummary entity.
    /// </summary>
    public class CategorySummary : DTOBase
    {
        private IEnumerable<SubcategorySummary> subcategories;

        /// <summary>
        /// Gets or sets the Category name property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets collection of subcategories property.
        /// </summary>
        public IEnumerable<SubcategorySummary> Subcategories
        {
            get { return this.subcategories.Where(a => !a.IsDeleted); }
            set { this.subcategories = value; }
        }
    }
}
