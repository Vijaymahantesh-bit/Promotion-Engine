using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine.Models
{
    /// <summary>
    /// SKU Model
    /// </summary>
    public class SKUModel
    {
        /// <summary>
        /// Gets or sets the name of the sku.
        /// </summary>
        /// <value>
        /// The name of the sku.
        /// </value>
        public string SKUName { get; set; }

        /// <summary>
        /// Gets or sets the sku unit price.
        /// </summary>
        /// <value>
        /// The sku unit price.
        /// </value>
        public double SKUUnitPrice { get; set; }
    }
}
