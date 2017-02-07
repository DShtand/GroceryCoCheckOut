using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryCoCheckout.Models;
using CsvHelper;
using System.IO;
using System.Net.NetworkInformation;
using GroceryCoCheckout.Inputs;

namespace GroceryCoCheckout.Services
{
    //Service class for helping with file input and console output
    public static class IOService
    {
        public static List<string> LoadCartItemNames()
        {
            var appSettings = ConfigurationManager.AppSettings;
            List<string> itemNames = new List<string>();
            Console.Write("Please begin scanning (Enter file - Sample is 'TextFile1.csv'):");
            string cartFile = Console.ReadLine();
            try
            {
                using (TextReader tr = File.OpenText(Path.Combine(appSettings["FilePath"], cartFile)))
                {
                    var csv = new CsvReader(tr);
                    csv.Configuration.HasHeaderRecord = false;

                    while (csv.Read())
                    {
                        string value;
                        for (int i = 0; csv.TryGetField<string>(i, out value); i++)
                        {
                            itemNames.Add(value);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (e is DirectoryNotFoundException)
                {
                    Console.WriteLine("Error reading from directory: " + appSettings["FilePath"]);
                }
                else if (e is FileNotFoundException)
                {
                    Console.WriteLine("File not found: " + cartFile);
                }
                else
                {
                    Console.WriteLine("Error :" + e.Message);
                }
                Console.WriteLine("Hit any key to exit");
                Console.ReadKey();
                Process.GetCurrentProcess().Kill();
            }
            return itemNames;
        }


        public static List<GroceryItem> LoadPrices()
        {
            var appSettings = ConfigurationManager.AppSettings;
            using (TextReader tr = File.OpenText(Path.Combine(appSettings["PricePath"], appSettings["PriceFile"])))
            {
                var csv = new CsvReader(tr);
                csv.Configuration.RegisterClassMap<PriceMapping>();

                return csv.GetRecords<GroceryItem>().ToList();
            }
        }

        public static List<Promotion> LoadPromos()
        {
            var appSettings = ConfigurationManager.AppSettings;
            using (TextReader tr = File.OpenText(Path.Combine(appSettings["PromoPath"], appSettings["PromoFile"])))
            {
                var csv = new CsvReader(tr);
                csv.Configuration.IgnorePrivateAccessor = true;
                csv.Configuration.RegisterClassMap<PromoMapping>();

                return csv.GetRecords<Promotion>().ToList();
            }

        }

        public static void PrintCart(Cart cart)
        {
            var orderedCartItems = cart.Items.OrderByDescending(s => s.Name.Length);
            //get longest item length for consistent pad
            int padLength = orderedCartItems.First().Name.Length + 12;
            foreach (CartItem item in cart.Items)
            {
                //Format and print item quantities and prices
                var quantityFormatted = (item.RegularQuantity + item.DiscountQuantity) + "x " + item.Name + ": ";
                var itemHeader = quantityFormatted.PadRight(padLength) +
                                    string.Format("{0:C}",
                                        ((item.DiscountQuantity*item.DiscountPrice) + (item.RegularQuantity*item.RegularPrice)));
                Console.WriteLine(itemHeader);
                if (item.RegularQuantity > 0)
                {
                    Console.WriteLine(("\t(" + item.RegularQuantity + "x @ " + string.Format("{0:C}", item.RegularPrice)) + ")");
                }
                if (item.DiscountQuantity > 0)
                {
                    Console.WriteLine(("\t(" + item.DiscountQuantity + "x @ " + string.Format("{0:C}", item.DiscountPrice)) + ")");
                }
                Console.WriteLine();
            }

            PrintCartTotals(cart);
            Console.WriteLine("\nThank you for shopping with GroceryCo!");
        }

        private static void PrintCartTotals(Cart cart)
        {
            int padLength = 23;
            Console.WriteLine("Before Savings: ".PadRight(padLength) + string.Format("{0:C}", cart.RegularTotal));
            Console.WriteLine("Savings: ".PadRight(padLength) + string.Format("{0:C}", cart.Savings));
            Console.WriteLine("Sub-Total: ".PadRight(padLength) + string.Format("{0:C}", cart.SubTotal));
            Console.WriteLine("GST: ".PadRight(padLength) + string.Format("{0:C}", cart.Tax));
            Console.WriteLine("Total Due: ".PadRight(padLength) + string.Format("{0:C}", cart.Total));
        }
    }
}
