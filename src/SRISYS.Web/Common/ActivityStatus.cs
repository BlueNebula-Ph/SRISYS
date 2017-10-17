namespace Srisys.Web.Common
{
    /// <summary>
    /// The activity status enum.
    /// </summary>
    public enum ActivityStatus
    {
        /// <summary>
        /// Used when materials in activity are not yet returned
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Used when all materials in activity are returned
        /// </summary>
        Complete = 2,
    }
}
