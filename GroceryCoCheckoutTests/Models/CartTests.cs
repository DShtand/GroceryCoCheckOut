using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryCoCheckout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCoCheckout.Models.Tests
{
    [TestClass()]
    public class CartTests
    {
        [TestMethod()]
        public void UpdateTotals_EmptyCartTest()
        {
            decimal regularTotalExpected = 0;
            decimal savingsExpected = 0;
            decimal subTotalExpected = 0;
            decimal taxExpected = 0;
            decimal totalExpected = 0;
        Cart cart = new Cart();
            cart.UpdateTotals();

            Assert.AreEqual(regularTotalExpected, cart.RegularTotal);
            Assert.AreEqual(savingsExpected, cart.Savings);
            Assert.AreEqual(subTotalExpected, cart.SubTotal);
            Assert.AreEqual(taxExpected, cart.Tax);
            Assert.AreEqual(totalExpected, cart.Total);
        }

        [TestMethod()]
        public void UpdateTotals_ItemInCart()
        {
            decimal regularTotalExpected = 30;
            decimal savingsExpected = 6;
            decimal subTotalExpected = 24;
            decimal taxExpected = 1.2m;
            decimal totalExpected = 25.2m;
            Cart cart = new Cart();
            cart.Items.Add(new CartItem()
            {
                ItemID = "GI999",
                Name = "Cantelope",
                RegularQuantity    = 2,
                DiscountQuantity = 3,
                RegularPrice = 6.00m,
                DiscountPrice = 4.00m,
                Discounted = true
            });
            cart.UpdateTotals();

            Assert.AreEqual(regularTotalExpected, cart.RegularTotal);
            Assert.AreEqual(savingsExpected, cart.Savings);
            Assert.AreEqual(subTotalExpected, cart.SubTotal);
            Assert.AreEqual(taxExpected, cart.Tax);
            Assert.AreEqual(totalExpected, cart.Total);
        }
    }
}