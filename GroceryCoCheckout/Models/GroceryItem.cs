using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCoCheckout.Models
{
    //Base item unit for a grocery store, no concern for quantities or relevant promotions
    public class GroceryItem
    {
        public string ItemID { get; set; }
        public string Name { get; set; }
        public decimal RegularPrice { get; set; }
           
    }
}
