using UstaPlatform.Domain;
using UstaPlatform.Infrastructure;

namespace UstaPlatform.Tests;

public class TestDiscountRule : FiyatKurali
{
    public string RuleName => "Test İndirim Kuralı";

    public decimal CalculatePrice(decimal currentPrice, Talep talep, Usta usta)
    {
        return currentPrice * 0.90m;
    }
}


public class FiyatlamaMotoruTests
{
    // OCP Kanıtı: Dinamik yüklenen kuralların (DLL'lerden) çalıştığını doğrular.
    [Fact]
    public void CalculateFinalPrice_ShouldApplyAllLoadedRulesSequentially()
    {
        // Arrange
        var engine = new FiyatlamaMotoru();
        
        var usta = new Usta { Id = 1, Ad = "Test Usta", Konum = (0, 0) };
        
        DateTime testKayitZamani = new DateTime(2025, 10, 21, 10, 0, 0); // 21 Ekim 2025 Salı, 10:00

        var talep = new Talep { 
            Id = Guid.NewGuid(), 
            KayitZamani = testKayitZamani,
            // Tercih edilen zamanı 2 saat sonrası yapıyoruz (Fark < 3 saat).
            TercihEdilenZaman = testKayitZamani.AddHours(2) 
        };
        decimal basePrice = 1000m;

        // Motorun metot adı kullanılmalı (Sizin kodunuzda SonUcretHesapla).
        decimal finalPrice = engine.SonUcretHesapla(talep, usta, basePrice);

        // Assert (Doğrulama)
        
        // Mantıksal Beklenti: SADECE Acil Çağrı Kuralı (+100 TL) TETİKLENMELİDİR.
        // (Çünkü test Salı gününe sabitlenmiştir, Hafta Sonu Kuralı TETİKLENMEZ.)
        decimal expectedPrice = 1000m + 100m; // 1100 TL

        // Kesin beklenen fiyata eşit olduğunu doğrula
        Assert.Equal(expectedPrice, finalPrice); 
    }
}