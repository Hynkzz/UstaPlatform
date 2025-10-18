using UstaPlatform.Domain;

namespace UstaPlatform.Pricing.Rules;

// Fiyatlama Motoru tarafından dinamik olarak yüklenen somut kural sınıfı
public class AcilCagriUcretiKurali : FiyatKurali // FiyatKurali arayüzünü uygular (OCP)
{
    // Kuralın adını tanımlar
    public string RuleName => "Acil Çağrı Ücreti (+100 TL)";

    // Mevcut fiyatı alır ve kuralı uygular
    public decimal CalculatePrice(decimal currentPrice, Talep talep, Usta usta)
    {
        // Kayıt zamanı Talep nesnesi oluşturulurken init-only olarak atanmıştı
        var timeDifference = talep.TercihEdilenZaman - talep.KayitZamani;

        // Kural Mantığı: 3 saatten az ise acil çağrıdır
        if (timeDifference.TotalHours < 3 && timeDifference.TotalHours >= 0)
        {
            // Sabit 100 TL ekleme işlemi
            return currentPrice + 100m;
        }

        return currentPrice;
    }
}