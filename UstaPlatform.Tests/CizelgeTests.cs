using UstaPlatform.Domain;

namespace UstaPlatform.Tests;

public class CizelgeTests
{
    // Dizinleyici (Indexer) özelliğinin doğru çalıştığını test eder
    [Fact]
    public void Dizinleyici_GuneAitDogruIsleriDondurur() 
    {
        var schedule = new Cizelge();
        
        var today = DateOnly.FromDateTime(DateTime.Today);
        var tomorrow = today.AddDays(1);

        // İş Emri 1 (Doğru init kullanımı)
        var workOrderToday1 = new IsEmri { 
            Id = Guid.NewGuid(), 
            Talep = new Talep { Id = Guid.NewGuid() }, // Talep objesi minimalist başlatıldı
            PlanlananZaman = today.ToDateTime(TimeOnly.MinValue), 
            HesaplananFiyat = 100m 
        };
        
        // İş Emri 2
        var workOrderToday2 = new IsEmri { 
            Id = Guid.NewGuid(), 
            Talep = new Talep { Id = Guid.NewGuid(), /* KayitZamani = DateTime.Now */ }, 
            PlanlananZaman = today.ToDateTime(TimeOnly.MinValue).AddHours(4), 
            HesaplananFiyat = 200m 
        };
        
        // İş Emri 3
        var workOrderTomorrow = new IsEmri { 
            Id = Guid.NewGuid(), 
            Talep = new Talep { Id = Guid.NewGuid(), /* KayitZamani = DateTime.Now */ }, 
            PlanlananZaman = tomorrow.ToDateTime(TimeOnly.MinValue), 
            HesaplananFiyat = 300m 
        };

        schedule.AddWorkOrder(workOrderToday1);
        schedule.AddWorkOrder(workOrderToday2);
        schedule.AddWorkOrder(workOrderTomorrow);

        var todayWork = schedule[today];
        var tomorrowWork = schedule[tomorrow];
        var nonExistentDayWork = schedule[today.AddDays(10)]; 

        
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