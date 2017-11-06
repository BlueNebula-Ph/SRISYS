namespace Srisys.Web.DTO
{
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// <see cref="MaterialLookup"/> class represents data for dropdowns.
    /// </summary>
    public class MaterialLookup : DTOBase
    {
        /// <summary>
        /// Gets or sets the material name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the material unit.
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets the material brand.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the material model.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the material size.
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// Gets or sets the remaining quantity of the material.
        /// </summary>
        public double RemainingQuantity { get; set; }
    }
}
