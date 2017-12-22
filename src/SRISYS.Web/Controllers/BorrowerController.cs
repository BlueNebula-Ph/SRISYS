namespace Srisys.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BlueNebula.Common.Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Srisys.Web.Common;
    using Srisys.Web.DTO;
    using Srisys.Web.Filters;

    /// <summary>
    /// <see cref="BorrowerController"/> class handles adding, editing, deleting and fetching of borrowers.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class BorrowerController : Controller
    {
        private readonly SrisysDbContext context;
        private readonly IMapper mapper;
        private readonly ISummaryListBuilder<Borrower, BorrowerSummary> builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="BorrowerController"/> class.
        /// </summary>
        /// <param name="context">The DbContext</param>
        /// <param name="mapper">The mapper</param>
        /// <param name="builder">The summary list builder</param>
        public BorrowerController(SrisysDbContext context, IMapper mapper, ISummaryListBuilder<Borrower, BorrowerSummary> builder)
        {
            this.context = context;
            this.mapper = mapper;
            this.builder = builder;
        }

        /// <summary>
        /// Returns list of active <see cref="Borrower"/>
        /// </summary>
        /// <param name="filter"><see cref="BorrowerFilterRequest"/></param>
        /// <returns>List of Borrowers</returns>
        [HttpPost("search", Name = "GetAllBorrowers")]
        public async Task<IActionResult> GetAll([FromBody]BorrowerFilterRequest filter)
        {
            // get list of active borrowers (not deleted)
            var list = this.context.Borrowers
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // filter
            if (!string.IsNullOrEmpty(filter?.SearchTerm))
            {
                list = list.Where(c => c.Name.ToLower().Contains(filter.SearchTerm.ToLower()));
            }

            // sort
            var ordering = $"Name {Constants.DefaultSortDirection}";
            if (!string.IsNullOrEmpty(filter?.SortBy))
            {
                ordering = $"{filter.SortBy} {filter.SortDirection}";
            }

            list = list.OrderBy(ordering);

            var entities = await this.builder.BuildAsync(list, filter);

            return this.Ok(entities);
        }

        /// <summary>
        /// Returns list of active <see cref="Borrower"/>
        /// </summary>
        /// <returns>List of Borrower</returns>
        [HttpGet("lookup", Name = "GetBorrowerLookup")]
        public IActionResult GetLookup()
        {
            // get list of active items (not deleted)
            var list = this.context.Borrowers
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // sort
            var ordering = $"Name {Constants.DefaultSortDirection}";

            list = list.OrderBy(ordering);

            var entities = list.ProjectTo<BorrowerLookup>();

            return this.Ok(entities);
        }

        /// <summary>
        /// Gets a specific <see cref="Borrower"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Borrower summary object</returns>
        [HttpGet("{id}", Name = "GetBorrower")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await this.context.Borrowers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (entity == null)
            {
                return this.NotFound(id);
            }

            var mappedEntity = this.mapper.Map<BorrowerSummary>(entity);
            return this.Ok(mappedEntity);
        }

        /// <summary>
        /// Creates a <see cref="Borrower"/>.
        /// </summary>
        /// <param name="entity">Borrower to be created</param>
        /// <returns>Borrower object</returns>
        [HttpPost]
        [ServiceFilter(typeof(CheckDuplicateBorrowerAttribute))]
        public async Task<IActionResult> Create([FromBody]SaveBorrowerRequest entity)
        {
            if (entity == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var borrower = this.mapper.Map<Borrower>(entity);
            await this.context.Borrowers.AddAsync(borrower);
            await this.context.SaveChangesAsync();

            return this.CreatedAtRoute("GetBorrower", new { id = borrower.Id }, entity);
        }

        /// <summary>
        /// Updates a specific <see cref="Borrower"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="entity">entity</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(CheckDuplicateBorrowerAttribute))]
        public async Task<IActionResult> Update(long id, [FromBody]SaveBorrowerRequest entity)
        {
            if (entity == null || entity.Id == 0 || id == 0)
            {
                return this.BadRequest();
            }

            var borrower = await this.context.Borrowers.SingleOrDefaultAsync(t => t.Id == id);
            if (borrower == null)
            {
                return this.NotFound(id);
            }

            try
            {
                this.mapper.Map(entity, borrower);
                this.context.Update(borrower);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific <see cref="Borrower"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var borrower = await this.context.Borrowers.SingleOrDefaultAsync(t => t.Id == id);
            if (borrower == null)
            {
                return this.NotFound(id);
            }

            borrower.IsDeleted = true;
            this.context.Update(borrower);
            await this.context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}