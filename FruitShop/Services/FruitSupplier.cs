public class FruitSupplier<TFruit> : IFruitSupplier<TFruit> where TFruit : Fruit
{
    private readonly FruitShop fruitShop;

    public FruitSupplier(FruitShop fruitShop)
    {
        this.fruitShop = fruitShop;
    }

    public void SupplyFruit(TFruit fruit)
    {
        Console.WriteLine($"Supplier supplied {typeof(TFruit).Name}.");
        fruitShop.AddFruit(fruit);
    }
}
