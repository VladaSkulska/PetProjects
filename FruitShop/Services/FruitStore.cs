using FruitShop.Models;
using FruitShop.Utilities;
using System.Collections.Concurrent;

namespace FruitShop.Services
{
    public class FruitStore
    {
        private ConcurrentQueue<(FruitType, string)> fruits = new();

        public async Task AddFruitAsync(FruitType fruitType, string name)
        {
            await Task.Run(() =>
            {
                fruits.Enqueue((fruitType, name));
            });

            ConsoleLogger.LogFruitArrival(name);
        }

        public async Task<(bool, string)> TryBuyFruitAsync(FruitType fruitType)
        {
            (bool, string) result = await Task.Run(async() =>
            {
                (FruitType, string) boughtFruit;
                if (fruits.TryDequeue(out boughtFruit) && boughtFruit.Item1 == fruitType)
                {
                    string boughtFruitName = boughtFruit.Item2;
                    return (true, boughtFruitName);
                }
                else
                {
                    return (false, null);
                }
            });

            return result;
        }
    }
}