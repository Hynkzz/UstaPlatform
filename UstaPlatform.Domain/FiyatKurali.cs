namespace UstaPlatform.Domain;

public interface FiyatKurali
{
    // Kuralın adını döndürür (Geliştirici takibi için).
    string RuleName { get; }
    
    // Mevcut fiyata kuralı uygular ve güncel fiyatı döndürür
    decimal CalculatePrice(decimal currentPrice, Talep talep, Usta usta);
}