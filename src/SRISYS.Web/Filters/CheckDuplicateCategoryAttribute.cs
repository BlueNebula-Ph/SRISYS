namespace Srisys.Web.Filters
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Srisys.Web.Common;
    using Srisys.Web.DTO;

    /// <summary>
    /// Action filter to check for duplicate category names.
    /// </summary>
    public class CheckDuplicateCategoryAttribute : ActionFilterAttribute
    {
        private readonly SrisysDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckDuplicateCategoryAttribute"/> class.
        /// </summary>
        /// <param name="dbContext">The database context</param>
        public CheckDuplicateCategoryAttribute(SrisysDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Checks for duplicate category.
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

            var model = input as SaveCategoryRequest;
            if (model == null)
            {
                return;
            }

            var entity = this.dbContext.Categories.FirstOrDefault(c => c.Name == model.Name && c.Id != model.Id && !c.IsDeleted);
            if (entity != null)
            {
                context.ModelState.AddModelError(Constants.ErrorMessage, "Category name already exists");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
