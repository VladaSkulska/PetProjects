using FruitShop.Models;

namespace FruitShop.Interfaces
{
    public interface IFruitSupplier
    {
        Task SupplyFruitAsync(FruitType fruitType, string name);
    }
}