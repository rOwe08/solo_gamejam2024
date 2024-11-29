using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CreatePanel : MonoBehaviour
{
    public Vector2 femaleStartPosition;    // Стартовая позиция для женщины
    public Vector2 maleStartPosition;      // Стартовая позиция для мужчины
    public Vector2 femaleTargetPosition;   // Целевая позиция для женщины
    public Vector2 maleTargetPosition;     // Целевая позиция для мужчины
    public float moveDuration = 0.5f;      // Длительность анимации

    private RectTransform rectTransform;
    private Tween moveTween;
    private Vector2 hiddenPosition;        // Скрытая позиция (будет обновляться перед анимацией)

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Метод для подготовки панели перед анимацией и выбора стартовой и целевой позиции
    public void PrepareAndMove(bool isFemale)
    {
        // Выбираем стартовую и целевую позицию в зависимости от пола
        Vector2 startPosition = isFemale ? femaleStartPosition : maleStartPosition;
        Vector2 targetPosition = isFemale ? femaleTargetPosition : maleTargetPosition;

        // Обновляем скрытую позицию как текущую стартовую позицию
        hiddenPosition = startPosition;

        StartCoroutine(PrepareCoroutine(startPosition, targetPosition));
    }

    private IEnumerator PrepareCoroutine(Vector2 startPosition, Vector2 targetPosition)
    {
        // Подготовка перед анимацией (например, пауза на 0.5 секунды)
        yield return new WaitForSeconds(0.5f);

        // Устанавливаем стартовую позицию
        rectTransform.anchoredPosition = startPosition;

        // Запуск анимации перемещения панели на целевую позицию
        MoveToTarget(targetPosition);
    }

    // Метод для перемещения CreatePanel на целевую позицию
    public void MoveToTarget(Vector2 targetPosition)
    {
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
