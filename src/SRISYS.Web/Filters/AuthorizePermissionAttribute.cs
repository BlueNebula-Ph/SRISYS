namespace Srisys.Web.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Srisys.Web.Common;

    public class AuthorizePermissionAttribute : TypeFilterAttribute
    {
        public AuthorizePermissionAttribute(PermissionItem item, PermissionAction action)
            : base(typeof(AuthorizePermissionFilter))
    {
            this.Arguments = new object[] { item, action };
        }
    }
}
