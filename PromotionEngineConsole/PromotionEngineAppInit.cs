using PromotionEngine.Models;
using PromotionEngine.Service;
using System;
using System.Collections.Generic;

namespace PromotionEngineConsole
{

    /// <summary>
    /// Promotion Engine Application Initilisation
    /// </summary>
    public class PromotionEngineAppInit
    {
        /// <summary>
        /// The promotion calculation
        /// </summary>
        private readonly IPromotionCalculation promotionCalculation;
        /// <summary>
        /// Gets or sets the sku models.
        /// </summary>
        /// <value>
        /// The sku models.
        /// </value>
        private List<SKUModel> skuModels { get; set; }
        /// <summary>
        /// Gets or sets the promotion models.
        /// </summary>
        /// <value>
        /// The promotion models.
        /// </value>
        private List<PromotionModel> promotionModels { get; set; }
        /// <summary>
        /// Gets or sets the cart model.
        /// </summary>
        /// <value>
        /// The cart model.
        /// </value>
        private CartModel cartModel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="PromotionEngineAppInit" /> class.
        /// </summary>
        public PromotionEngineAppInit()
        {
            skuModels = new List<SKUModel>();
            promotionModels = new List<PromotionModel>();
            cartModel = new CartModel();
            promotionCalculation = new PromotionCalculation();

            PromotionEngineOptions();
        }

        /// <summary>
        /// Promotions engine options.
        /// </summary>
        public void PromotionEngineOptions()
        {
            Console.WriteLine("---------------- Promotion Engine Options -----------------");
            Console.WriteLine("(1) Add SKUs");
            Console.WriteLine("(2) Add Promotions");
            Console.WriteLine("(3) Buy SKUs (Cart)");
            Console.WriteLine("(4) View SKUs & Promotions");
            Console.WriteLine("(5) Exit");
            Console.WriteLine("---------------- Promotion Engine Options -----------------");

            try
            {
                int promitionEngineOptionSelection = Convert.ToInt32(Console.ReadLine());
                switch (promitionEngineOptionSelection)
                {
                    case 1: AddSKUs(); break;
                    case 2: AddPromotions(); break;
                    case 3: BuySKUs(); break;
                    case 4: ViewSKusAndPromotions(); break;
                    case 5: break;
                    default: Console.WriteLine("Invalid selection.!"); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("---------------- Error -----------------");
                Console.WriteLine(ex.Message);
                PromotionEngineOptions();
            }
        }

        /// <summary>
        /// Adds the skus.
        /// </summary>
        private void AddSKUs()
        {
            bool addMoreItem = true;

            while (addMoreItem)
            {
                var _skuModel = new SKUModel();

                Console.WriteLine("Enter SKU Name:");
                _skuModel.SKUName = Convert.ToString(Console.ReadLine().ToUpper());

                Console.WriteLine("Enter SKU Unit Price:");
                _skuModel.SKUUnitPrice = Convert.ToDouble(Console.ReadLine());

                skuModels.Add(_skuModel);

                //Add one SKU
                Console.WriteLine("Do you want to add one more SKU? (Y/N) :");

                addMoreItem = GetYesOrNoConfirmation();
            }

            Console.WriteLine("SKUs Added Successfully\n");
            PromotionEngineOptions();
        }

        /// <summary>
        /// Adds the promotions.
        /// </summary>
        private void AddPromotions()
        {
            if (skuModels.Count == 0)
            {
                Console.WriteLine("SKUs not found. Please add atleast one SKU to proceed.!\n");

                PromotionEngineOptions();
                return;
            }

            bool addMorePromotion = true;
            while (addMorePromotion)
            {
                var _promotionModel = new PromotionModel();

                Console.WriteLine("Enter Promotion Name:");
                _promotionModel.PromotionName = Convert.ToString(Console.ReadLine());

                bool addMorePromotionItems = true;
                while (addMorePromotionItems)
                {
                    var _promotionItem = new PromotionItem();
                    Console.WriteLine("Select Promotion Item (SKU) from the below list:");
                    _promotionItem.SKU = SelectSKU();
                    if (_promotionItem.SKU == null)
                    {
                        Console.WriteLine("SKU not found.!");
                        break;
                    }

                    Console.WriteLine("Enter Promotion Item (SKU) Quantity:");
                    _promotionItem.Quantity = Convert.ToInt32(Console.ReadLine());

                    _promotionModel.PromotionItems.Add(_promotionItem);
                    Console.WriteLine("Do you want to add another SKU to promotion? (Y/N):");
                    addMorePromotionItems = GetYesOrNoConfirmation();
                }


                Console.WriteLine("Select Promotion Discount Type \n (1) Fixed Price Discount \n (2) Percentage Discount");
                _promotionModel.DiscountType = (DiscountType)Convert.ToInt16(Console.ReadLine());

                if (_promotionModel.DiscountType == DiscountType.FixedPrice)
                    Console.WriteLine("Enter Fixed Price Discount:");
                else
                    Console.WriteLine("Enter Discount Percentage:");

                _promotionModel.DiscountPrice = Convert.ToDouble(Console.ReadLine());

                promotionModels.Add(_promotionModel);
                //Add one promotion
                Console.WriteLine("Do you want to add one more promotion? (Y/N) :");

                addMorePromotion = GetYesOrNoConfirmation();
            }

            PromotionEngineOptions();
        }

        /// <summary>
        /// Buy the skus.
        /// </summary>
        private void BuySKUs()
        {
            cartModel = new CartModel();
            Console.WriteLine("------- Welcome to Cart --------");

            bool addMoreSKUToCart = true;
            while (addMoreSKUToCart)
            {
                Console.WriteLine("Select SKU from the below list:");
                var _cartItem = new CartItem();
                _cartItem.SKU = SelectSKU();

                Console.WriteLine("Enter to quantity to buy:");
                _cartItem.Quantity = Convert.ToInt32(Console.ReadLine());

                cartModel.CartItems.Add(_cartItem);

                Console.WriteLine("Do you want to add one more to SKU to cart? (Y/N) :");
                addMoreSKUToCart = GetYesOrNoConfirmation();
            }

            //Calculate total value of cart with promotion & without promotion
            Console.WriteLine("--------------- Cart Details ---------------");
            Console.WriteLine("Purchased SKUs:");

            cartModel.TotalAmountWithourPromotion = 0;
            for (int i = 0; i < cartModel.CartItems.Count; i++)
            {
                Console.WriteLine(string.Format("{0}. {1} (Price/Unit: {2}) * {3}", i + 1, cartModel.CartItems[i].SKU.SKUName, cartModel.CartItems[i].SKU.SKUUnitPrice, cartModel.CartItems[i].Quantity));
                cartModel.TotalAmountWithourPromotion += cartModel.CartItems[i].SKU.SKUUnitPrice * cartModel.CartItems[i].Quantity;
            }
            Console.WriteLine(string.Format("Total cart value without promotion : {0}", cartModel.TotalAmountWithourPromotion));

            double totalAmountDiscount = promotionCalculation.CalculateDiscount(skuModels, promotionModels, cartModel);
            
            cartModel.TotalAmountWithPromotion = cartModel.TotalAmountWithourPromotion - totalAmountDiscount;
            Console.WriteLine(string.Format("Total cart value with promotion : {0}", cartModel.TotalAmountWithPromotion));

            PromotionEngineOptions();
        }

        /// <summary>
        /// Views the skus and promotions.
        /// </summary>
        private void ViewSKusAndPromotions()
        {
            Console.WriteLine("------- List of SKUs --------");
            for (int i = 0; i < skuModels.Count; i++)
            {
                Console.WriteLine(string.Format("({0}) {1} (Price/Unit:{2})", i + 1, skuModels[i].SKUName, skuModels[i].SKUUnitPrice));
            }
            Console.WriteLine();

            Console.WriteLine("------- Active Promotions --------");
            for (int i = 0; i < promotionModels.Count; i++)
            {
                Console.WriteLine(string.Format("({0}) {1}", i + 1, promotionModels[i].PromotionName));
            }
            Console.WriteLine();
            PromotionEngineOptions();
        }

        /// <summary>
        /// Gets the yes or no confirmation.
        /// </summary>
        /// <returns></returns>
        private bool GetYesOrNoConfirmation()
        {
            string addMoreSelection = Console.ReadLine();
            switch (addMoreSelection.ToUpper())
            {
                case "Y": return true; ;
                case "N": return false;
                default: return false;
            }
        }

        /// <summary>
        /// Selects the sku.
        /// </summary>
        /// <returns></returns>
        private SKUModel SelectSKU()
        {
            for (int i = 0; i < skuModels.Count; i++)
            {
                Console.WriteLine(string.Format("({0}) {1}", i + 1, skuModels[i].SKUName));
            }

            var selectedSKUIndex = Convert.ToInt32(Console.ReadLine());
            return skuModels[selectedSKUIndex - 1];
        }
    }
}
