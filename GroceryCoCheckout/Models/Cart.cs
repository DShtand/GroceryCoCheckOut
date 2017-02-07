using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCoCheckout.Models
{
    public class Cart : ICart
    {
        public List<CartItem> Items { get; private set; }
        public decimal RegularTotal { get; private set; }
        public decimal Savings { get; private set;}
        public decimal SubTotal { get; private set; }
        public decimal Tax { get; private set; }
        public decimal Total { get; private set; }

        public Cart()
        {
            Items = new List<CartItem>();
            RegularTotal = 0;
        }
       
        private void AddToCart(GroceryItem item)
        {
            //If item not found in cart by ID, add, otherwise increment quantity
            int i = Items.FindIndex(x => x.ItemID == item.ItemID);
            if (i < 0)
            {
                Items.Add(new CartItem(item));
            }
            else
            {
                Items[i].RegularQuantity += 1;
            }
            
        }
        public void UpdateTotals()
        {
            foreach(CartItem item in Items)
            {
                //Regular total based on total quantity
                RegularTotal = RegularTotal + ((item.RegularQuantity + item.DiscountQuantity)*item.RegularPrice);
                Savings = Savings + ((item.RegularPrice - item.DiscountPrice) * item.DiscountQuantity);
            }
            SubTotal = RegularTotal - Savings;
            Tax = SubTotal * 0.05m;
            Total = SubTotal + Tax;
        }

        //Add list of items to cart where Item is a valid item at Grocery
        public void AddListToCart(List<string> items, List<GroceryItem> groceryItems)
        {
            foreach (string name in items)
            {
                int j = groceryItems.FindIndex(x => x.Name == name);
                if (j < 0)
                {
                    Console.WriteLine("Invalid Item in Cart: " + name);
                }
                else
                {
                    AddToCart(groceryItems[j]);
                }
            }
        }


    }
}
