public interface IFruitSupplier<TFruit> where TFruit : Fruit
{
    void SupplyFruit(TFruit fruit);
}
