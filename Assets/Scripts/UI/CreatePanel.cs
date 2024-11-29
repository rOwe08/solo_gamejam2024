using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreatePanel : MonoBehaviour
{
    public TextMeshProUGUI titleText;        // Текст для заголовка
    public Button confirmButton;            // Кнопка подтверждения
    public Slider[] sliders;                // Массив слайдеров

    public Human currentHuman;              // Текущий человек (жена или муж)

    public Vector2 femaleStartPosition;     // Стартовая позиция для женщины
    public Vector2 maleStartPosition;       // Стартовая позиция для мужчины
    public Vector2 femaleTargetPosition;    // Целевая позиция для женщины
    public Vector2 maleTargetPosition;      // Целевая позиция для мужчины
    public float moveDuration = 0.5f;       // Длительность анимации

    private RectTransform rectTransform;
    private Tween moveTween;
    private Vector2 hiddenPosition;         // Скрытая позиция

    private Stage currentStage;  // Текущий этап
    private float[] sliderValues; // Значения слайдеров для каждого этапа

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        currentHuman = new Human();
        sliderValues = new float[sliders.Length];

        // Настройка начальных значений
        confirmButton.onClick.AddListener(OnConfirmClicked);
        ShowStage(Stage.Physical);
    }

    // Метод для отображения нужного этапа
    void ShowStage(Stage stage)
    {
        currentStage = stage;

        // Скрываем все слайдеры
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].gameObject.SetActive(false);
        }

        switch (stage)
        {
            case Stage.Physical:
                titleText.text = "Select Physical Attributes";
                sliders[0].gameObject.SetActive(true); // Сила
                sliders[1].gameObject.SetActive(true); // Выносливость
                sliders[2].gameObject.SetActive(true); // Ловкость
                break;

            case Stage.Intellectual:
                titleText.text = "Select Intellectual Attributes";
                sliders[3].gameObject.SetActive(true); // Логика
                sliders[4].gameObject.SetActive(true); // Креативность
                sliders[5].gameObject.SetActive(true); // Обучаемость
                break;

            case Stage.Mental:
                titleText.text = "Select Mental Attributes";
                sliders[6].gameObject.SetActive(true); // Эмоциональная устойчивость
                sliders[7].gameObject.SetActive(true); // Социальные навыки
                sliders[8].gameObject.SetActive(true); // Мотивация
                break;
        }
    }

    // Метод для подготовки CreatePanel с учетом выбранного пола
    public void Prepare(bool isFemale)
    {
        // Создаем экземпляр нужного класса (Woman или Man)
        currentHuman = isFemale ? new Woman() : new Man();

        // Обновляем текст заголовка
        titleText.text = isFemale ? "Create Female" : "Create Male";

        switch (currentStage)
        {
            case Stage.Physical:
                sliders[0].value = currentHuman.strength;
                sliders[1].value = currentHuman.endurance;
                sliders[2].value = currentHuman.agility;
                break;

            case Stage.Intellectual:
                sliders[0].value = currentHuman.logic;
                sliders[1].value = currentHuman.creativity;
                sliders[2].value = currentHuman.learnability;
                break;

            case Stage.Mental:
                sliders[0].value = currentHuman.emotionalStability;
                sliders[1].value = currentHuman.socialSkills;
                sliders[2].value = currentHuman.motivation;
                break;
        }
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

    // Обработчик нажатия на кнопку "Confirm"
    void OnConfirmClicked()
    {
        // Сохраняем значения слайдеров в зависимости от текущего этапа
        for (int i = 0; i < sliders.Length; i++)
        {
            if (sliders[i].gameObject.activeSelf) // Сохраняем только активные слайдеры
            {
                sliderValues[i] = sliders[i].value;
            }
        }

        // Применяем значения в зависимости от этапа
        switch (currentStage)
        {
            case Stage.Physical:
                currentHuman.strength = sliderValues[0];
                currentHuman.endurance = sliderValues[1];
                currentHuman.agility = sliderValues[2];
                ShowStage(Stage.Intellectual);
                break;

            case Stage.Intellectual:
                currentHuman.logic = sliderValues[3];
                currentHuman.creativity = sliderValues[4];
                currentHuman.learnability = sliderValues[5];
                ShowStage(Stage.Mental);
                break;

            case Stage.Mental:
                currentHuman.emotionalStability = sliderValues[6];
                currentHuman.socialSkills = sliderValues[7];
                currentHuman.motivation = sliderValues[8];
                CreateHuman(); // Создание персонажа
                break;
        }
    }

    // Метод для создания персонажа
    void CreateHuman()
    {
        Debug.Log("Character created!");
        Debug.Log("Physical: " + currentHuman.strength + ", " + currentHuman.endurance + ", " + currentHuman.agility);
        Debug.Log("Intellectual: " + currentHuman.logic + ", " + currentHuman.creativity + ", " + currentHuman.learnability);
        Debug.Log("Mental: " + currentHuman.emotionalStability + ", " + currentHuman.socialSkills + ", " + currentHuman.motivation);
        // Здесь можно добавить логику создания персонажа
    }

    public enum Stage
    {
        Physical,       // Первый этап (физика)
        Intellectual,   // Второй этап (интеллект)
        Mental          // Третий этап (ментальные)
    }
}
