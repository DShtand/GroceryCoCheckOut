using CsvHelper.Configuration;
using GroceryCoCheckout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCoCheckout.Inputs
{
    public sealed class PriceMapping : CsvClassMap<GroceryItem>
    {
        public PriceMapping()
        {
            Map(m => m.ItemID).Name("ID");
            Map(m => m.Name).Name("Name");
            Map(m => m.RegularPrice).Name("Price");
        }

    }
}
