public class Human
{
    public float physical;   // ���������� ���������
    public float intellectual; // ���������������� ���������
    public float mental;     // ���������� ���������

    public Human(float physical, float intellectual, float mental)
    {
        this.physical = physical;
        this.intellectual = intellectual;
        this.mental = mental;
    }
}

public class Woman : Human
{
    public Woman() : base(70f, 80f, 75f)  // ��������� ��������� ��������
    {
    }
}

public class Man : Human
{
    public Man() : base(80f, 75f, 70f)  // ��������� ��������� ��������
    {
    }
}
