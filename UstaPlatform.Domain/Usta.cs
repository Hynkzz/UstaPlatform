namespace UstaPlatform.Domain;

public class Usta
{
    public int Id { get; init; } 
    public string Ad { get; set; }
    public string UzmanlikAlani { get; set; } 
    public int Puan { get; set; } 
    public (int X, int Y) Konum { get; set; }
    public int GuncelIsYuku { get; set; } 
}