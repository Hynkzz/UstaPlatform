namespace UstaPlatform.Domain;

public interface IFiyatKurali
{
    // Kuralın adını döndürür
    string RuleName { get; }
    
    // Mevcut fiyata kuralı uygular ve güncel fiyatı döndürür
    decimal CalculatePrice(decimal currentPrice, Talep talep, Usta usta);
}