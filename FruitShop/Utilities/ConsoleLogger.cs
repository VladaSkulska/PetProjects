using FruitShop.Models;

namespace FruitShop.Utilities
{
    public class ConsoleLogger
    {
        public static void LogFruitArrival(string name)
        {
            Console.WriteLine($"Fruit '{name}' has arrived.");
        }

        public static void LogSuccessfulPurchase(string buyerName, string fruitName)
        {
            Console.WriteLine($"{buyerName} bought {fruitName}.");
        }

        public static void LogUnsuccessfulPurchase(string buyerName, FruitType fruitType)
        {
            Console.WriteLine($"{buyerName} couldn't buy a fruit of type {fruitType}, as there are none available.");
        }

        public static void LogSupplierSupply(FruitType fruitType)
        {
            Console.WriteLine($"Supplier supplied {fruitType}.");
        }
    }
}