# GroceryCoCheckOut
Followup Exercise for Interview

This is a solution to the exercise posed following an interview. It was meant to be done in 3 days, but took 4+ due to personal time constraints.

Following 3 input files meant to define a hypothetical grocery stores merchandise, current promotions on said merchandise, and a shopper's cart full of items,
the program calculates the value of the items in the cart per item, and any savings from deals. Then a cart sub-total and total are given.


Promotions

  The listed requirements for accomodating promotions left a fair bit of room for assumptions. I've included what I hope is considered additional functionality, where a promotion can offer a discount on X item(s) after buying Y item(s) of a different type.
  
  There is an assumption that once an item has been discounted by 1 promotion, it is ineligible for further promotions. 

  The basic premise I followed in the structure of promotions (ignoring type and name), was to take the csv left to right as a deal might be read. (Required Item, Discounted Item, Required Quantity, Discounted Quantity, Discount), or Buy X of this Item, get Y of this Item for Discount% off.
  
  If the Discounted Item Quantity is 0, it's assumed the promotion is reflected right on the quantity of the bought item, like 'Buy 3, get 30% off' (If the discount is a flat value, % was manually calculated for the csv file) 
  
  
Design Choices

  Most of the design choices are based around a beginner's efforts to structure things best from an Object-Oriented perspective. For example, I felt my base GroceryItem object shouldn't be concerned with quantity or any discounts until it is in someones cart. Then those fields come into play as a Cart Item.
  
  The lack of testing, input validation, and interfaces was due to time constraints. I included minor examples as a proof of concept, and enjoyed the bit of learning it took to set them up.
  
  
Running
  
  Changing 3 path key values in app.config to '<projectroot>\GroceryCoCheckout\Inputs\' might first be necessary. As required, if GroceryCo staff wanted to change price/item/promotion info, they can either change the Path or File values to point to a different csv.
  
  Hit run and the console will prompt for a cart input file. The two sample files provided are 'TextFile1.csv' and 'IntenseCart.csv'. Type in either and an itemized receipt will be printed out.
  
  
