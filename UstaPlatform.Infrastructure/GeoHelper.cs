namespace UstaPlatform.Infrastructure;

// Statik yardımcı sınıf (Sadece bir görevi yerine getirme sorumluluğu - SRP)
public static class GeoHelper
{
    // İki nokta arasındaki mesafeyi (basit Öklid mesafesi) hesaplar.
    public static double MesafeHesapla((int X, int Y) point1, (int X, int Y) point2) 
    {
        int dx = point1.X - point2.X;
        int dy = point1.Y - point2.Y;
        
        // Basit çözüm: Gerçek rota optimizasyonu yerine düz çizgi mesafesi (Öklit metodu) kullanılır
        return Math.Sqrt(dx * dx + dy * dy);
    }
    
    // Mesafeye göre temel fiyatlandırma tahmini yapar (Basit buluşsal yöntem)
    public static decimal TemelFiyatAl(double distance) 
    {
        // Temel fiyatlandırma buluşsal yöntemi
        decimal basePrice = 50m; 
        if (distance > 5)
        {
            basePrice += (decimal)((distance - 5) * 5);
        }
        return basePrice;
    }
}