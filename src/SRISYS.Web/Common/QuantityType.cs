namespace Srisys.Web.Common
{
    /// <summary>
    /// The quantity type enum represents the quantity adjusted
    /// Possible values: Quantity, RemainingQuantity, Both
    /// </summary>
    public enum QuantityType
    {
        /// <summary>
        /// Represents the main quantity field.
        /// </summary>
        Quantity = 1,

        /// <summary>
        /// Represents the remaining quantity field.
        /// </summary>
        RemainingQuantity = 2,

        /// <summary>
        /// Represents both quantity fields.
        /// </summary>
        Both = 3,
    }
}
