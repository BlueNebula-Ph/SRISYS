namespace Srisys.Web.Configuration
{
    /// <summary>
    /// The configuration options for authentication
    /// </summary>
    public class AuthenticationOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationOptions"/> class.
        /// </summary>
        public AuthenticationOptions()
        {
        }

        /// <summary>
        /// Gets or sets the issuer value
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the audience value
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Gets or sets the key value
        /// </summary>
        public string Key { get; set; }
    }
}
