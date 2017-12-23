namespace Srisys.Web.Filters
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using BlueNebula.Common.DTOs;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Srisys.Web.Common;

    /// <summary>
    /// The <see cref="InjectMetadataAttribute"/> injects metadata before a save action is performed.
    /// Metadata includes: createdby, createddatate, updatedby, updateddate
    /// </summary>
    public class InjectMetadataAttribute : ActionFilterAttribute
    {
        private readonly ActionType actionType;
        private readonly string propertyName;

        /// <summary>
        /// Initializes a new instance of the <see cref="InjectMetadataAttribute"/> class.
        /// </summary>
        /// <param name="actionType">The action type. Add or Edit</param>
        /// <param name="propertyName">The property name.</param>
        public InjectMetadataAttribute(ActionType actionType, string propertyName)
        {
            this.actionType = actionType;
            this.propertyName = propertyName;
        }

        /// <summary>
        /// Injects the meta data.
        /// </summary>
        /// <param name="context">The context passed</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var input = context.ActionArguments[this.propertyName];
            if (input == null)
            {
                return;
            }

            var model = input as SaveRequestBase;
            if (model == null)
            {
                return;
            }

            var sidValue = context.HttpContext.User.Claims.FirstOrDefault(a => a.Type == JwtRegisteredClaimNames.Sid)?.Value;
            if (string.IsNullOrEmpty(sidValue))
            {
                return;
            }

            if (int.TryParse(sidValue, out int userId))
            {
                if (this.actionType == ActionType.Create)
                {
                    model.CreatedBy = userId;
                    model.CreatedDate = DateTime.Now;
                }

                model.LastUpdatedBy = userId;
                model.LastUpdatedDate = DateTime.Now;
            }
        }
    }
}
