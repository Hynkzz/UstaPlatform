
# 🧰 UstaPlatform 🧰

UstaPlatform projesi, Arcadia şehrindeki kayıp uzmanları (Tesisatçı, Elektrikçi, vb.) vatandaş talepleriyle eşleştiren, dinamik fiyatlama ve akıllı rota planlama yapabilen, Genişletilebilir ve Açık Uçlu bir yazılım platformudur.

# 🚀 Kurulum ve Çalıştırma adımları

## dotnet CLI ile çalıştırma

Projenin bulunduğu ana dizine geç:

```bash
 cd UstaPlatform/UstaPlatform.App
```
Projeyi çalıştır:

```bash
 dotnet run
```

## Visual Studio veya Jetbrains Rider ile çalıştırma

```bash
UstaPlatform.sln dosyasını aç,

Başlangıç projesi olarak UstaPlatform.App’i seç,

Run tuşuna bas ve projeyi çalıştır.
```


# 🧩 Proje Yapısı ve Açıklamaları


## UstaPlatform


- `UstaPlatform.App`                         --- Giriş noktası (Console App)  

  - `Program.cs`  
---
- `UstaPlatform.Domain`                     --- Temel sınıflar ve domain modelleri  

  - `Cizelge.cs`   -
  - `FiyatKurali.cs`   
  - `IsEmri.cs`   
  - `Rota.cs`   
  - `Talep.cs`   
  - `Usta.cs`   
  - `Vatandas.cs`   
---
- `UstaPlatform.Infrastructure`             --- Altyapı ve yardımcı metotlar  

  - `Dogrulama.cs`   
  - `FiyatlamaMotoru.cs`   
  - `GeoHelper.cs` 
  - `ParaFormatlayici.cs` 
---
- `UstaPlatform.Pricing.Rules`              --- Fiyatlandırma kurallarının metotları 

  - `AcilCagriUcretiKurali.cs`   
  - `HaftaSonuEkUcretiKurali.cs`   

---

- `UstaPlatform.Pricing.Rules`              --- Unit test projeleri    

  - `CizelgeTests.cs`   
  - `FiyatlamaMotoruTests.cs`   

---  

# 🧪 Gereksinimler


.NET SDK 8.0+
