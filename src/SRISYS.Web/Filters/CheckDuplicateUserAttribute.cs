namespace Srisys.Web.Filters
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Srisys.Web.Common;
    using Srisys.Web.DTO;

    /// <summary>
    /// Action filter to check for duplicate users.
    /// </summary>
    public class CheckDuplicateUserAttribute : ActionFilterAttribute
    {
        private readonly SrisysDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckDuplicateUserAttribute"/> class.
        /// </summary>
        /// <param name="dbContext">The database context</param>
        public CheckDuplicateUserAttribute(SrisysDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Checks for duplicate users.
        /// </summary>
        /// <param name="context">The context</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var input = context.ActionArguments["entity"];
            if (input == null)
            {
                return;
            }

            var model = input as SaveUserRequest;
            if (model == null)
            {
                return;
            }

            var entity = this.dbContext.Users.FirstOrDefault(c => c.Username == model.Username && c.Id != model.Id && !c.IsDeleted);
            if (entity != null)
            {
                context.ModelState.AddModelError(Constants.ErrorMessage, "Username already exists");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
