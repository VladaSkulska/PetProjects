public class City
{
    public string Name { get; set; }
    public Dictionary<City, int> Distances { get; } = new Dictionary<City, int>();

    public City(string name)
    {
        Name = name;
    }
}

