using System.Globalization;

namespace UstaPlatform.Infrastructure;

// Tek Sorumluluk Prensibine (SRP) uygun statik yardımcı sınıf
public static class ParaFormatlayici 
{
    // Türkçe kültür ayarını (tr-TR) sabitlendi
    private static readonly CultureInfo TrCulture = new CultureInfo("tr-TR");

    // Decimal değeri TL para birimi formatında string olarak döndürür
    public static string Formatla(decimal miktar)
    {
        return miktar.ToString("C", TrCulture);
    }
}