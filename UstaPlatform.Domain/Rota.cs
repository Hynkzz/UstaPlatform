using System.Collections;
namespace UstaPlatform.Domain;

public class Rota : IEnumerable<(int X, int Y)>
{
    private readonly List<(int X, int Y)> _stops = new List<(int X, int Y)>();

    public void Add((int X, int Y) coordinate) 
    {
        _stops.Add(coordinate);
    }
    

    public IEnumerator<(int X, int Y)> GetEnumerator()
    {
        return _stops.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}