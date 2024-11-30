using TMPro; // Если используешь TextMeshPro
using UnityEngine;
using UnityEngine.UI;

public class StatSliderScript : MonoBehaviour
{
    private Slider slider; // Ссылка на компонент Slider
    private TextMeshProUGUI numberText; // Ссылка на компонент Text для отображения значения
    private int previousValue; // Предыдущее значение слайдера

    void Start()
    {
        // Получаем слайдер на текущем объекте
        slider = GetComponent<Slider>();

        // Ищем компонент Text среди дочерних объектов
        numberText = transform.Find("NumberText").GetComponent<TextMeshProUGUI>();

        // Сохраняем начальное значение слайдера
        previousValue = (int)slider.value;

        // Подписываемся на изменение значения слайдера
        slider.onValueChanged.AddListener(OnSliderValueChanged);

        // Инициализируем текст
        UpdateNumberText(slider.value);
    }

    // Метод для обновления текста при изменении значения слайдера
    void OnSliderValueChanged(float value)
    {
        // Вычисляем доступные очки
        int availablePoints = UIManager.Instance.GetAvailablePoints();

        // Проверяем, не превышает ли новое значение слайдера доступные очки
        if ((int)value > previousValue && availablePoints <= 0)
        {
            // Если очки закончились и пытаемся увеличить значение, отменяем изменение
            slider.value = previousValue;
        }
        else
        {
            // Обновляем значение текста и сохраняем предыдущее значение
            previousValue = (int)slider.value;
            UpdateNumberText(slider.value);
            UIManager.Instance.UpdateAvailablePoints();
        }
    }

    // Обновляем текст при каждом изменении слайдера
    void UpdateNumberText(float value)
    {
        numberText.text = value.ToString("0"); // Преобразуем значение в строку (целое число)
    }
}
