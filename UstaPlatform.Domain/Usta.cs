namespace UstaPlatform.Domain;

public class Usta
{
    // init Sadece nesne başlatılırken atanabilir. Sonradan değiştirilemez.
    public int Id { get; init; } 
    public string Ad { get; set; }
    public string UzmanlikAlani { get; set; } // Tesisatçı, Elektrikçi
    public int Puan { get; set; } 
    public (int X, int Y) Konum { get; set; } // Koordinatı
    public int GuncelIsYukü { get; set; } 
}