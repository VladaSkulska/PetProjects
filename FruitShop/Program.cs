using FruitShop.Models;
using FruitShop.Services;

namespace FruitShop
{
    public class Program
    {
        static async Task Main()
        {
            await MainAsync();
        }

        static async Task MainAsync()
        {
            FruitStore fruitStore = new FruitStore();

            var appleSupplier = new FruitSupplier(fruitStore);
            var orangeSupplier = new FruitSupplier(fruitStore);
            var appleProducers = Enumerable.Range(1, 50).Select(i => Task.Run(() => appleSupplier.SupplyFruitAsync(FruitType.Apple, $"Apple {i}")));
            var orangeProducers = Enumerable.Range(1, 50).Select(i => Task.Run(() => orangeSupplier.SupplyFruitAsync(FruitType.Orange, $"Orange {i}")));

            var appleBuyers = Enumerable.Range(1, 50).Select(i => Task.Run(() => new FruitBuyer(fruitStore, $"Apple Buyer {i}").BuyFruitAsync(FruitType.Apple)));

            var orangeBuyers = Enumerable.Range(1, 50).Select(i => Task.Run(() => new FruitBuyer(fruitStore, $"Orange Buyer {i}").BuyFruitAsync(FruitType.Orange)));


            await Task.WhenAll(appleProducers.Concat(orangeProducers).Concat(appleBuyers).Concat(orangeBuyers));
        }
    }
}