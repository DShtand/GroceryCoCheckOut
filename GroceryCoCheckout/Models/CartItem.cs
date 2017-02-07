using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCoCheckout.Models
{
    public class CartItem : GroceryItem
    {
        public int DiscountQuantity { get; set; }
        public int RegularQuantity { get; set; }
        public decimal DiscountPrice { get; set; }
        //If item is discounted already, ineligible for other promos
        public bool Discounted { get; set; } 

        public CartItem(GroceryItem gi)
        {
            this.ItemID = gi.ItemID;
            this.Name = gi.Name;
            this.RegularQuantity = 1;
            this.DiscountQuantity = 0;
            this.RegularPrice = gi.RegularPrice;
            this.DiscountPrice = this.RegularPrice;
        }
        public CartItem()
        {
            this.ItemID = ItemID;
            this.Name = Name;
            this.RegularQuantity = 1;
            this.DiscountQuantity = 0;
            this.RegularPrice = RegularPrice;
            this.DiscountPrice = DiscountPrice;
        }
    }
}
