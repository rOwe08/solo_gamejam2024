
[System.Serializable]
public class Stat
{
    public string Name { get; set; }
    public string Abbreviation { get; set; } // ����� ���� ��� ������������
    public float Value { get; set; }

    public Stat(string name, string abbreviation, float value)
    {
        Name = name;
        Abbreviation = abbreviation;
        Value = value;
    }
}
