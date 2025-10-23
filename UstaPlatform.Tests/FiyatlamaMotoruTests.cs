using UstaPlatform.Domain;
using UstaPlatform.Infrastructure;

namespace UstaPlatform.Tests;

public class TestDiscountRule : IFiyatKurali
{
    public string RuleName => "Test İndirim Kuralı";

    public decimal CalculatePrice(decimal currentPrice, Talep talep, Usta usta)
    {
        return currentPrice * 0.90m;
    }
}


public class FiyatlamaMotoruTests
{
    // OCP Kanıtı: Dinamik yüklenen kuralların (DLL'lerden) çalıştığını doğrular
    [Fact]
    public void SonFiyatHesapla_TumYukluKurallariSiraylaUygulamali()
    {
        // Arrange
        var engine = new FiyatlamaMotoru();
        
        var usta = new Usta { Id = 1, Ad = "Test Usta", Konum = (0, 0) };
        
        DateTime testKayitZamani = new DateTime(2025, 10, 24, 12, 0, 0);
        var talep = new Talep { 
            Id = Guid.NewGuid(), 
            KayitZamani = testKayitZamani,
            TercihEdilenZaman = testKayitZamani.AddHours(2) 
        };
        decimal basePrice = 1000m;

        decimal finalPrice = engine.SonUcretHesapla(talep, usta, basePrice);

        // Assert (Doğrulama)
        
        decimal expectedPrice = 1000m + 100m; // 1100 TL

        // Kesin beklenen fiyata eşit olduğunu doğrula
        Assert.Equal(expectedPrice, finalPrice); 
    }
}