namespace Srisys.Web.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// The <see cref="PopulateCurrentUserAttribute"/> populates the model with the logged in user information.
    /// </summary>
    public class PopulateCurrentUserAttribute : ActionFilterAttribute
    {
        private readonly string modelName;

        /// <summary>
        /// Initializes a new instance of the <see cref="PopulateCurrentUserAttribute"/> class.
        /// </summary>
        /// <param name="modelName">The name of the model to populate</param>
        public PopulateCurrentUserAttribute(string modelName)
        {
            this.modelName = modelName;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments[this.modelName];
            if (param != null)
            {
                var 
            }

            base.OnActionExecuting(context);
        }
    }
}
