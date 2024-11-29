using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CloseCreatePanelButton : MonoBehaviour
{
    public CreatePanel createPanel;  // Ссылка на CreatePanel

    public HumanPanel womanPanel;    // Ссылка на WomanPanel
    public HumanPanel manPanel;      // Ссылка на ManPanel

    private HumanPanel activeHumanPanel;  // Активная панель, которую нужно вернуть

    void Start()
    {
        // Добавление события на нажатие кнопки для вызова закрытия панели
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnButtonClick);
    }

    // Метод, который будет вызываться при нажатии на кнопку
    void OnButtonClick()
    {
        if (createPanel != null)
        {
            // Определяем, какая панель активна (женская или мужская)
            DetermineActiveHumanPanel();

            if (activeHumanPanel != null)
            {
                // Сначала вызываем метод перемещения CreatePanel в скрытую позицию
                createPanel.MoveToHidden();

                // После завершения анимации перемещения CreatePanel, перемещаем активную HumanPanel
                StartCoroutine(MoveHumanPanelAfterCreatePanel());
            }
            else
            {
                Debug.LogError("Активная панель не найдена!");
            }
        }
        else
        {
            Debug.LogError("Ссылка на CreatePanel не прикреплена!");
        }
    }

    // Метод для определения активной HumanPanel
    void DetermineActiveHumanPanel()
    {
        // Сравниваем текущую позицию каждой панели с её стартовой позицией
        if (womanPanel.rectTransform.anchoredPosition != womanPanel.startPosition)
        {
            activeHumanPanel = womanPanel; // Если позиция женщины изменилась, она активна
        }
        else if (manPanel.rectTransform.anchoredPosition != manPanel.startPosition)
        {
            activeHumanPanel = manPanel; // Если позиция мужчины изменилась, он активен
        }
        else
        {
            activeHumanPanel = null; // Если обе панели на своих стартовых позициях
        }
    }

    // Корутин для синхронизации анимаций
    private IEnumerator MoveHumanPanelAfterCreatePanel()
    {
        // Ожидаем завершения анимации перемещения CreatePanel
        yield return new WaitForSeconds(createPanel.moveDuration);

        // После завершения анимации перемещения CreatePanel, перемещаем активную HumanPanel
        activeHumanPanel.MoveToStart();
    }
}
