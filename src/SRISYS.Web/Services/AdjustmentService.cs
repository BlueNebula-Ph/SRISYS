namespace Srisys.Web.Services
{
    using AutoMapper;
    using Srisys.Web.Common;
    using Srisys.Web.DTO;
    using Srisys.Web.Models;
    using Srisys.Web.Services.Interfaces;

    /// <summary>
    /// Class for material adjustments
    /// </summary>
    internal class AdjustmentService : IAdjustmentService
    {
        private readonly SrisysDbContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdjustmentService"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="mapper">Automapper</param>
        public AdjustmentService(SrisysDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Method to adjust activity quantity
        /// </summary>
        /// <param name="activityType"><see cref="ActivityType"/></param>
        /// <param name="material"><see cref="Material"/></param>
        /// <param name="quantity">Quantity</param>
        public void ModifyMaterialQuantity(ActivityType activityType, Material material, double quantity)
        {
            if (material != null)
            {
                switch (activityType)
                {
                    case ActivityType.Borrow:
                        material.RemainingQuantity = material.RemainingQuantity + quantity;
                        break;
                    case ActivityType.Return:
                        material.RemainingQuantity = material.RemainingQuantity - quantity;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Method to adjust item quantities
        /// </summary>
        /// <param name="material"><see cref="Material"/></param>
        /// <param name="quantityType">The quantity to be adjusted</param>
        /// <param name="adjustmentRequest">The adjustment request</param>
        public void ModifyQuantity(Material material, QuantityType quantityType, SaveAdjustmentRequest adjustmentRequest)
        {
            if (material != null)
            {
                switch (quantityType)
                {
                    case QuantityType.Quantity:
                        this.AdjustQuantity(material, adjustmentRequest.AdjustmentType, adjustmentRequest.Quantity);
                        break;
                    case QuantityType.RemainingQuantity:
                        this.AdjustRemainingQuantity(material, adjustmentRequest.AdjustmentType, adjustmentRequest.Quantity);
                        break;
                    default:
                        this.AdjustQuantity(material, adjustmentRequest.AdjustmentType, adjustmentRequest.Quantity);
                        this.AdjustRemainingQuantity(material, adjustmentRequest.AdjustmentType, adjustmentRequest.Quantity);
                        break;
                }

                this.SaveAdjustment(adjustmentRequest);
            }
        }

        private void AdjustQuantity(Material material, AdjustmentType adjustmentType, double adjustmentQuantity)
        {
            material.Quantity = adjustmentType == AdjustmentType.Add ?
                            material.Quantity + adjustmentQuantity :
                            material.Quantity - adjustmentQuantity;
        }

        private void AdjustRemainingQuantity(Material material, AdjustmentType adjustmentType, double adjustmentQuantity)
        {
            material.RemainingQuantity = adjustmentType == AdjustmentType.Add ?
                            material.RemainingQuantity + adjustmentQuantity :
                            material.RemainingQuantity - adjustmentQuantity;
        }

        private void SaveAdjustment(SaveAdjustmentRequest adjustmentRequest)
        {
            var adjustment = this.mapper.Map<Adjustment>(adjustmentRequest);
            this.context.Add(adjustment);
        }
    }
}
