namespace Srisys.Web.DTO
{
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// View model for the BorrowerFilterRequest entity.
    /// </summary>
    public class BorrowerFilterRequest : FilterRequestBase
    {
        /// <summary>
        /// Gets or sets the borrower name property.
        /// </summary>
        public string Name { get; set; }
    }
}
