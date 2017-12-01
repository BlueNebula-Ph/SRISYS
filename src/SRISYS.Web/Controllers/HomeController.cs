namespace Srisys.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The default controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// The index action
        /// </summary>
        /// <returns>The index view</returns>
        public IActionResult Index()
        {
            return this.View();
        }
    }
}