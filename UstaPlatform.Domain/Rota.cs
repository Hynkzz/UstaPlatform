using System.Collections;
namespace UstaPlatform.Domain;

// Route sınıfı, durakları (X, Y koordinatları) tutan ve gezilebilir özel koleksiyondur.
public class Rota : IEnumerable<(int X, int Y)>
{
    private readonly List<(int X, int Y)> _stops = new List<(int X, int Y)>();

    // !!! DEĞİŞİKLİK BURADA !!!
    // Artık koleksiyon başlatıcılardan gelen tek argümanı (tuple'ı) kabul ediyor.
    public void Add((int X, int Y) coordinate) 
    {
        _stops.Add(coordinate);
    }

    // Gerekli (int X, int Y) metodu istenirse eski yapıyı korumak için overload olarak tutulabilir:
    // public void Add(int X, int Y) { _stops.Add((X, Y)); }
    // Ancak hata çözümünde bu tek argümanlı Add metodu ana çözümdür.

    public IEnumerator<(int X, int Y)> GetEnumerator()
    {
        return _stops.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}