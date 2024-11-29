public class Human
{
    public float physical;   // Физическое состояние
    public float intellectual; // Интеллектуальное состояние
    public float mental;     // Ментальное состояние

    public Human(float physical, float intellectual, float mental)
    {
        this.physical = physical;
        this.intellectual = intellectual;
        this.mental = mental;
    }
}

public class Woman : Human
{
    public Woman() : base(70f, 80f, 75f)  // Примерные стартовые значения
    {
    }
}

public class Man : Human
{
    public Man() : base(80f, 75f, 70f)  // Примерные стартовые значения
    {
    }
}
