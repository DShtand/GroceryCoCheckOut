using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCoCheckout.Models
{
    //See readme for more detailed explanation, important fields are the ItemIDs and Quantities
    public class Promotion
    {
        public string PromoType { get; private set; }
        public string PromoID { get; private set; }
        public string PromoName { get; private set; }
        public string BuyItemID { get; private set; }
        public string DealItemID { get; private set; }
        public int BuyItemQuantity { get; private set; }
        public int DealItemQuantity { get; private set; }
        public decimal Discount { get; private set; }

    }
}
