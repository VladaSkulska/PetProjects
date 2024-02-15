using FruitShop.Models;
using FruitShop.Utilities;
using System.Collections.Concurrent;

namespace FruitShop.Services
{
    public class FruitStore
    {
        private readonly ConcurrentDictionary<FruitType, ConcurrentQueue<string>> fruitsByType = new();

        public async Task AddFruitAsync(FruitType fruitType, string name)
        {
            await Task.Run(() =>
            {
                var fruitQueue = fruitsByType.GetOrAdd(fruitType, _ => new ConcurrentQueue<string>());
                fruitQueue.Enqueue(name);
            });

            ConsoleLogger.LogFruitArrival(name);
        }

        public async Task<(bool isSuccess, string fruit)> TryBuyFruitAsync(FruitType fruitType)
        {
            (bool isSuccess, string fruit) result = await Task.Run(() =>
            {
                if(fruitsByType.TryGetValue(fruitType, out var fruitQueue) && fruitQueue.TryDequeue(out string boughtFruitName))
                {
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