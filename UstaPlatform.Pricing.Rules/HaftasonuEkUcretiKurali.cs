using UstaPlatform.Domain;

namespace UstaPlatform.Pricing.Rules;

// Fiyatlama Motoru tarafından dinamik olarak yüklenen somut kural sınıfı
public class HaftasonuEkUcretiKurali : FiyatKurali // FiyatKurali arayüzünü uygular (OCP)
{
    // Kuralın adını tanımlar
    public string RuleName => "Hafta Sonu Ek Ücreti (%15)";

    // Mevcut fiyatı alır ve kuralı uygular
    public decimal CalculatePrice(decimal currentPrice, Talep talep, Usta usta)
    {
        // Talep edilen zamanın haftanın hangi günü olduğunu kontrol et
        var dayOfWeek = talep.TercihEdilenZaman.DayOfWeek;

        // Kural Mantığı: Eğer hafta sonu ise (Cumartesi/Pazar), ek ücret uygulanır
        if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
        {
            // %15 ek ücret ekleme işlemi
            return currentPrice * 1.15m;
        }

        return currentPrice;
    }
}