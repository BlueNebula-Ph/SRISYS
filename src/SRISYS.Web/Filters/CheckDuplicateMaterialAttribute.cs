namespace Srisys.Web.Filters
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Srisys.Web.Common;
    using Srisys.Web.DTO;

    /// <summary>
    /// Action filter to check for duplicate materials.
    /// </summary>
    public class CheckDuplicateMaterialAttribute : ActionFilterAttribute
    {
        private readonly SrisysDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckDuplicateMaterialAttribute"/> class.
        /// </summary>
        /// <param name="dbContext">The database context</param>
        public CheckDuplicateMaterialAttribute(SrisysDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Checks for duplicate materials
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

            var model = input as SaveMaterialRequest;
            if (model == null)
            {
                return;
            }

            var entity = this.dbContext.Materials.FirstOrDefault(c => c.Name == model.Name && c.Id != model.Id && !c.IsDeleted);
            if (entity != null)
            {
                context.ModelState.AddModelError(Constants.ErrorMessage, "Material name already exists");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
