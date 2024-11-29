using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CreateButton : MonoBehaviour
{
    public HumanPanel humanPanel;    // ������ �� ������ HumanPanel
    public CreatePanel createPanel;  // ������ �� ������ CreatePanel
    private Button button;           // ������ �� ���� ������

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    // �����, ������� ����� ���������� ��� ������� �� ������
    void OnButtonClick()
    {
        // ���������, ��� ������ �� ������ �� ������
        if (humanPanel != null && createPanel != null)
        {
            // ��������� �������� ������������� ������� ��� ����������� ����
            bool isFemale = transform.parent.name.Contains("WomanPanel");

            // �������� ����� ����������� ������ HumanPanel
            humanPanel.MoveToTarget(() => StartCreatePanelMovement(isFemale));
        }
        else
        {
            Debug.LogError("������ �� ������ �� �����������!");
        }
    }

    // ����� ��� ������ �������� ����������� CreatePanel
    void StartCreatePanelMovement(bool isFemale)
    {
        createPanel.PrepareAndMove(isFemale); // �������� ������ ����������
    }
}
