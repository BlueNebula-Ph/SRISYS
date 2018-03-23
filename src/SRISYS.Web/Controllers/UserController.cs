namespace Srisys.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using AutoMapper;
    using BlueNebula.Common.Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Srisys.Web.Common;
    using Srisys.Web.DTO;
    using Srisys.Web.Filters;
    using Srisys.Web.Models;

    /// <summary>
    /// <see cref="UserController"/> class handles adding, editing, deleting and fetching of users.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    [ValidateModel]
    public class UserController : Controller
    {
        private readonly SrisysDbContext context;
        private readonly IMapper mapper;
        private readonly ISummaryListBuilder<ApplicationUser, UserSummary> builder;
        private readonly IPasswordHasher<ApplicationUser> passwordHasher;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="context">The DbContext</param>
        /// <param name="mapper">The mapper</param>
        /// <param name="builder">The summary list builder</param>
        /// <param name="passwordHasher">The tool for hashing passwords</param>
        public UserController(
            SrisysDbContext context,
            IMapper mapper,
            ISummaryListBuilder<ApplicationUser, UserSummary> builder,
            IPasswordHasher<ApplicationUser> passwordHasher)
        {
            this.context = context;
            this.mapper = mapper;
            this.builder = builder;
            this.passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Returns list of active <see cref="ApplicationUser"/>
        /// </summary>
        /// <param name="filter"><see cref="UserFilterRequest"/></param>
        /// <returns>List of Users</returns>
        [HttpPost("search", Name = "GetAllUsers")]
        public async Task<IActionResult> GetAll([FromBody]UserFilterRequest filter)
        {
            // get list of active users (not deleted)
            var list = this.context.Users
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // filter
            if (filter != null)
            {
                list = list.Where(a => a.Username.Contains(filter.SearchTerm) ||
                    a.Firstname.Contains(filter.SearchTerm) ||
                    a.Lastname.Contains(filter.SearchTerm));
            }

            // sort
            var ordering = $"Username {Constants.DefaultSortDirection}";
            if (!string.IsNullOrEmpty(filter?.SortBy))
            {
                ordering = $"{filter.SortBy} {filter.SortDirection}";
            }

            list = list.OrderBy(ordering);

            var entities = await this.builder.BuildAsync(list, filter);

            return this.Ok(entities);
        }

        /// <summary>
        /// Gets a specific <see cref="ApplicationUser"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>User summary object</returns>
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await this.context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (entity == null)
            {
                return this.NotFound(id);
            }

            var mappedEntity = this.mapper.Map<UserSummary>(entity);
            return this.Ok(mappedEntity);
        }

        /// <summary>
        /// Creates a <see cref="ApplicationUser"/>.
        /// </summary>
        /// <param name="entity">User to be created</param>
        /// <returns>User object</returns>
        [HttpPost]
        [ServiceFilter(typeof(CheckDuplicateUserAttribute))]
        public async Task<IActionResult> Create([FromBody]SaveUserRequest entity)
        {
            var user = this.mapper.Map<ApplicationUser>(entity);
            user.PasswordHash = this.passwordHasher.HashPassword(user, entity.Password);

            await this.context.Users.AddAsync(user);
            await this.context.SaveChangesAsync();

            return this.CreatedAtRoute("GetUser", new { id = user.Id }, entity);
        }

        /// <summary>
        /// Updates a specific <see cref="ApplicationUser"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="entity">entity</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(CheckDuplicateUserAttribute))]
        public async Task<IActionResult> Update(long id, [FromBody]SaveUserRequest entity)
        {
            var user = await this.context.Users.SingleOrDefaultAsync(t => t.Id == id);
            if (user == null)
            {
                return this.NotFound(id);
            }

            try
            {
                this.mapper.Map(entity, user);
                user.PasswordHash = this.passwordHasher.HashPassword(user, entity.Password);

                this.context.Update(user);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific <see cref="ApplicationUser"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var user = await this.context.Users.SingleOrDefaultAsync(t => t.Id == id);
            if (user == null)
            {
                return this.NotFound(id);
            }

            user.IsDeleted = true;
            this.context.Update(user);
            await this.context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}