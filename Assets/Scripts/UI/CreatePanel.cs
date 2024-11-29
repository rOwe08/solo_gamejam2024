using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; // Для работы с LINQ

public class CreatePanel : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI infoText;

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
    private float[] sliderValues;

    // Ссылка на InputField для имени персонажа
    public TMP_InputField nameInputField;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        currentHuman = new Human();
        sliderValues = new float[sliders.Length];

        confirmButton.onClick.AddListener(OnConfirmClicked);
        resetButton.onClick.AddListener(ResetSliders);
        ShowStage(Stage.Physical);
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

        for (int i = 0; i < sliders.Length; i++)
        {
            if (sliders[i].gameObject.activeSelf)
            {
                sliderValues[i] = sliders[i].value;
            }
        }

        switch (currentStage)
        {
            case Stage.Physical:
            currentHuman.strength = sliderValues[0];
            currentHuman.endurance = sliderValues[1];
            currentHuman.agility = sliderValues[2];
            ShowStage(Stage.Intellectual);
            break;

            case Stage.Intellectual:
            currentHuman.logic = sliderValues[0];
            currentHuman.creativity = sliderValues[1];
            currentHuman.learnability = sliderValues[2];
            ShowStage(Stage.Mental);
            break;

            case Stage.Mental:
            currentHuman.emotionalStability = sliderValues[0];
            currentHuman.socialSkills = sliderValues[1];
            currentHuman.motivation = sliderValues[2];
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
            break;

            case Stage.Intellectual:
            infoText.text += "Intellectual Parameters";
            break;

            case Stage.Mental:
            infoText.text += "Mental Parameters";
            break;
        }

        switch (currentStage)
        {
            case Stage.Physical:
            sliders[0].value = currentHuman.strength;
            sliders[0].transform.Find("SliderTitle").GetComponent<TextMeshProUGUI>().text = "Strength";
            sliders[1].value = currentHuman.endurance;
            sliders[1].transform.Find("SliderTitle").GetComponent<TextMeshProUGUI>().text = "Endurance";
            sliders[2].value = currentHuman.agility;
            sliders[2].transform.Find("SliderTitle").GetComponent<TextMeshProUGUI>().text = "Agility";
            break;

            case Stage.Intellectual:
            sliders[0].value = currentHuman.logic;
            sliders[0].transform.Find("SliderTitle").GetComponent<TextMeshProUGUI>().text = "Logic";
            sliders[1].value = currentHuman.creativity;
            sliders[1].transform.Find("SliderTitle").GetComponent<TextMeshProUGUI>().text = "Creativity";
            sliders[2].value = currentHuman.learnability;
            sliders[2].transform.Find("SliderTitle").GetComponent<TextMeshProUGUI>().text = "Learnability";
            break;

            case Stage.Mental:
            sliders[0].value = currentHuman.emotionalStability;
            sliders[0].transform.Find("SliderTitle").GetComponent<TextMeshProUGUI>().text = "Emotional Stability";
            sliders[1].value = currentHuman.socialSkills;
            sliders[1].transform.Find("SliderTitle").GetComponent<TextMeshProUGUI>().text = "Social Skills";
            sliders[2].value = currentHuman.motivation;
            sliders[2].transform.Find("SliderTitle").GetComponent<TextMeshProUGUI>().text = "Motivation";
            break;
        }
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
        Debug.Log("Character created!");
        Debug.Log("Name: " + currentHuman.humanName);
        Debug.Log("Physical: " + currentHuman.strength + ", " + currentHuman.endurance + ", " + currentHuman.agility);
        Debug.Log("Intellectual: " + currentHuman.logic + ", " + currentHuman.creativity + ", " + currentHuman.learnability);
        Debug.Log("Mental: " + currentHuman.emotionalStability + ", " + currentHuman.socialSkills + ", " + currentHuman.motivation);

        HumanManager.Instance.AddHuman(currentHuman);
        HumanManager.Instance.AssignHuman(currentHuman);
    }

    public enum Stage
    {
        Physical,
        Intellectual,
        Mental
    }
}
