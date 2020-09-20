using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine.Service
{
    /// <summary>
    /// Calculate the discount amount using the promotion
    /// </summary>
    /// <seealso cref="PromotionEngine.Service.IPromotionCalculation" />
    public class PromotionCalculation : IPromotionCalculation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PromotionCalculation"/> class.
        /// </summary>
        public PromotionCalculation() { }
        /// <summary>
        /// Calculates the discount.
        /// </summary>
        /// <param name="skus">The skus.</param>
        /// <param name="promotions">The promotions.</param>
        /// <param name="cart">The cart.</param>
        /// <returns></returns>
        public double CalculateDiscount(IEnumerable<SKUModel> skus, IEnumerable<PromotionModel> promotions, CartModel cart)
        {
            double totalAmountDiscount = 0;
            foreach (var promotion in promotions)
            {
                bool promotionMatched = false;

                int howManyTimePromotionNeedToApply = 0;

                //check if all the promotion items exists in cart
                var promotionSKUsCount = promotion.PromotionItems.Count();
                var matchedCartSKUs = promotion.PromotionItems.Select(x => x.SKU.SKUName).Intersect(cart.CartItems.Select(x => x.SKU.SKUName)).Count();

                if (promotionSKUsCount == matchedCartSKUs)
                {
                    //check if promotionItems quantity matches the cart items quantity
                    foreach (var promotionItem in promotion.PromotionItems)
                    {
                        var cartSKUItemQuantity = cart.CartItems.Find(x => x.SKU.SKUName.Equals(promotionItem.SKU.SKUName)).Quantity;
                        if (cartSKUItemQuantity >= promotionItem.Quantity)
                        {
                            howManyTimePromotionNeedToApply = cartSKUItemQuantity / promotionItem.Quantity;
                            promotionMatched = true;
                        }
                    }
                }

                if (promotionMatched)
                {
                    double promotionItemsTotal = 0;
                    foreach (var promotionItem in promotion.PromotionItems)
                    {
                        promotionItemsTotal += skus.Where(x => x.SKUName.Equals(promotionItem.SKU.SKUName)).
                            FirstOrDefault().SKUUnitPrice * promotionItem.Quantity;
                    }

                    if (promotion.DiscountType.Equals(DiscountType.FixedPrice))
                    {
                        totalAmountDiscount += (promotionItemsTotal - promotion.DiscountPrice) * howManyTimePromotionNeedToApply;
                    }
                    else
                    {
                        totalAmountDiscount += (promotionItemsTotal - (promotionItemsTotal * promotion.DiscountPrice) / 100)
                            * howManyTimePromotionNeedToApply;
                    }
                }
            }

            return totalAmountDiscount;
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
