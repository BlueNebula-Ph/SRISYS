namespace Srisys.Web.DTO
{
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// <see cref="BorrowerLookup"/> class represents data for dropdowns.
    /// </summary>
    public class BorrowerLookup : DTOBase
    {
        /// <summary>
        /// Gets or sets the borrower name.
        /// </summary>
        public string Name { get; set; }
    }
}
