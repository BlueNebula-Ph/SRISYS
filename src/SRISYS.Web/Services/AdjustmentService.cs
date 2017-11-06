namespace Srisys.Web.Services
{
    using System;
    using Srisys.Web.Common;
    using Srisys.Web.Models;
    using Srisys.Web.Services.Interfaces;

    /// <summary>
    /// Class for material adjustments
    /// </summary>
    internal class AdjustmentService : IAdjustmentService
    {
        private readonly SrisysDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdjustmentService"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        public AdjustmentService(SrisysDbContext context)
        {
            this.context = context;
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
        /// <param name="adjustmentQuantity">Adjustment Quantity</param>
        /// <param name="adjustmentType">Adjustment Type</param>
        /// <param name="quantityType">The quantity to be adjusted</param>
        /// <param name="remarks">Remarks</param>
        public void ModifyQuantity(Material material, double adjustmentQuantity, AdjustmentType adjustmentType, QuantityType quantityType, string remarks)
        {
            if (material != null)
            {
                switch (quantityType)
                {
                    case QuantityType.Quantity:
                        this.AdjustQuantity(material, adjustmentType, adjustmentQuantity);
                        break;
                    case QuantityType.RemainingQuantity:
                        this.AdjustRemainingQuantity(material, adjustmentType, adjustmentQuantity);
                        break;
                    default:
                        this.AdjustQuantity(material, adjustmentType, adjustmentQuantity);
                        this.AdjustRemainingQuantity(material, adjustmentType, adjustmentQuantity);
                        break;
                }

                this.SaveAdjustment(material, adjustmentQuantity, adjustmentType, remarks);
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

        private void SaveAdjustment(Material material, double adjustmentQuantity, AdjustmentType adjustmentType, string remarks)
        {
            var adjustment = new Adjustment
            {
                MaterialId = material.Id,
                Date = DateTime.Today,
                AdjustmentType = Enum.GetName(typeof(AdjustmentType), adjustmentType),
                Quantity = adjustmentQuantity,
                Remarks = remarks,
            };

            this.context.Add(adjustment);
        }
    }
}
