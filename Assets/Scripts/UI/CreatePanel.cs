using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; // Для работы с LINQ
using System.Collections.Generic;
using System; // Для работы со списками

public class CreatePanel : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI pointsText;

    public GameObject closeButton;

    public Button confirmButton;
    public Button resetButton;
    public Slider[] sliders;

    public Human currentHuman;

    public Vector2 startPosition;
    public Vector2 targetPosition;
    public float moveDuration = 0.5f;

    private RectTransform rectTransform;
    private Tween moveTween;
    private Vector2 hiddenPosition;

    private Stage currentStage;

    public int availablePoints = 0;

    // Ссылка на InputField для имени персонажа
    public TMP_InputField nameInputField;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        confirmButton.onClick.AddListener(OnConfirmClicked);
        resetButton.onClick.AddListener(ResetSliders);
        ShowStage(Stage.Physical);

        UIManager.Instance.createPanel = this;
    }

    void ShowStage(Stage stage)
    {
        currentStage = stage;

        sliders[0].gameObject.SetActive(true);
        sliders[1].gameObject.SetActive(true);
        sliders[2].gameObject.SetActive(true);
    }

    public void Prepare(bool isFemale)
    {
        currentStage = Stage.Physical;

        currentHuman = isFemale ? new Woman() : new Man();
        titleText.text = isFemale ? "Create Female" : "Create Male";
        UpdateCreatePanel();

        hiddenPosition = startPosition;

        rectTransform.anchoredPosition = startPosition;
    }

    public void Move(bool isFemale)
    {
        if (moveTween != null && moveTween.IsActive())
        {
            moveTween.Kill();
        }

        moveTween = rectTransform.DOAnchorPos(targetPosition, moveDuration)
                                 .SetEase(Ease.InOutQuad);
    }

    public void MoveToHidden()
    {
        if (moveTween != null && moveTween.IsActive())
        {
            moveTween.Kill();
        }

        moveTween = rectTransform.DOAnchorPos(hiddenPosition, moveDuration)
                                 .SetEase(Ease.InOutQuad);
    }

    void OnConfirmClicked()
    {
        // Получаем имя из InputField, если оно пустое, устанавливаем дефолтное уникальное имя
        string characterName = string.IsNullOrEmpty(nameInputField.text) ? GetUniqueDefaultName(currentHuman is Woman) : nameInputField.text;
        currentHuman.humanName = characterName;

        // Обновляем значения Stat на основе текущей стадии
        switch (currentStage)
        {
            case Stage.Physical:
                currentHuman.Stats.First(stat => stat.Name == "Strength").Value = sliders[0].value;
                currentHuman.Stats.First(stat => stat.Name == "Endurance").Value = sliders[1].value;
                currentHuman.Stats.First(stat => stat.Name == "Agility").Value = sliders[2].value;
                ShowStage(Stage.Intellectual);
                break;

            case Stage.Intellectual:
                currentHuman.Stats.First(stat => stat.Name == "Logic").Value = sliders[0].value;
                currentHuman.Stats.First(stat => stat.Name == "Creativity").Value = sliders[1].value;
                currentHuman.Stats.First(stat => stat.Name == "Learnability").Value = sliders[2].value;
                ShowStage(Stage.Mental);
                break;

            case Stage.Mental:
                currentHuman.Stats.First(stat => stat.Name == "Emotional Stability").Value = sliders[0].value;
                currentHuman.Stats.First(stat => stat.Name == "Social Skills").Value = sliders[1].value;
                currentHuman.Stats.First(stat => stat.Name == "Motivation").Value = sliders[2].value;
                closeButton.GetComponent<CloseCreatePanelButton>().OnButtonClick();
                CreateHuman();
                break;
        }

        UpdateCreatePanel();
    }

    // Метод для получения уникального имени (для мальчиков "Adam", для девочек "Eva")
    string GetUniqueDefaultName(bool isFemale)
    {
        string baseName = isFemale ? "Eva" : "Adam";
        int index = 1;
        string newName = baseName + index;

        // Проходим по списку существующих персонажей и ищем максимальный номер
        while (HumanManager.Instance.listHumans.Any(human => human.humanName == newName))
        {
            index++;
            newName = baseName + index;
        }

        return newName;
    }

    public void UpdateCreatePanel()
    {
        infoText.text = "Choose ";

        switch (currentStage)
        {
            case Stage.Physical:
                infoText.text += "Physical Parameters";

                sliders[0].value = currentHuman.Stats.First(stat => stat.Name == "Strength").Value;
                sliders[0].transform.Find("SliderTitle").GetComponent<TextMeshProUGUI>().text = "Strength";
                sliders[1].value = currentHuman.Stats.First(stat => stat.Name == "Endurance").Value;
                sliders[1].transform.Find("SliderTitle").GetComponent<TextMeshProUGUI>().text = "Endurance";
                sliders[2].value = currentHuman.Stats.First(stat => stat.Name == "Agility").Value;
                sliders[2].transform.Find("SliderTitle").GetComponent<TextMeshProUGUI>().text = "Agility";

                break;

            case Stage.Intellectual:
                infoText.text += "Intellectual Parameters";

                sliders[0].value = currentHuman.Stats.First(stat => stat.Name == "Logic").Value;
                sliders[0].transform.Find("SliderTitle").GetComponent<TextMeshProUGUI>().text = "Logic";
                sliders[1].value = currentHuman.Stats.First(stat => stat.Name == "Creativity").Value;
                sliders[1].transform.Find("SliderTitle").GetComponent<TextMeshProUGUI>().text = "Creativity";
                sliders[2].value = currentHuman.Stats.First(stat => stat.Name == "Learnability").Value;
                sliders[2].transform.Find("SliderTitle").GetComponent<TextMeshProUGUI>().text = "Learnability";

                break;

            case Stage.Mental:
                infoText.text += "Mental Parameters";

                sliders[0].value = currentHuman.Stats.First(stat => stat.Name == "Emotional Stability").Value;
                sliders[0].transform.Find("SliderTitle").GetComponent<TextMeshProUGUI>().text = "Emotional Stability";
                sliders[1].value = currentHuman.Stats.First(stat => stat.Name == "Social Skills").Value;
                sliders[1].transform.Find("SliderTitle").GetComponent<TextMeshProUGUI>().text = "Social Skills";
                sliders[2].value = currentHuman.Stats.First(stat => stat.Name == "Motivation").Value;
                sliders[2].transform.Find("SliderTitle").GetComponent<TextMeshProUGUI>().text = "Motivation";

                break;
        }
        UpdateAvailablePoints();
    }

    private void ResetSliders()
    {
        foreach (Slider slider in sliders)
        {
            slider.value = 0;
        }
    }

    void CreateHuman()
    {
        HumanManager.Instance.AddHuman(currentHuman);
        HumanManager.Instance.AssignHuman(currentHuman);
    }

    internal void UpdateAvailablePoints()
    {
        int tempValue = 0;

        foreach(Slider slider in sliders)
        {
            tempValue += (int)slider.value;
        }

        switch (currentStage)
        {
            case Stage.Physical:
                pointsText.text = $"Available points: {GameManager.Instance.physicsPoints - tempValue}/{GameManager.Instance.physicsPoints}";
                availablePoints = GameManager.Instance.physicsPoints - tempValue;
                break;

            case Stage.Intellectual:
                pointsText.text = $"Available points: {GameManager.Instance.intellectPoints - tempValue}/{GameManager.Instance.intellectPoints}";
                availablePoints = GameManager.Instance.intellectPoints - tempValue;
                break;

            case Stage.Mental:
                pointsText.text = $"Available points: {GameManager.Instance.mentalPoints - tempValue}/{GameManager.Instance.mentalPoints}";
                availablePoints = GameManager.Instance.mentalPoints - tempValue;
                break;
        }
    }

    public enum Stage
    {
        Physical,
        Intellectual,
        Mental
    }
}
