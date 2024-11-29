using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreatePanel : MonoBehaviour
{
    public TextMeshProUGUI titleText;        // ����� ��� ���������
    public Button confirmButton;            // ������ �������������
    public Slider[] sliders;                // ������ ���������

    public Human currentHuman;              // ������� ������� (���� ��� ���)

    public Vector2 femaleStartPosition;     // ��������� ������� ��� �������
    public Vector2 maleStartPosition;       // ��������� ������� ��� �������
    public Vector2 femaleTargetPosition;    // ������� ������� ��� �������
    public Vector2 maleTargetPosition;      // ������� ������� ��� �������
    public float moveDuration = 0.5f;       // ������������ ��������

    private RectTransform rectTransform;
    private Tween moveTween;
    private Vector2 hiddenPosition;         // ������� �������

    private Stage currentStage;  // ������� ����
    private float[] sliderValues; // �������� ��������� ��� ������� �����

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        currentHuman = new Human();
        sliderValues = new float[sliders.Length];

        // ��������� ��������� ��������
        confirmButton.onClick.AddListener(OnConfirmClicked);
        ShowStage(Stage.Physical);
    }

    // ����� ��� ����������� ������� �����
    void ShowStage(Stage stage)
    {
        currentStage = stage;

        // �������� ��� ��������
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].gameObject.SetActive(false);
        }

        switch (stage)
        {
            case Stage.Physical:
                titleText.text = "Select Physical Attributes";
                sliders[0].gameObject.SetActive(true); // ����
                sliders[1].gameObject.SetActive(true); // ������������
                sliders[2].gameObject.SetActive(true); // ��������
                break;

            case Stage.Intellectual:
                titleText.text = "Select Intellectual Attributes";
                sliders[3].gameObject.SetActive(true); // ������
                sliders[4].gameObject.SetActive(true); // ������������
                sliders[5].gameObject.SetActive(true); // �����������
                break;

            case Stage.Mental:
                titleText.text = "Select Mental Attributes";
                sliders[6].gameObject.SetActive(true); // ������������� ������������
                sliders[7].gameObject.SetActive(true); // ���������� ������
                sliders[8].gameObject.SetActive(true); // ���������
                break;
        }
    }

    // ����� ��� ���������� CreatePanel � ������ ���������� ����
    public void Prepare(bool isFemale)
    {
        // ������� ��������� ������� ������ (Woman ��� Man)
        currentHuman = isFemale ? new Woman() : new Man();

        // ��������� ����� ���������
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
        // ��������� ������� �������
        Vector2 startPosition = isFemale ? femaleStartPosition : maleStartPosition;
        hiddenPosition = startPosition;

        // ������������� ��������� ������� ������ ����� ���������
        rectTransform.anchoredPosition = startPosition;
    }

    // ����� ��� ����������� CreatePanel �� ������� �������
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

    // ����� ��� �������� CreatePanel � ������� �������
    public void MoveToHidden()
    {
        if (moveTween != null && moveTween.IsActive())
        {
            moveTween.Kill();
        }

        // ���������� ������ � ������� �������, ������� ���� ����������� �����
        moveTween = rectTransform.DOAnchorPos(hiddenPosition, moveDuration)
                                 .SetEase(Ease.InOutQuad);
    }

    // ���������� ������� �� ������ "Confirm"
    void OnConfirmClicked()
    {
        // ��������� �������� ��������� � ����������� �� �������� �����
        for (int i = 0; i < sliders.Length; i++)
        {
            if (sliders[i].gameObject.activeSelf) // ��������� ������ �������� ��������
            {
                sliderValues[i] = sliders[i].value;
            }
        }

        // ��������� �������� � ����������� �� �����
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
                CreateHuman(); // �������� ���������
                break;
        }
    }

    // ����� ��� �������� ���������
    void CreateHuman()
    {
        Debug.Log("Character created!");
        Debug.Log("Physical: " + currentHuman.strength + ", " + currentHuman.endurance + ", " + currentHuman.agility);
        Debug.Log("Intellectual: " + currentHuman.logic + ", " + currentHuman.creativity + ", " + currentHuman.learnability);
        Debug.Log("Mental: " + currentHuman.emotionalStability + ", " + currentHuman.socialSkills + ", " + currentHuman.motivation);
        // ����� ����� �������� ������ �������� ���������
    }

    public enum Stage
    {
        Physical,       // ������ ���� (������)
        Intellectual,   // ������ ���� (���������)
        Mental          // ������ ���� (����������)
    }
}
