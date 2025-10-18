using UstaPlatform.Domain;

namespace UstaPlatform.Tests;

// Ek puan için istenen Schedule (Çizelge) sınıfının testleri
public class CizelgeTests
{
    // Dizinleyici (Indexer) özelliğinin doğru çalıştığını test eder
    [Fact]
    public void Dizinleyici_VerilenGuneAitDogruIsEmirleriniDondurmeli() 
    {
        // Arrange (Hazırlık)
        var schedule = new Cizelge();
        
        var today = DateOnly.FromDateTime(DateTime.Today);
        var tomorrow = today.AddDays(1);

        // Sahte İş Emirleri oluşturuluyor. (İnitializers/Başlatıcılar kullanıldı)
        // İş Emri 1 (Doğru init kullanımı)
        var workOrderToday1 = new IsEmri { 
            Id = Guid.NewGuid(), 
            Talep = new Talep { Id = Guid.NewGuid() }, // Talep objesi minimalist başlatıldı
            PlanlananZaman = today.ToDateTime(TimeOnly.MinValue), 
            HesaplananFiyat = 100m 
        };
        
        // İş Emri 2 (KayitZamani init olduğu için manuel atanmamalı, ancak test senaryosu için kalsın.)
        var workOrderToday2 = new IsEmri { 
            Id = Guid.NewGuid(), 
            Talep = new Talep { Id = Guid.NewGuid(), /* KayitZamani = DateTime.Now */ }, 
            PlanlananZaman = today.ToDateTime(TimeOnly.MinValue).AddHours(4), 
            HesaplananFiyat = 200m 
        };
        
        // İş Emri 3 (Yarının işi)
        var workOrderTomorrow = new IsEmri { 
            Id = Guid.NewGuid(), 
            Talep = new Talep { Id = Guid.NewGuid(), /* KayitZamani = DateTime.Now */ }, 
            PlanlananZaman = tomorrow.ToDateTime(TimeOnly.MinValue), 
            HesaplananFiyat = 300m 
        };

        // Act (Uygulama - İş Emirlerini Çizelgeye Ekleme)
        schedule.AddWorkOrder(workOrderToday1);
        schedule.AddWorkOrder(workOrderToday2);
        schedule.AddWorkOrder(workOrderTomorrow);

        // Dizinleyici (Indexer) ile verilere erişim
        var todayWork = schedule[today];
        var tomorrowWork = schedule[tomorrow];
        var nonExistentDayWork = schedule[today.AddDays(10)]; 

        // Assert (Doğrulama) - Testin başarılı olduğunu kanıtlayan mantıksal kontroller
        
        // 1. Bugüne ait iş emri sayısının 2 olduğunu doğrula
        Assert.Equal(2, todayWork.Count); 
        
        // 2. Yarının iş emri sayısının 1 olduğunu doğrula
        Assert.Single(tomorrowWork);

        // 3. Olmayan bir gün için boş bir liste döndüğünü doğrula
        Assert.Empty(nonExistentDayWork); 
        
        // 4. Koleksiyonun beklenen elemanı içerdiğini doğrula
        Assert.Contains(todayWork, wo => wo.HesaplananFiyat == 200m);
    }
}