using UnityEngine;
using TMPro;

public class ScrollingText : MonoBehaviour
{
    public TextMeshProUGUI scrollingText; // Ссылка на TextMeshProUGUI
    public float speed = 50f; // Скорость движения текста
    public RectTransform canvasRect; // Ссылка на RectTransform канваса для отслеживания его размеров

    private float textWidth;
    private float startPositionX;

    void Start()
    {
        // Получаем ширину текста и начальную позицию по оси X
        textWidth = scrollingText.rectTransform.rect.width;
        startPositionX = scrollingText.rectTransform.anchoredPosition.x;
    }

    void Update()
    {
        // Двигаем текст влево
        scrollingText.rectTransform.anchoredPosition += Vector2.left * speed * Time.deltaTime;
    }
}
