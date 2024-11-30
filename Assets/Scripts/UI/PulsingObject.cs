using UnityEngine;
using DG.Tweening;  // Для анимации через DOTween

public class PulsingObject : MonoBehaviour
{
    public float pulseScale = 1.2f;  // Масштаб при пульсации
    public float pulseDuration = 0.3f;  // Время для одного цикла увеличения/уменьшения
    public bool isActive = false;  // Флаг активности объекта (будет ли он пульсировать)

    private Vector3 initialScale;  // Исходный размер объекта
    private Tween pulseTween;  // Переменная для хранения анимации

    void Start()
    {
        // Сохраняем исходный размер объекта
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Если объект активен и анимация ещё не запущена
        if (isActive && (pulseTween == null || !pulseTween.IsActive()))
        {
            Pulse();  // Запускаем пульсацию
        }
        else if (!isActive && pulseTween != null && pulseTween.IsActive())
        {
            pulseTween.Kill();  // Останавливаем пульсацию, если объект не активен
            transform.localScale = initialScale;  // Возвращаем исходный размер
        }
    }

    void Pulse()
    {
        // Плавное увеличение и уменьшение
        pulseTween = transform.DOScale(initialScale * pulseScale, pulseDuration)
            .SetLoops(-1, LoopType.Yoyo)  // Бесконечное пульсирование (обратно и снова)
            .SetEase(Ease.InOutSine);  // Используем плавную кривую для анимации
    }
}
