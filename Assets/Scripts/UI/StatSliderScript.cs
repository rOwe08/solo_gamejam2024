using TMPro; // Если используешь TextMeshPro
using UnityEngine;
using UnityEngine.UI;

public class StatSliderScript : MonoBehaviour
{
    private Slider slider; // Ссылка на компонент Slider
    private TextMeshProUGUI numberText; // Ссылка на компонент Text для отображения значения

    void Start()
    {
        // Получаем слайдер на текущем объекте
        slider = GetComponent<Slider>();

        // Ищем компонент TextMeshProUGUI среди дочерних объектов
        numberText = transform.Find("NumberText").GetComponent<TextMeshProUGUI>();

        // Подписываемся на изменение значения слайдера
        slider.onValueChanged.AddListener(UpdateNumberText);

        // Инициализируем текст
        UpdateNumberText(slider.value);
    }

    // Метод для обновления текста при изменении значения слайдера
    void UpdateNumberText(float value)
    {
        numberText.text = value.ToString("0"); // Преобразуем значение в строку (целое число)
    }
}
