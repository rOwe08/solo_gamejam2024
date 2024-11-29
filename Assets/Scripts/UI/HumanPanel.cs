using System.Collections;
using UnityEngine;
using DG.Tweening;

public class HumanPanel : MonoBehaviour
{
    public Vector2 targetPosition;    // Целевая позиция для перемещения
    public Vector2 startPosition;     // Начальная позиция
    public float moveDuration = 0.5f; // Длительность анимации

    public RectTransform rectTransform;
    private Tween moveTween;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }

    // Метод для перемещения HumanPanel на целевую позицию и вызова делегата по завершению
    public void MoveToTarget(System.Action onComplete)
    {
        if (moveTween != null && moveTween.IsActive())
        {
            moveTween.Kill();
        }

        moveTween = rectTransform.DOAnchorPos(targetPosition, moveDuration)
                                 .SetEase(Ease.InOutQuad)
                                 .OnComplete(() => onComplete?.Invoke()); // Вызов делегата после завершения анимации
    }

    // Метод для возврата панели на начальную позицию
    public void MoveToStart()
    {
        if (moveTween != null && moveTween.IsActive())
        {
            moveTween.Kill();
        }

        moveTween = rectTransform.DOAnchorPos(startPosition, moveDuration)
                                 .SetEase(Ease.InOutQuad);
    }
}
