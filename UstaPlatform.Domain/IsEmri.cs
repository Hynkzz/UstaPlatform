namespace UstaPlatform.Domain;

public class IsEmri
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Talep Talep { get; init; } // Başlangıçta atanır ve değiştirilemez (Program.cs in içinde)
    public Usta AtananUsta { get; set; }
    public DateTime PlanlananZaman { get; set; }
    public decimal HesaplananFiyat { get; set; }
    public string RotaDurumBilgisi { get; set; } // Basit rota bilgisi
}