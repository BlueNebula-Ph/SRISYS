﻿namespace Srisys.Web.Services.Interfaces
{
    using Srisys.Web.Common;
    using Srisys.Web.DTO;
    using Srisys.Web.Models;

    /// <summary>
    /// Defines the structure for AdjustmentService
    /// </summary>
    public interface IAdjustmentService
    {
        /// <summary>
        /// Method to adjust item current quantity when transaction is an order transaction
        /// </summary>
        /// <param name="material"><see cref="Material"/></param>
        /// <param name="quantityType">The quantity to adjust</param>
        /// <param name="adjustmentRequest">The adjustment request object</param>
        void ModifyQuantity(Material material, QuantityType quantityType, SaveAdjustmentRequest adjustmentRequest);

        /// <summary>
        /// Method to adjust activity quantity
        /// </summary>
        /// <param name="activityType"><see cref="ActivityType"/></param>
        /// <param name="material"><see cref="Material"/></param>
        /// <param name="quantity">Quantity</param>
        void ModifyMaterialQuantity(ActivityType activityType, Material material, double quantity);
    }
}
