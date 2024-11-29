using System.Collections;
using UnityEngine;
using DG.Tweening;

public class HumanPanel : MonoBehaviour
{
    public Vector2 targetPosition;    // ������� ������� ��� �����������
    public Vector2 startPosition;     // ��������� �������
    public float moveDuration = 0.5f; // ������������ ��������

    public RectTransform rectTransform;
    private Tween moveTween;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }

    // ����� ��� ����������� HumanPanel �� ������� ������� � ������ �������� �� ����������
    public void MoveToTarget(System.Action onComplete)
    {
        if (moveTween != null && moveTween.IsActive())
        {
            moveTween.Kill();
        }

        moveTween = rectTransform.DOAnchorPos(targetPosition, moveDuration)
                                 .SetEase(Ease.InOutQuad)
                                 .OnComplete(() => onComplete?.Invoke()); // ����� �������� ����� ���������� ��������
    }

    // ����� ��� �������� ������ �� ��������� �������
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
