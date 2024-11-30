using System.Collections.Generic;

public class EraEvent
{
    public string eventName;  // Название события

    public List<Stat> eventReqs;

    public bool happened;  // Произошло событие или нет

    public EraEvent(string eventName, List<Stat> eventReqs, bool happened = false)
    {
        this.eventName = eventName;
        this.eventReqs = eventReqs;
        this.happened = happened;
    }
}
