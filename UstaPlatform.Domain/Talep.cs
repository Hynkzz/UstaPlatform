namespace UstaPlatform.Domain;

public class Talep
{
    public Guid Id { get; init; } = Guid.NewGuid();
    // init Sadece başlatmada atanabilir
    public DateTime KayitZamani { get; init; } = DateTime.Now; 
    
    // Basit bir Talep Tipi tanımı
    public string TalepTipi { get; set; } // Örn: Sızıntı, Sigorta Atması, Dolap Tamiri vs.
    public string Aciklama { get; set; }
    public Vatandas TalepEden { get; set; }
    public (int X, int Y) Konum { get; set; }
    public DateTime TercihEdilenZaman { get; set; }
}