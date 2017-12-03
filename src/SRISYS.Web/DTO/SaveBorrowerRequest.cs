namespace Srisys.Web.DTO
{
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// View model for the SaveBorrowerRequest entity.
    /// </summary>
    public class SaveBorrowerRequest : DTOBase
    {
        /// <summary>
        /// Gets or sets the borrower name property.
        /// </summary>
        public string Name { get; set; }
    }
}
