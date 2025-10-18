using System.Reflection; // Dinamik DLL yükleme (Reflection) için gereklidşr
using UstaPlatform.Domain; 

namespace UstaPlatform.Infrastructure; // Altyapı katmanında yer alır (SRP)

// Plug-in mimarisini yöneten çekirdek sınıftır
public class FiyatlamaMotoru
{
    private readonly List<FiyatKurali> _rules = new(); // Yüklenen kural örneklerini tutar
    
    // Eklenti klasörünün yolunu dinamik olarak belirler
    private readonly string PluginDirectory = 
        Path.Combine(AppContext.BaseDirectory, "plugins"); 

    public FiyatlamaMotoru()
    {
        // Motor oluşturulurken kuralları otomatik yükleyen metot çağrılır
        KuralYukle();
    }
    
    // Kuralları DLL'lerden dinamik olarak yükler (OCP'nin anahtarı)
    private void KuralYukle()
    {
        _rules.Clear();
        
        if (!Directory.Exists(PluginDirectory))
        {
            Directory.CreateDirectory(PluginDirectory);
        }

        var pluginFiles = Directory.GetFiles(PluginDirectory, "*.dll");

        foreach (var file in pluginFiles)
        {
            try
            {
                var assembly = Assembly.LoadFrom(file); // DLL dosyasını yükle
                
                // Reflection: FiyatKurali arayüzünü uygulayan somut sınıfları bulur
                var ruleTypes = assembly.GetTypes()
                    .Where(t => typeof(FiyatKurali).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                    .ToList(); 

                foreach (var type in ruleTypes)
                {
                    // Reflection: Kuralın bir örneğini oluşturur
                    var ruleInstance = Activator.CreateInstance(type) as FiyatKurali;

                    if (ruleInstance != null)
                    {
                        _rules.Add(ruleInstance); // Başarıyla yüklenen kuralı listeye ekler
                    }
                }
            }
            catch (Exception)
            {
                // Kural yüklenemezse sessizce geçer (Üretim ortamı davranışı).
            }
        }
    }
    
    // Nihai fiyatı tüm kuralları ardışık uygulayarak hesaplar
    public decimal SonUcretHesapla(Talep talep, Usta usta, decimal basePrice)
    {
        decimal currentPrice = basePrice;
        // Temel Ücret çıktısı
        Console.WriteLine($"--- Fiyat Hesaplama Başlatıldı --- (Temel Ücret: {ParaFormatlayici.Formatla(currentPrice)})");

        foreach (var rule in _rules)
        {
            // Fiyatlandırma kurallarını kompozisyon olarak uygular
            currentPrice = rule.CalculatePrice(currentPrice, talep, usta);
        }

        // Nihai Fiyat çıktısı
        Console.WriteLine($"--- Nihai Fiyat: {ParaFormatlayici.Formatla(currentPrice)} ---\n");
        return currentPrice;
    }
}