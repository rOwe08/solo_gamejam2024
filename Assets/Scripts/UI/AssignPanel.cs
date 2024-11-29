using UnityEngine;
using UnityEngine.UI; // Для работы с UI компонентами
using TMPro; // Для использования TextMeshPro

public class AssignPanel : MonoBehaviour
{
    bool isWomanPanel;

    public Transform panelToSpawnButtons;  // Панель, где будут размещаться кнопки
    public GameObject humanButtonPrefab;   // Префаб кнопки
    private TextMeshProUGUI buttonText;     // Текст, который будет на кнопке


    public void PreparePanel()
    {
        isWomanPanel = transform.parent != null && transform.parent.name.ToLower().Contains("woman");

        // Очищаем панель от старых кнопок
        foreach (Transform child in panelToSpawnButtons)
        {
            Destroy(child.gameObject);
        }

        // Перебираем всех людей из списка
        foreach (Human human in HumanManager.Instance.listHumans)
        {
            if (isWomanPanel)
            {
                if (human is Woman)
                {
                    SpawnButton(human);
                }
            }
            else
            {
                if (human is Man)
                {
                    SpawnButton(human);
                }
            }
        }
    }

    // Метод для создания кнопки и назначения текста
    private void SpawnButton(Human human)
    {
        // Создаем кнопку из префаба
        GameObject newButton = Instantiate(humanButtonPrefab, panelToSpawnButtons);

        // Получаем компонент TextMeshProUGUI для изменения текста на кнопке
        TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();

        // Изменяем текст на имя человека
        if (buttonText != null)
        {
            buttonText.text = human.humanName;
        }

        // Дополнительная логика, если необходимо, например, привязка обработчика события
        Button button = newButton.GetComponent<Button>();
        button.onClick.AddListener(() => OnButtonClicked(human));
    }

    // Метод обработки нажатия на кнопку
    private void OnButtonClicked(Human human)
    {
        Debug.Log("Button clicked for: " + human.humanName);
        // Добавьте вашу логику обработки нажатия на кнопку
    }
}
