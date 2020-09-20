using PromotionEngine.Models;
using System;
using System.Collections.Generic;

namespace PromotionEngine.Service
{
    /// <summary>
    /// Interface Promotion Calculation
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IPromotionCalculation : IDisposable
    {
        /// <summary>
        /// Calculates the discount.
        /// </summary>
        /// <param name="skus">The skus.</param>
        /// <param name="promotions">The promotions.</param>
        /// <param name="cart">The cart.</param>
        /// <returns></returns>
        double CalculateDiscount(IEnumerable<SKUModel> skus, IEnumerable<PromotionModel> promotions, CartModel cart);
    }
}