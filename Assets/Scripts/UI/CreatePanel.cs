using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreatePanel : MonoBehaviour
{
    public TextMeshProUGUI titleText;                  // ����� ��� ���������
    public Slider physicalSlider;           // ������� ��� ����������� ���������
    public Slider intellectualSlider;       // ������� ��� ����������������� ���������
    public Slider mentalSlider;             // ������� ��� ����������� ���������

    public Human currentHuman;              // ������� ������� (���� ��� ���)

    public Vector2 femaleStartPosition;     // ��������� ������� ��� �������
    public Vector2 maleStartPosition;       // ��������� ������� ��� �������
    public Vector2 femaleTargetPosition;    // ������� ������� ��� �������
    public Vector2 maleTargetPosition;      // ������� ������� ��� �������
    public float moveDuration = 0.5f;       // ������������ ��������

    private RectTransform rectTransform;
    private Tween moveTween;
    private Vector2 hiddenPosition;         // ������� �������

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // ����� ��� ���������� CreatePanel � ������ ���������� ����
    public void Prepare(bool isFemale)
    {
        // ������� ��������� ������� ������ (Woman ��� Man)
        currentHuman = isFemale ? new Woman() : new Man();

        // ��������� ����� ���������
        titleText.text = isFemale ? "Create Female" : "Create Male";

        // ������������� �������� ��������� � ����������� �� �������� ���������
        physicalSlider.value = currentHuman.physical;
        intellectualSlider.value = currentHuman.intellectual;
        mentalSlider.value = currentHuman.mental;

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
}
