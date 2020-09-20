using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine.Models
{
    /// <summary>
    /// Cart Model
    /// </summary>
    public class CartModel
    {
        /// <summary>
        /// The cart items
        /// </summary>
        public List<CartItem> CartItems;

        /// <summary>
        /// The total amount withour promotion
        /// </summary>
        public double TotalAmountWithourPromotion;

        /// <summary>
        /// The total amount with promotion
        /// </summary>
        public double TotalAmountWithPromotion;
        /// <summary>
        /// Initializes a new instance of the <see cref="CartModel"/> class.
        /// </summary>
        public CartModel()
        {
            CartItems = new List<CartItem>();
        }
    }

    /// <summary>
    /// Cart Item
    /// </summary>
    public class CartItem
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
}
