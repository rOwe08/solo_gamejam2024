using System.Collections.Generic;

public class Human
{
    public string humanName;

    // Список характеристик
    public List<Stat> Stats { get; private set; }

    public Human()
    {
        Stats = new List<Stat>();
    }

    public Human(string name, float strength, float endurance, float agility,
                 float logic, float creativity, float learnability,
                 float emotionalStability, float socialSkills, float motivation)
    {
        humanName = name;
        Stats = new List<Stat>
        {
            new Stat("Strength", "STR", strength),                  // Сила
            new Stat("Endurance", "END", endurance),                // Выносливость
            new Stat("Agility", "AGI", agility),                    // Ловкость
            new Stat("Logic", "LOG", logic),                        // Логика
            new Stat("Creativity", "CRE", creativity),              // Креативность
            new Stat("Learnability", "LRN", learnability),          // Обучаемость
            new Stat("Emotional Stability", "EMO", emotionalStability), // Эмоциональная стабильность
            new Stat("Social Skills", "SOC", socialSkills),         // Социальные навыки
            new Stat("Motivation", "MOT", motivation)               // Мотивация
        };

    }

    // Метод для получения общего рейтинга
    public float GetOverallRating()
    {
        float total = 0;
        foreach (Stat stat in Stats)
        {
            total += stat.Value;
        }

        return total / Stats.Count;
    }
}



public class Woman : Human
{
    public Woman()
        : base("Eva", 0f, 0f, 0f,
               0f, 0f, 0f,
               0f, 0f, 0f) 
    {
    }
}

public class Man : Human
{
    public Man()
        : base("Adam", 0f, 0f, 0f, 
               0f, 0f, 0f, 
               0f, 0f, 0f) 
    {
    }
}
