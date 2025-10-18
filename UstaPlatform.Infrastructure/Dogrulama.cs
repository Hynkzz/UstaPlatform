using System.Runtime.CompilerServices; // CallerArgumentExpression için gerekli

namespace UstaPlatform.Infrastructure; // Altyapı katmanında yer alır

// Statik yardımcı sınıf: Parametre doğrulama sorumluluğu (SRP)
public static class Dogrulama 
{
    // Parametrenin null olup olmadığını kontrol eder
    // Tipi class olan referans tipler için kullanılır.
    public static T NullDegilDogrula<T>( 
        T? deger, 
        // CallerArgumentExpression: Parametre adını otomatik yakalamak için C# özelliği
        [CallerArgumentExpression(nameof(deger))] string? parametreAdi = null) where T : class
    {
        if (deger is null)
        {
            throw new ArgumentNullException(parametreAdi ?? "Bilinmeyen parametre", $"{parametreAdi} null olamaz.");
        }
        return deger;
    }
    
    // Değerin pozitif olup olmadığını kontrol eder
    public static decimal PozitifDogrula(
        decimal deger, 
        [CallerArgumentExpression(nameof(deger))] string? parametreAdi = null)
    {
        if (deger <= 0)
        {
            throw new ArgumentOutOfRangeException(parametreAdi, deger, $"Değer ({parametreAdi}) sıfırdan büyük olmalıdır.");
        }
        return deger;
    }

    // String'in boş veya null olmadığını kontrol eden metot
    public static string StringDoluDogrula(
        string? deger,
        [CallerArgumentExpression(nameof(deger))] string? parametreAdi = null)
    {
        if (string.IsNullOrWhiteSpace(deger))
        {
            throw new ArgumentException($"Değer ({parametreAdi}) boş veya sadece boşluk karakterlerinden oluşamaz.", parametreAdi);
        }
        return deger;
    }
}