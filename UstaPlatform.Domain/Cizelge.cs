namespace UstaPlatform.Domain;

public class Cizelge
{
    // İş Emirlerini tutacak olan dahili sözlük
    private readonly Dictionary<DateOnly, List<IsEmri>> _dailyWork = new();

    public List<IsEmri> this[DateOnly day]
    {
        get
        {
            // Eğer o gün için bir liste yoksa, boş bir liste döndürür
            if (!_dailyWork.ContainsKey(day))
            {
                return new List<IsEmri>();
            }
            return _dailyWork[day];
        }
        set
        {
            // O güne ait iş emri listesini ayarlar
            _dailyWork[day] = value;
        }
    }

    // Takvime yeni bir İş Emri eklemek için yardımcı metot
    public void AddWorkOrder(IsEmri workOrder)
    {
        // Planlanan zamanı DateOnly olarak alır
        var day = DateOnly.FromDateTime(workOrder.PlanlananZaman);
        
        // Eğer sözlükte o gün yoksa, yeni bir liste oluştur ve ekler
        if (!_dailyWork.ContainsKey(day))
        {
            _dailyWork[day] = new List<IsEmri>();
        }

        _dailyWork[day].Add(workOrder);
    }
}