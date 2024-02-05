public class Program
{
    static void Main()
    {
        Async().Wait();
    }

    static async Task Async()
    {
        FruitShop fruitShop = new FruitShop();

        var appleSupplier = new FruitSupplier<Apple>(fruitShop);
        var orangeSupplier = new FruitSupplier<Orange>(fruitShop);
        var appleProducers = Enumerable.Range(1, 50).Select(i => Task.Run(() => appleSupplier.SupplyFruit(new Apple($"Apple {i}"))));
        var orangeProducers = Enumerable.Range(1, 50).Select(i => Task.Run(() => orangeSupplier.SupplyFruit(new Orange($"Orange {i}"))));

        var appleBuyers = Enumerable.Range(1, 50).Select(i => Task.Run(() => new FruitBuyer<Apple>(fruitShop, $"Apple Buyer {i}").BuyFruit()));
        var orangeBuyers = Enumerable.Range(1, 50).Select(i => Task.Run(() => new FruitBuyer<Orange>(fruitShop, $"Orange Buyer {i}").BuyFruit()));

        await Task.WhenAll(appleProducers.Concat(orangeProducers).Concat(appleBuyers).Concat(orangeBuyers));
    }
}