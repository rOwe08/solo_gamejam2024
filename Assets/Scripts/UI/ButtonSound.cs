using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource; // Ссылка на AudioSource
    public AudioClip buttonClickSound; // Звук при нажатии кнопки

    private Button button; // Ссылка на компонент Button

    void Start()
    {
        // Получаем компонент Button на текущем объекте
        button = GetComponent<Button>();

        // Подписываемся на событие нажатия на кнопку
        if (button != null)
        {
            button.onClick.AddListener(PlaySound);
        }

        // Если не задан AudioSource в инспекторе, пытаемся найти его на объекте
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();

            // Если AudioSource всё ещё не найден, создаём новый
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        // Отключаем PlayOnAwake, чтобы звук не воспроизводился сразу при старте
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
    }

    // Метод для проигрывания звука
    void PlaySound()
    {
        // Проверяем, есть ли привязанный звук
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
