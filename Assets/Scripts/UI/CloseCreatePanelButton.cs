using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CloseCreatePanelButton : MonoBehaviour
{
    public CreatePanel createPanel;

    public HumanPanel womanPanel;    
    public HumanPanel manPanel;     

    private HumanPanel activeHumanPanel;  

    void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        if (createPanel != null)
        {
            DetermineActiveHumanPanel();

            if (activeHumanPanel != null)
            {
                createPanel.MoveToHidden();

                StartCoroutine(MoveHumanPanelAfterCreatePanel());
            }
            else
            {
                Debug.LogError("јктивна€ панель не найдена!");
            }
        }
        else
        {
            Debug.LogError("—сылка на CreatePanel не прикреплена!");
        }
    }

    void DetermineActiveHumanPanel()
    {
        if (womanPanel.rectTransform.anchoredPosition != womanPanel.startPosition)
        {
            activeHumanPanel = womanPanel;
        }
        else if (manPanel.rectTransform.anchoredPosition != manPanel.startPosition)
        {
            activeHumanPanel = manPanel;
        }
        else
        {
            activeHumanPanel = null; 
        }
    }

    private IEnumerator MoveHumanPanelAfterCreatePanel()
    {
        yield return new WaitForSeconds(createPanel.moveDuration);

        activeHumanPanel.MoveToStart();
    }
}
