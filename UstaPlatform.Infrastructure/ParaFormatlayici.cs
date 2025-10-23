using System.Globalization;

namespace UstaPlatform.Infrastructure;

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