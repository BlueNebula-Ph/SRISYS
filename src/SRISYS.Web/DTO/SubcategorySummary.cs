namespace Srisys.Web.DTO
{
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// View model for the SubcategorySummary entity.
    /// </summary>
    public class SubcategorySummary : DTOBase
    {
        /// <summary>
        /// Gets or sets the Subcategory name property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent category name property.
        /// </summary>
        public int CategoryId { get; set; }
    }
}
