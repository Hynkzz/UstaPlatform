using UstaPlatform.Domain;
using UstaPlatform.Infrastructure; 
using DateOnly = System.DateOnly;


namespace UstaPlatform;

public class Program
{
    // Fiyatlama Motoru , uygulamanın temel bağımlılığıdır (Bağımlılıkların Tersine Çevrilmesi Prensibi)
    private static readonly FiyatlamaMotoru FiyatlamaMotoru = new();  // Bir kez değer atandıktan sonra tekrar değiştirilememesi icin private readonly olarak tanimlanir
    
    // Veri Deposu simülasyonu
    private static List<Usta> _ustalar = new List<Usta>
    {
        // Objeler tanımlandı
        new Usta { Id = 1, Ad = "Hasan Tesisat", UzmanlikAlani = "Tesisatçı", Puan = 5, Konum = (10, 20), GuncelIsYuku = 2 }, // Id init-only
        new Usta { Id = 2, Ad = "Hüseyin Elektrik ve Elektronik", UzmanlikAlani = "Elektrikçi", Puan = 4, Konum = (50, 60), GuncelIsYuku = 1 },
        new Usta { Id = 3, Ad = "Mehmet Marangoz", UzmanlikAlani = "Marangoz", Puan = 4, Konum = (30, 40), GuncelIsYuku = 0 }
    };

    private static Vatandas _veli = new Vatandas
    {
        Id = 101, //  Sadece başlangıçta değer atanır (init)
        Ad = "Hasan ÖKSÜZ",
        Adres = "Arcadia Merkez Mahallesi 100.cadde",
        Konum = (15, 25)
    };

    public static void Main(string[] args)
    {
        Console.WriteLine("=============================================");
        Console.WriteLine(" UstaPlatform: Şehir Talep Platformu");
        Console.WriteLine("=============================================\n");

        SimulateWorkflow();
    }
    
    private static void SimulateWorkflow() // Ana iş akışı metodu tanımlanır
    {
        // Vatandaş talep açar
        var talep = new Talep
        {
            // KayitZamani init-only ile otomatik olarak atanır
            TalepTipi = "Sızıntı ve kaçak tamiri",
            TalepEden = _veli,
            Konum = _veli.Konum,
            // Kural Tetikleyici: Hafta Sonu ve Acil çağrı için zaman ayarı
            TercihEdilenZaman = DateTime.Now.Date.AddDays(GetNextSundayOffset()).AddHours(DateTime.Now.Hour + 2) 
        };
        Console.WriteLine($"[1] Talep Oluşturuldu: {talep.TalepTipi}");
        Console.WriteLine($" -> Konum: {talep.Konum}, Tercih Edilen Zaman: {talep.TercihEdilenZaman:dd.MM.yyyy HH:mm}\n");
        



        // 2. UYGULAMA USTA EŞLEŞTİRİR (Basit Buluşsal Yöntem)
        var eslesenUsta = MatchMaster(talep); 

        if (eslesenUsta == null)
        {
            Console.WriteLine("[2] HATA: Uygun usta bulunamadı.");
            return;
        }

        Console.WriteLine($"[2] Usta Eşleştirildi: {eslesenUsta.Ad} ({eslesenUsta.UzmanlikAlani})\n");
        
        // 3. FİYAT HESAPLANIR
        // Statik Yardımcı Sınıf (GeoHelper) kullanılır
        Console.WriteLine("[3] FİYAT HESAPLAMA VE KURAL UYGULAMA\n"); 
        double distance = GeoHelper.MesafeHesapla(talep.Konum, eslesenUsta.Konum);
        decimal basePrice = GeoHelper.TemelFiyatAl(distance);
        
        // Dinamik fiyatlama: Motor, DLL'lerden yüklenen tüm kuralları uygular (OCP).
        decimal finalPrice = FiyatlamaMotoru.SonUcretHesapla(talep, eslesenUsta, basePrice);

        // 4. İŞ EMRİ OLUŞTURULUR
        var isEmri = new IsEmri
        {
            // Talep ve Id init-only
            Talep = talep,
            AtananUsta = eslesenUsta,
            PlanlananZaman = talep.TercihEdilenZaman,
            HesaplananFiyat = finalPrice
        };
        eslesenUsta.GuncelIsYuku++; 
        Console.WriteLine($"[4] İş Emri Oluşturuldu: ID: {isEmri.Id}");

        Console.WriteLine($" -> Nihai Fiyat: {ParaFormatlayici.Formatla(isEmri.HesaplananFiyat)}"); 
        
        // 5. İŞ EMRİ, UZMANIN ÇİZELGESİNE/ROTASINA YERLEŞTİRİLİR 
        
        // Rota (IEnumerable) - Özel koleksiyon başlatıcıları kullanıldı
        var ustaRotasi = new Rota
        {
            (eslesenUsta.Konum.X, eslesenUsta.Konum.Y), 
            (talep.Konum.X, talep.Konum.Y)              
        };
        
        Console.WriteLine("\n[5a] Usta Rotası (IEnumerable) Kontrolü:");
        // Rota sınıfının IEnumerable arayüzü sayesinde foreach kullanılır
        foreach (var durak in ustaRotasi)
        {
            Console.WriteLine($"    -> Durak: ({durak.X}, {durak.Y})");
        }
        
        // Çizelge (Indexer) - Çizelge sınıfı kullanılır
        var ustaCizelgesi = new Cizelge();
        ustaCizelgesi.AddWorkOrder(isEmri);
        
        // Dizinleyici (Indexer) ile iş emrine erişim
        var isGunu = DateOnly.FromDateTime(isEmri.PlanlananZaman);
        var oGununIsleri = ustaCizelgesi[isGunu]; 
        
        Console.WriteLine("\n[5b] Usta Çizelgesi (Indexer) Kontrolü:");
        Console.WriteLine($"    -> {isGunu} tarihindeki iş sayısı: {oGununIsleri.Count}");
        Console.WriteLine($"    -> İlk İş Emri Fiyatı (Indexer ile çekildi): {ParaFormatlayici.Formatla(oGununIsleri.First().HesaplananFiyat)}");    }
    
    private static Usta? MatchMaster(Talep talep)
    {
        return _ustalar
            .Where(u => u.UzmanlikAlani == "Tesisatçı") 
            .OrderBy(u => u.GuncelIsYuku) 
            .FirstOrDefault();
    }
    
    // Test için yardımcı metot
    private static int GetNextSundayOffset()
    {
        int day = (int)DateTime.Now.DayOfWeek; 
        
        if (day == 0) return 7; 
        return 7 - day;
    }
}