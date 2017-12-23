namespace Srisys.Web.Filters
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Srisys.Web.Common;
    using Srisys.Web.DTO;

    /// <summary>
    /// Action filter to check for duplicate borrower names
    /// </summary>
    public class CheckDuplicateBorrowerAttribute : ActionFilterAttribute
    {
        private readonly SrisysDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckDuplicateBorrowerAttribute"/> class.
        /// </summary>
        /// <param name="dbContext">The database context</param>
        public CheckDuplicateBorrowerAttribute(SrisysDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Checks for duplicate borrower names.
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

            var model = input as SaveBorrowerRequest;
            if (model == null)
            {
                return;
            }

            var entity = this.dbContext.Borrowers.FirstOrDefault(c => c.Name == model.Name && c.Id != model.Id && !c.IsDeleted);
            if (entity != null)
            {
                context.ModelState.AddModelError(Constants.ErrorMessage, "Borrower name already exists");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
