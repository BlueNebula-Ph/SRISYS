namespace Srisys.Web.DTO
{
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// View model for the MaterialFilterRequest entity.
    /// </summary>
    public class MaterialFilterRequest : FilterRequestBase
    {
        /// <summary>
        /// Gets or sets property Material Type.
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Gets or sets property Category Id.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets property SubCategory Id.
        /// </summary>
        public int SubCategoryId { get; set; }

        /// <summary>
        /// Gets or sets property Supplier Id.
        /// </summary>
        public int SupplierId { get; set; }
    }
}
