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
}
