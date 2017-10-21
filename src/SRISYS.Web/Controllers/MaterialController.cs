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
    using Models;
    using Srisys.Web.Common;
    using Srisys.Web.DTO;
    using Srisys.Web.Services.Interfaces;

    /// <summary>
    /// <see cref="MaterialController"/> class handles Material basic add, edit, delete and get.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MaterialController : Controller
    {
        private readonly SrisysDbContext context;
        private readonly IMapper mapper;
        private readonly ISummaryListBuilder<Material, MaterialSummary> builder;
        private readonly IAdjustmentService adjustmentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="mapper">Automapper</param>
        /// <param name="builder">Builder</param>
        public MaterialController(SrisysDbContext context, IMapper mapper, ISummaryListBuilder<Material, MaterialSummary> builder, IAdjustmentService adjustmentService)
        {
            this.context = context;
            this.mapper = mapper;
            this.builder = builder;
            this.adjustmentService = adjustmentService;
        }

        /// <summary>
        /// Returns list of active <see cref="Material"/>
        /// </summary>
        /// <param name="filter"><see cref="MaterialFilterRequest"/></param>
        /// <returns>List of Material</returns>
        [HttpPost("search", Name = "GetAllMaterials")]
        public async Task<IActionResult> GetAll([FromBody]MaterialFilterRequest filter)
        {
            // get list of active materials (not deleted)
            var list = this.context.Materials
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // filter
            if (!(filter?.TypeId).IsNullOrZero())
            {
                list = list.Where(c => c.TypeId == filter.TypeId);
            }

            if (!string.IsNullOrEmpty(filter?.SearchTerm))
            {
                list = list.Where(c => c.Name.ToLower().Contains(filter.SearchTerm.ToLower()));
            }

            if (!(filter?.CategoryId).IsNullOrZero())
            {
                list = list.Where(c => c.CategoryId == filter.CategoryId);
            }

            if (!(filter?.SubCategoryId).IsNullOrZero())
            {
                list = list.Where(c => c.SubCategoryId == filter.SubCategoryId);
            }

            if (!(filter?.SupplierId).IsNullOrZero())
            {
                list = list.Where(c => c.SupplierId == filter.SupplierId);
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
        /// Returns list of active <see cref="Material"/>
        /// </summary>
        /// <returns>List of Materials</returns>
        [HttpGet("lookup", Name = "GetMaterialLookup")]
        public IActionResult GetLookup()
        {
            // get list of active items (not deleted)
            var list = this.context.Materials
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // sort
            var ordering = $"Name {Constants.DefaultSortDirection}";

            list = list.OrderBy(ordering);

            var entities = list.ProjectTo<MaterialLookup>();

            return this.Ok(entities);
        }

        /// <summary>
        /// Gets a specific <see cref="Material"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Material</returns>
        [HttpGet("{id}", Name = "GetMaterial")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await this.context.Materials
                .AsNoTracking()
                .Include(c => c.Type)
                .Include(c => c.Supplier)
                .Include(c => c.Category)
                .Include(c => c.SubCategory)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (entity == null)
            {
                return this.NotFound(id);
            }

            var mappedEntity = this.mapper.Map<MaterialSummary>(entity);

            return this.Ok(mappedEntity);
        }

        /// <summary>
        /// Creates a <see cref="Material"/>.
        /// </summary>
        /// <param name="entity">entity to be created</param>
        /// <returns>Material</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaveMaterialRequest entity)
        {
            if (entity == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var material = this.mapper.Map<Material>(entity);
            await this.context.Materials.AddAsync(material);
            await this.context.SaveChangesAsync();

            return this.CreatedAtRoute("GetMaterial", new { id = material.Id }, entity);
        }

        /// <summary>
        /// Updates a specific <see cref="Material"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="entity">entity</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] SaveMaterialRequest entity)
        {
            if (entity == null || entity.Id == 0 || id == 0)
            {
                return this.BadRequest();
            }

            var material = await this.context.Materials.SingleOrDefaultAsync(t => t.Id == id);
            if (material == null)
            {
                return this.NotFound(id);
            }

            try
            {
                this.mapper.Map(entity, material);
                this.context.Update(material);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific <see cref="Material"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var material = await this.context.Materials.SingleOrDefaultAsync(t => t.Id == id);
            if (material == null)
            {
                return this.NotFound(id);
            }

            material.IsDeleted = true;
            this.context.Update(material);
            await this.context.SaveChangesAsync();

            return new NoContentResult();
        }

        /// <summary>
        /// Adjusts the quantities of a material.
        /// </summary>
        /// <param name="saveAdjustmentRequest">The adjustment to save</param>
        /// <returns>No content result</returns>
        [HttpPost("adjust")]
        public async Task<IActionResult> Adjust([FromBody]SaveAdjustmentRequest saveAdjustmentRequest)
        {
            if (saveAdjustmentRequest == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            // Fetch the material to be adjusted and attach it to the dbcontext.
            var material = await this.context.Materials
                .FindAsync(saveAdjustmentRequest.MaterialId);

            if (material == null)
            {
                return this.NotFound();
            }

            // Perform adjustment through the adjustment service.
            this.adjustmentService.ModifyQuantity(
                material,
                saveAdjustmentRequest.Quantity,
                saveAdjustmentRequest.AdjustmentType,
                QuantityType.Both,
                saveAdjustmentRequest.Remarks);

            // If the adjustment is a result of a purchase, update the last purchase date and price of the material.
            if (saveAdjustmentRequest.IsPurchase)
            {
                material.LastPurchaseDate = saveAdjustmentRequest.PurchaseDate;
                material.Price = saveAdjustmentRequest.UpdatePrice ?
                    saveAdjustmentRequest.Price :
                    material.Price;
            }

            // Add the adjustment to the context and save all changes.
            await this.context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}