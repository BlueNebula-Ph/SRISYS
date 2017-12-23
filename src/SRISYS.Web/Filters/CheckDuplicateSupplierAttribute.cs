namespace Srisys.Web.Filters
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Srisys.Web.Common;
    using Srisys.Web.DTO;

    /// <summary>
    /// Action filter to check for duplicate suppliers
    /// </summary>
    public class CheckDuplicateSupplierAttribute : ActionFilterAttribute
    {
        private readonly SrisysDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckDuplicateSupplierAttribute"/> class.
        /// </summary>
        /// <param name="dbContext">The database context</param>
        public CheckDuplicateSupplierAttribute(SrisysDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Checks for duplicate suppliers.
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

            var model = input as SaveSupplierRequest;
            if (model == null)
            {
                return;
            }

            var entity = this.dbContext.Suppliers.FirstOrDefault(c => c.Name == model.Name && c.Id != model.Id && !c.IsDeleted);
            if (entity != null)
            {
                context.ModelState.AddModelError(Constants.ErrorMessage, "Supplier name already exists");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
