namespace Srisys.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BlueNebula.Common.Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Srisys.Web.Common;
    using Srisys.Web.DTO;
    using Srisys.Web.Filters;
    using Srisys.Web.Models;

    /// <summary>
    /// <see cref="SupplierController"/> class handles Supplier basic add, edit, delete and get.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SupplierController : Controller
    {
        private readonly SrisysDbContext context;
        private readonly IMapper mapper;
        private readonly ISummaryListBuilder<Supplier, SupplierSummary> builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="mapper">Automapper</param>
        /// <param name="builder">Builder</param>
        public SupplierController(SrisysDbContext context, IMapper mapper, ISummaryListBuilder<Supplier, SupplierSummary> builder)
        {
            this.context = context;
            this.mapper = mapper;
            this.builder = builder;
        }

        /// <summary>
        /// Returns list of active <see cref="Supplier"/>
        /// </summary>
        /// <param name="filter"><see cref="SupplierFilterRequest"/></param>
        /// <returns>List of Supplier</returns>
        [HttpPost("search", Name = "GetAllSuppliers")]
        public async Task<IActionResult> GetAll([FromBody]SupplierFilterRequest filter)
        {
            // get list of active materials (not deleted)
            var list = this.context.Suppliers
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // filter
            if (!string.IsNullOrEmpty(filter?.SearchTerm))
            {
                list = list.Where(c => c.Name.Contains(filter.SearchTerm));
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
        /// Returns list of active <see cref="Supplier"/>
        /// </summary>
        /// <returns>List of Suppliers</returns>
        [HttpGet("lookup", Name = "GetSupplierLookup")]
        public IActionResult GetLookup()
        {
            // get list of active items (not deleted)
            var list = this.context.Suppliers
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // sort
            var ordering = $"Name {Constants.DefaultSortDirection}";

            list = list.OrderBy(ordering);

            var entities = list.ProjectTo<SupplierLookup>();

            return this.Ok(entities);
        }

        /// <summary>
        /// Gets a specific <see cref="Supplier"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Supplier</returns>
        [HttpGet("{id}", Name = "GetSupplier")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await this.context.Suppliers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (entity == null)
            {
                return this.NotFound(id);
            }

            var mappedEntity = this.mapper.Map<SupplierSummary>(entity);

            return this.Ok(mappedEntity);
        }

        /// <summary>
        /// Creates a <see cref="Supplier"/>.
        /// </summary>
        /// <param name="entity">entity to be created</param>
        /// <returns>Supplier</returns>
        [HttpPost]
        [InjectMetadata(ActionType.Create, "entity")]
        [ServiceFilter(typeof(CheckDuplicateSupplierAttribute))]
        public async Task<IActionResult> Create([FromBody]SaveSupplierRequest entity)
        {
            if (entity == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var supplier = this.mapper.Map<Supplier>(entity);
            await this.context.Suppliers.AddAsync(supplier);
            await this.context.SaveChangesAsync();

            return this.CreatedAtRoute("GetSupplier", new { id = supplier.Id }, entity);
        }

        /// <summary>
        /// Updates a specific <see cref="Supplier"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="entity">entity</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        [InjectMetadata(ActionType.Update, "entity")]
        [ServiceFilter(typeof(CheckDuplicateSupplierAttribute))]
        public async Task<IActionResult> Update(long id, [FromBody]SaveSupplierRequest entity)
        {
            if (entity == null || entity.Id == 0 || id == 0)
            {
                return this.BadRequest();
            }

            var supplier = await this.context.Suppliers.SingleOrDefaultAsync(t => t.Id == id);
            if (supplier == null)
            {
                return this.NotFound(id);
            }

            try
            {
                this.mapper.Map(entity, supplier);
                this.context.Update(supplier);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific <see cref="Supplier"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var supplier = await this.context.Suppliers.SingleOrDefaultAsync(t => t.Id == id);
            if (supplier == null)
            {
                return this.NotFound(id);
            }

            supplier.IsDeleted = true;
            this.context.Update(supplier);
            await this.context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}