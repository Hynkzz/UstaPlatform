using UstaPlatform.Domain;
using UstaPlatform.Infrastructure;
using Xunit;
using System;
using System.IO;
using System.Reflection;

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
        
        // Talep, Kayıt Zamanından 2 saat sonra işlensin (Fark 3 saatten az).
        var talep = new Talep { 
            Id = Guid.NewGuid(), 
            // Tercih edilen zamanı 2 saat sonrası yapalır
            TercihEdilenZaman = DateTime.Now.AddHours(2) 
        };
        decimal basePrice = 1000m;

        decimal finalPrice = engine.SonUcretHesapla(talep, usta, basePrice);
        
        // Yüklenen kurallar: 
        // 1. Acil Çağrı Kuralı ( +100 TL) tetiklenir
        // 2. Hafta Sonu Kuralı ( %15 )tetiklenir
        decimal priceAfterAcilCagri = 1000m + 100m; 
        
        decimal expectedPrice = priceAfterAcilCagri * 1.15m;
        // Kesin beklenen fiyata eşit olduğunu doğrula
        Assert.Equal(expectedPrice, finalPrice); 
    }
}