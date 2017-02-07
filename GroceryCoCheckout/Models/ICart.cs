using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCoCheckout.Models
{
    public interface ICart
    {
        void UpdateTotals();
        void AddListToCart(List<string> items, List<GroceryItem> groceryItems);
    }
}
