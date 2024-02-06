using FruitShop.Interfaces;
using FruitShop.Models;
using FruitShop.Utilities;

namespace FruitShop.Services
{
    public class FruitSupplier : IFruitSupplier
    {
        private readonly FruitStore fruitStore;

        public FruitSupplier(FruitStore fruitStore)
        {
            this.fruitStore = fruitStore;
        }

        public async Task SupplyFruitAsync(FruitType fruitType, string name)
        {
            await Task.Delay(1000);
            await fruitStore.AddFruitAsync(fruitType, name);

            ConsoleLogger.LogSupplierSupply(fruitType);
        }
    }
}