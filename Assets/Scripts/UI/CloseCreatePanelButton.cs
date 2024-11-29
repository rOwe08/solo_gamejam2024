using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CloseCreatePanelButton : MonoBehaviour
{
    public CreatePanel createPanel;  // ������ �� CreatePanel

    public HumanPanel womanPanel;    // ������ �� WomanPanel
    public HumanPanel manPanel;      // ������ �� ManPanel

    private HumanPanel activeHumanPanel;  // �������� ������, ������� ����� �������

    void Start()
    {
        // ���������� ������� �� ������� ������ ��� ������ �������� ������
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnButtonClick);
    }

    // �����, ������� ����� ���������� ��� ������� �� ������
    void OnButtonClick()
    {
        if (createPanel != null)
        {
            // ����������, ����� ������ ������� (������� ��� �������)
            DetermineActiveHumanPanel();

            if (activeHumanPanel != null)
            {
                // ������� �������� ����� ����������� CreatePanel � ������� �������
                createPanel.MoveToHidden();

                // ����� ���������� �������� ����������� CreatePanel, ���������� �������� HumanPanel
                StartCoroutine(MoveHumanPanelAfterCreatePanel());
            }
            else
            {
                Debug.LogError("�������� ������ �� �������!");
            }
        }
        else
        {
            Debug.LogError("������ �� CreatePanel �� �����������!");
        }
    }

    // ����� ��� ����������� �������� HumanPanel
    void DetermineActiveHumanPanel()
    {
        // ���������� ������� ������� ������ ������ � � ��������� ��������
        if (womanPanel.rectTransform.anchoredPosition != womanPanel.startPosition)
        {
            activeHumanPanel = womanPanel; // ���� ������� ������� ����������, ��� �������
        }
        else if (manPanel.rectTransform.anchoredPosition != manPanel.startPosition)
        {
            activeHumanPanel = manPanel; // ���� ������� ������� ����������, �� �������
        }
        else
        {
            activeHumanPanel = null; // ���� ��� ������ �� ����� ��������� ��������
        }
    }

    // ������� ��� ������������� ��������
    private IEnumerator MoveHumanPanelAfterCreatePanel()
    {
        // ������� ���������� �������� ����������� CreatePanel
        yield return new WaitForSeconds(createPanel.moveDuration);

        // ����� ���������� �������� ����������� CreatePanel, ���������� �������� HumanPanel
        activeHumanPanel.MoveToStart();
    }
}
