public class Graph
{
    private List<City> cities = new List<City>();

    public void AddCity(City city) => cities.Add(city);

    public void AddDistance(City city1, City city2, int distance)
    {
        city1.Distances.Add(city2, distance);
        city2.Distances.Add(city1, distance);
    }

    public List<City> FindShortestPath(City startCity, City endCity)
    {
        var distances = InitializeDistances();
        var previous = new Dictionary<City, City>();
        var unvisitedCities = new List<City>(cities);

        distances[startCity] = 0;

        while (unvisitedCities.Count > 0)
        {
            var currentCity = GetMinimumDistanceCity(unvisitedCities, distances);
            unvisitedCities.Remove(currentCity);

            UpdateDistances(currentCity, distances, previous);
        }

        return ReconstructPath(startCity, endCity, previous);
    }

    private Dictionary<City, int> InitializeDistances()
    {
        return cities.ToDictionary(city => city, city => int.MaxValue);
    }

    private City GetMinimumDistanceCity(List<City> unvisitedCities, Dictionary<City, int> distances)
    {
        City minCity = unvisitedCities.First();
        int minDistance = distances[minCity];

        foreach (var city in unvisitedCities)
        {
            if (distances[city] < minDistance)
            {
                minCity = city;
                minDistance = distances[city];
            }
        }

        return minCity;
    }

    private void UpdateDistances(City currentCity, Dictionary<City, int> distances, Dictionary<City, City> previous)
    {
        foreach (var neighbor in currentCity.Distances)
        {
            int potentialDistance = distances[currentCity] + neighbor.Value;

            if (potentialDistance < distances[neighbor.Key])
            {
                distances[neighbor.Key] = potentialDistance;
                previous[neighbor.Key] = currentCity;
            }
        }
    }

    private List<City> ReconstructPath(City startCity, City endCity, Dictionary<City, City> previous)
    {
        var path = new List<City>();
        var currentCity = endCity;

        while (currentCity != null)
        {
            path.Add(currentCity);

            if (currentCity == startCity)
            {
                break;
            }

            if (previous.TryGetValue(currentCity, out var nextCity))
            {
                currentCity = nextCity;
            }
            else
            {
                currentCity = null;
            }
        }

        path.Reverse();
        return path;
    }
}

