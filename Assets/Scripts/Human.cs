public class Human
{
    // Физические характеристики
    public float strength;      // Сила
    public float endurance;     // Выносливость
    public float agility;       // Ловкость

    // Интеллектуальные характеристики
    public float logic;         // Логика
    public float creativity;    // Креативность
    public float learnability;  // Обучаемость

    // Ментальные характеристики
    public float emotionalStability;  // Эмоциональная устойчивость
    public float socialSkills;        // Социальные навыки
    public float motivation;          // Мотивация

    public Human()
    {
    }

    // Конструктор для инициализации всех характеристик
    public Human(float strength, float endurance, float agility,
                 float logic, float creativity, float learnability,
                 float emotionalStability, float socialSkills, float motivation)
    {
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
}

public class Woman : Human
{
    public Woman()
        : base(0f, 0f, 0f,  // Физические характеристики для женщины
               0f, 0f, 0f,  // Интеллектуальные характеристики для женщины
               0f, 0f, 0f)  // Ментальные характеристики для женщины
    {
    }
}

public class Man : Human
{
    public Man()
        : base(0f, 0f, 0f,  // Физические характеристики для мужчины
               0f, 0f, 0f,  // Интеллектуальные характеристики для мужчины
               0f, 0f, 0f)  // Ментальные характеристики для мужчины
    {
    }
}
