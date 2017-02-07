using GroceryCoCheckout.Models;
using System;
using System.Collections.Generic;

namespace GroceryCoCheckout.Services
{
    public static class PromoService
    {
        public static void CheckPromotions(List<Promotion> promos, Cart cart )
        {
            //Not bothering ordering which promos are checked first as assumption is there isn't multiple promotions concerning a single Item
            foreach(Promotion promo in promos)
            {
                //Is required Item from current promo in cart
                int cartBuyIndex = cart.Items.FindIndex(x => x.ItemID == promo.BuyItemID);
                
                //Is deal Item from current promo in cart
                int cartDealIndex = cart.Items.FindIndex(x => x.ItemID == promo.DealItemID);

                if (cartBuyIndex > -1 && cartDealIndex > -1)
                {
                    if (!cart.Items[cartDealIndex].Discounted && cart.Items[cartBuyIndex].RegularQuantity >= promo.BuyItemQuantity)
                    //Item to be discounted is still eligible and required bought Item quantity is met 
                    {
                        //Deal quantity 0 indicates discount will apply to whole group (1 to X Items) (IE, Buy 3 Apples for 25% Off)
                        if (promo.DealItemQuantity == 0)
                        {
                   
                            //Number of times Promotion requirements are met
                            decimal promoMultiplier = SetPromoMultiplier(cart, cartBuyIndex, promo);

                            //Set discount price and update regular and discounted quantities of Item
                            cart.Items[cartDealIndex].DiscountPrice = SetDiscountPrice(cart, cartDealIndex, promo);

                            //Discount quantity based on minimum between # of bought items required per promo, and # of items bought.
                            cart.Items[cartDealIndex].DiscountQuantity =
                                Math.Min((int) (promo.BuyItemQuantity*promoMultiplier),
                                    cart.Items[cartDealIndex].RegularQuantity);

                            //Subract discounted item quantity from regular quantity for new regular quantity
                            cart.Items[cartDealIndex].RegularQuantity = SetRegularQuantity(cart, cartDealIndex);

                            //Set deal Item to discounted so it's ineligible for further promos
                            cart.Items[cartDealIndex].Discounted = true;
                        }

                        //Buy and Item IDs equal indicates deal is on same Item as one being bought, 
                        //so remove required buy quantity from eligible deal quantity (IE, Buy 1 Banana, get 1 50% Off)
                        else if (promo.BuyItemID == promo.DealItemID)
                        {
                            decimal promoMultiplier = SetPromoMultiplier(cart, cartDealIndex, promo);


                            //Set discount price and update regular and discounted quantities of Item
                            cart.Items[cartDealIndex].DiscountPrice = SetDiscountPrice(cart, cartDealIndex, promo);

                            //My best PleaseDontLookInMe() 
                            //Add Promo buy quantity and deal quantity to get full set on deal, then Mod quantity bought by it and subract Promo Buy quantity to get 
                            //if any partial set is eligible for discount. If result is negative, add 0 instead
                            cart.Items[cartDealIndex].DiscountQuantity =
                                (int) (promoMultiplier*promo.DealItemQuantity) +
                                Math.Max((cart.Items[cartBuyIndex].RegularQuantity % (promo.BuyItemQuantity + promo.DealItemQuantity)) - promo.BuyItemQuantity, 0);

                            cart.Items[cartDealIndex].RegularQuantity = SetRegularQuantity(cart, cartDealIndex);
        
                            cart.Items[cartDealIndex].Discounted = true;
                        }
                        //Promotion requires buying one Item type for a deal on another (Cross Promotion) (IE, Buy Banana, Get Orange Free)
                        else if (promo.BuyItemID != promo.DealItemID)
                        {
                            //Don't include promo deal quantity as factor of set
                            decimal promoMultiplier = SetCrossPromoMultiplier(cart, cartBuyIndex, promo);


                            //Set discount price and update regular and discounted quantities of Item
                            cart.Items[cartDealIndex].DiscountPrice = SetDiscountPrice(cart, cartDealIndex, promo);

                            //Take min of # of deal items eligible for discount, and # of deal items bought
                            cart.Items[cartDealIndex].DiscountQuantity =
                                Math.Min((int)(promoMultiplier * promo.DealItemQuantity),
                                cart.Items[cartDealIndex].RegularQuantity);

                            cart.Items[cartDealIndex].RegularQuantity = SetRegularQuantity(cart, cartDealIndex);

                            cart.Items[cartDealIndex].Discounted = true;
                        }
                    }
                }
            }
        }
  

        private static int SetRegularQuantity(Cart cart, int cartDealIndex)
        {
            return cart.Items[cartDealIndex].RegularQuantity -
                   cart.Items[cartDealIndex].DiscountQuantity;
        }

        private static decimal SetPromoMultiplier(Cart cart, int cartBuyIndex, Promotion promo)
        {
            return Math.Floor((decimal) cart.Items[cartBuyIndex].RegularQuantity/(promo.BuyItemQuantity + promo.DealItemQuantity));
        }

        private static decimal SetCrossPromoMultiplier(Cart cart, int cartBuyIndex, Promotion promo)
        {
            return Math.Floor((decimal)cart.Items[cartBuyIndex].RegularQuantity / promo.BuyItemQuantity);
        }

        private static decimal SetDiscountPrice(Cart cart, int cartDealIndex, Promotion promo)
        {
            return cart.Items[cartDealIndex].RegularPrice * (1 - promo.Discount);
        }
    }
}
