using UnityEngine;
using UnityEngine.UI;

public class CreateButton : MonoBehaviour
{
    public HumanPanel humanPanel;    // Ссылка на скрипт HumanPanel
    public CreatePanel createPanel;  // Ссылка на скрипт CreatePanel
    private Button button;           // Ссылка на саму кнопку

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    // Метод, который будет вызываться при нажатии на кнопку
    void OnButtonClick()
    {
        // Проверяем, что ссылки на панели не пустые
        if (humanPanel != null && createPanel != null)
        {
            // Проверяем название родительского объекта для определения пола
            bool isFemale = transform.parent.name.Contains("WomanPanel");

            // Вызываем метод перемещения панели HumanPanel
            humanPanel.MoveToTarget(() =>
            {
                // Подготавливаем CreatePanel
                PrepareCreatePanel(isFemale);
                // Перемещаем CreatePanel
                StartCreatePanelMovement(isFemale);
            });
        }
        else
        {
            Debug.LogError("Ссылки на панели не прикреплены!");
        }
    }

    // Метод для начала анимации перемещения CreatePanel
    void StartCreatePanelMovement(bool isFemale)
    {
        createPanel.Move(isFemale);
    }

    void PrepareCreatePanel(bool isFemale)
    {
        createPanel.Prepare(isFemale);
    }
}
