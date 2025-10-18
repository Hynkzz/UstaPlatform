namespace UstaPlatform.Domain;

public class Vatandas
{
    public int Id { get; init; }
    public string Ad { get; set; }
    public string Adres { get; set; }
    public (int X, int Y) Konum { get; set; }
}