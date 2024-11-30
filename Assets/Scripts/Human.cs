public class Human
{
    public string humanName;

    public float strength;
    public float endurance;
    public float agility;

    public float logic;
    public float creativity;
    public float learnability;

    public float emotionalStability;
    public float socialSkills;
    public float motivation;

    public Human()
    {
    }

    public Human(string name, float strength, float endurance, float agility,
                 float logic, float creativity, float learnability,
                 float emotionalStability, float socialSkills, float motivation)
    {
        this.humanName = name;
        this.strength = strength;
        this.endurance = endurance;
        this.agility = agility;

        this.logic = logic;
        this.creativity = creativity;
        this.learnability = learnability;

        this.emotionalStability = emotionalStability;
        this.socialSkills = socialSkills;
        this.motivation = motivation;
    }

    // Метод для получения общего рейтинга (среднего значения всех характеристик)
    public float GetOverallRating()
    {
        float total = strength + endurance + agility
                    + logic + creativity + learnability
                    + emotionalStability + socialSkills + motivation;

        int numOfAttributes = 9;  // Всего 9 характеристик

        return total / numOfAttributes;
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
