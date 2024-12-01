using UnityEngine;
using UnityEngine.UI;

public class PrefabButtonAudio : MonoBehaviour
{
    public AudioClip buttonClickSound; // Звук для кнопки

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
    }

    void OnClick()
    {
        if (buttonClickSound != null)
        {
            // Отправляем звук синглтону
            SFXManager.Instance.PlaySound(buttonClickSound);
        }
    }
}
