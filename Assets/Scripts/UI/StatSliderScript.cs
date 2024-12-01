using TMPro; // Если используешь TextMeshPro
using UnityEngine;
using UnityEngine.UI;

public class StatSliderScript : MonoBehaviour
{
    private Slider slider; // Ссылка на компонент Slider
    private TextMeshProUGUI numberText; // Ссылка на компонент Text для отображения значения
    private int previousValue; // Предыдущее значение слайдера

    public AudioSource audioSource; // Ссылка на AudioSource
    public AudioClip sliderSound; // Звук изменения слайдера

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

        // Инициализируем аудиосурс, если не привязан в инспекторе
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void OnSliderValueChanged(float value)
    {
        // Вычисляем доступные очки
        int availablePoints = UIManager.Instance.GetAvailablePoints();

        if ((int)value > previousValue && availablePoints <= 0)
        {
            slider.value = previousValue;
        }
        else
        {
            previousValue = (int)slider.value;
            UpdateNumberText(slider.value);
            UIManager.Instance.UpdateAvailablePoints();

            // Проигрывание звука, если значение больше 0
            if (value > 0)
            {
                audioSource.Play();
            }
        }
    }


    // Обновляем текст при каждом изменении слайдера
    void UpdateNumberText(float value)
    {
        numberText.text = value.ToString("0"); // Преобразуем значение в строку (целое число)
    }
}
