using System.Collections.Concurrent;
public class FruitShop
{
    private ConcurrentQueue<Fruit> fruits = new();

    public void AddFruit(Fruit fruit)
    {
        Console.WriteLine($"Fruit '{fruit.Name}' has arrived.");
        fruits.Enqueue(fruit);
    }

    public bool TryBuyFruit<TFruit>(out TFruit boughtFruit) where TFruit : Fruit
    {
        if (fruits.TryDequeue(out Fruit baseFruit) && baseFruit is TFruit)
        {
            boughtFruit = (TFruit)baseFruit;
            Console.WriteLine($"Successfully bought {boughtFruit.Name}.");
            return true;
        }
        else
        {
            boughtFruit = null;
            Console.WriteLine($"Couldn't buy a fruit of type {typeof(TFruit).Name}, as there are none available.");
            return false;
        }
    }
}
