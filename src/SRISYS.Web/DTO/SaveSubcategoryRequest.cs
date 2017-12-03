namespace Srisys.Web.DTO
{
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// View model for the SaveSubcategoryRequest entity.
    /// </summary>
    public class SaveSubcategoryRequest : DTOBase
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