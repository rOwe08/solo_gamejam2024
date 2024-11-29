using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreatePanel : MonoBehaviour
{
    public TextMeshProUGUI titleText;                  // Текст для заголовка
    public Slider physicalSlider;           // Слайдер для физического состояния
    public Slider intellectualSlider;       // Слайдер для интеллектуального состояния
    public Slider mentalSlider;             // Слайдер для ментального состояния

    public Human currentHuman;              // Текущий человек (жена или муж)

    public Vector2 femaleStartPosition;     // Стартовая позиция для женщины
    public Vector2 maleStartPosition;       // Стартовая позиция для мужчины
    public Vector2 femaleTargetPosition;    // Целевая позиция для женщины
    public Vector2 maleTargetPosition;      // Целевая позиция для мужчины
    public float moveDuration = 0.5f;       // Длительность анимации

    private RectTransform rectTransform;
    private Tween moveTween;
    private Vector2 hiddenPosition;         // Скрытая позиция

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Метод для подготовки CreatePanel с учетом выбранного пола
    public void Prepare(bool isFemale)
    {
        // Создаем экземпляр нужного класса (Woman или Man)
        currentHuman = isFemale ? new Woman() : new Man();

        // Обновляем текст заголовка
        titleText.text = isFemale ? "Create Female" : "Create Male";

        // Устанавливаем значения слайдеров в зависимости от текущего состояния
        physicalSlider.value = currentHuman.physical;
        intellectualSlider.value = currentHuman.intellectual;
        mentalSlider.value = currentHuman.mental;

        // Обновляем скрытую позицию
        Vector2 startPosition = isFemale ? femaleStartPosition : maleStartPosition;
        hiddenPosition = startPosition;

        // Устанавливаем начальную позицию панели перед анимацией
        rectTransform.anchoredPosition = startPosition;
    }

    // Метод для перемещения CreatePanel на целевую позицию
    public void Move(bool isFemale)
    {
        Vector2 targetPosition = isFemale ? femaleTargetPosition : maleTargetPosition;

        if (moveTween != null && moveTween.IsActive())
        {
            moveTween.Kill();
        }

        moveTween = rectTransform.DOAnchorPos(targetPosition, moveDuration)
                                 .SetEase(Ease.InOutQuad);
    }

    // Метод для возврата CreatePanel в скрытую позицию
    public void MoveToHidden()
    {
        if (moveTween != null && moveTween.IsActive())
        {
            moveTween.Kill();
        }

        // Перемещаем панель в скрытую позицию, которая была установлена ранее
        moveTween = rectTransform.DOAnchorPos(hiddenPosition, moveDuration)
                                 .SetEase(Ease.InOutQuad);
    }
}
