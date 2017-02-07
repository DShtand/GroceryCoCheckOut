using System;
using System.Collections.Generic;
using GroceryCoCheckout.Models;
using GroceryCoCheckout.Services;

namespace GroceryCoCheckout
{
    class Program
    {
        static void Main(string[] args)
        {
            List<GroceryItem> groceryItems;
            List<Promotion> promos;
            Cart cart = new Cart();

            //load file inputs
            groceryItems = IOService.LoadPrices();
            promos = IOService.LoadPromos();
            var inputItemNames = IOService.LoadCartItemNames();

            //add Items to cart by name from existing grocery items
            cart.AddListToCart(inputItemNames, groceryItems);
          
            //check which promotions are qualified for
            PromoService.CheckPromotions(promos, cart);
            cart.UpdateTotals();

            IOService.PrintCart(cart);

            Console.ReadKey();
        }
    }
}
