public class FruitBuyer<TFruit> where TFruit : Fruit
{
    private readonly FruitShop fruitShop;
    private readonly string buyerName;

    public FruitBuyer(FruitShop fruitShop, string buyerName)
    {
        this.fruitShop = fruitShop;
        this.buyerName = buyerName;
    }

    public void BuyFruit()
    {
        if (fruitShop.TryBuyFruit(out TFruit boughtFruit))
        {
            Console.WriteLine($"{buyerName} bought {boughtFruit.Name}.");
        }
        else
        {
            Console.WriteLine($"{buyerName} couldn't buy a fruit of type {typeof(TFruit).Name}, as there are none available.");
        }
    }
}
