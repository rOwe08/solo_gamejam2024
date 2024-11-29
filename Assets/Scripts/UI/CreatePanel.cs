using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CreatePanel : MonoBehaviour
{
    public Vector2 femaleStartPosition;    // ��������� ������� ��� �������
    public Vector2 maleStartPosition;      // ��������� ������� ��� �������
    public Vector2 femaleTargetPosition;   // ������� ������� ��� �������
    public Vector2 maleTargetPosition;     // ������� ������� ��� �������
    public float moveDuration = 0.5f;      // ������������ ��������

    private RectTransform rectTransform;
    private Tween moveTween;
    private Vector2 hiddenPosition;        // ������� ������� (����� ����������� ����� ���������)

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // ����� ��� ���������� ������ ����� ��������� � ������ ��������� � ������� �������
    public void PrepareAndMove(bool isFemale)
    {
        // �������� ��������� � ������� ������� � ����������� �� ����
        Vector2 startPosition = isFemale ? femaleStartPosition : maleStartPosition;
        Vector2 targetPosition = isFemale ? femaleTargetPosition : maleTargetPosition;

        // ��������� ������� ������� ��� ������� ��������� �������
        hiddenPosition = startPosition;

        StartCoroutine(PrepareCoroutine(startPosition, targetPosition));
    }

    private IEnumerator PrepareCoroutine(Vector2 startPosition, Vector2 targetPosition)
    {
        // ���������� ����� ��������� (��������, ����� �� 0.5 �������)
        yield return new WaitForSeconds(0.5f);

        // ������������� ��������� �������
        rectTransform.anchoredPosition = startPosition;

        // ������ �������� ����������� ������ �� ������� �������
        MoveToTarget(targetPosition);
    }

    // ����� ��� ����������� CreatePanel �� ������� �������
    public void MoveToTarget(Vector2 targetPosition)
    {
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
