using FruitShop.Models;
using FruitShop.Utilities;

namespace FruitShop.Services
{
    public class FruitBuyer
    {
        private readonly FruitStore fruitStore;
        private readonly string buyerName;

        public FruitBuyer(FruitStore fruitStore, string buyerName)
        {
            this.fruitStore = fruitStore;
            this.buyerName = buyerName;
        }

        public async Task BuyFruitAsync(FruitType fruitType)
        {
            await Task.Delay(1000);
            var result = await fruitStore.TryBuyFruitAsync(fruitType);

            if (result.Item1)
                ConsoleLogger.LogSuccessfulPurchase(buyerName, result.Item2);
            else
                ConsoleLogger.LogUnsuccessfulPurchase(buyerName, fruitType);
        }
    }
}