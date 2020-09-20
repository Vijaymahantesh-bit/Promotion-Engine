using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine.Models
{
    /// <summary>
    /// Promotion Model
    /// </summary>
    public class PromotionModel
    {
        /// <summary>
        /// Gets or sets the name of the promotion.
        /// </summary>
        /// <value>
        /// The name of the promotion.
        /// </value>
        public string PromotionName { get; set; }
        /// <summary>
        /// Gets or sets the promotion items.
        /// </summary>
        /// <value>
        /// The promotion items.
        /// </value>
        public List<PromotionItem> PromotionItems { get; set; }
        /// <summary>
        /// Gets or sets the type of the discount.
        /// </summary>
        /// <value>
        /// The type of the discount.
        /// </value>
        public DiscountType DiscountType { get; set; }
        /// <summary>
        /// Gets or sets the discount price.
        /// </summary>
        /// <value>
        /// The discount price.
        /// </value>
        public double DiscountPrice { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PromotionModel"/> class.
        /// </summary>
        public PromotionModel()
        {
            PromotionItems = new List<PromotionItem>();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PromotionItem
    {
        /// <summary>
        /// Gets or sets the sku.
        /// </summary>
        /// <value>
        /// The sku.
        /// </value>
        public SKUModel SKU { get; set; }
        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public int Quantity { get; set; }
    }

    /// <summary>
    /// Discount Type
    /// </summary>
    public enum DiscountType
    {
        /// <summary>
        /// The fixed price
        /// </summary>
        FixedPrice = 1,
        /// <summary>
        /// The percentage
        /// </summary>
        Percentage = 2
    }
}
