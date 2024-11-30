using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Era
{
    public string eraName;  // Название эпохи
    public List<EraEvent> events;  // Список событий
    public int survivalThreshold;  // Порог выживания (или условие)
    public Sprite successSprite;  // Спрайт, если эпоха успешна
    public Sprite failureSprite;  // Спрайт, если эпоха провалена

    public Era(string eraName, List<EraEvent> events, int survivalThreshold, Sprite successSprite, Sprite failureSprite)
    {
        this.eraName = eraName;
        this.events = events;
        this.survivalThreshold = survivalThreshold;
        this.successSprite = successSprite;
        this.failureSprite = failureSprite;
    }
}
