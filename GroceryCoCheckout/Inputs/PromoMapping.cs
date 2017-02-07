using CsvHelper.Configuration;
using GroceryCoCheckout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCoCheckout.Inputs
{
    public sealed class PromoMapping : CsvClassMap<Promotion>
    {
        public PromoMapping()
        {
            Map(m => m.PromoType).Name("PromoType");
            Map(m => m.PromoID).Name("ID");
            Map(m => m.PromoName).Name("Name");
            Map(m => m.BuyItemID).Name("BuyItemID");
            Map(m => m.DealItemID).Name("DealItemID");
            Map(m => m.BuyItemQuantity).Name("BuyQuantity");
            Map(m => m.DealItemQuantity).Name("DealQuantity");
            Map(m => m.Discount).Name("Discount");
        }
    }
}
