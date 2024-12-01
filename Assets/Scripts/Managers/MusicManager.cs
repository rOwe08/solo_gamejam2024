using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    // Синглтон экземпляр
    public static MusicManager Instance;

    private AudioSource audioSource;  // AudioSource для фоновой музыки
    public AudioClip[] musicClips;   // Массив из 3-х разных звуков

    public float fadeDuration = 2f;  // Время для плавного затухания

    void Awake()
    {
        // Проверка, чтобы экземпляр синглтона был только один
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Сохраняем объект между сценами
        }
        else
        {
            Destroy(gameObject);  // Удаляем новый экземпляр, если он уже существует
        }

        // Получаем компонент AudioSource
        audioSource = GetComponent<AudioSource>();

        // Отключаем автоматическое воспроизведение музыки на старте
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
    }

    private void Start()
    {

        int buildIndex = 0;
        if (SceneManager.GetActiveScene().name == "SimulationScene")
        {
            buildIndex = 2;
        }
        else if (SceneManager.GetActiveScene().name == "GeneratingScene")
        {
            buildIndex = 1;
        }
        else
        {
            buildIndex = 0;
        }
        PlayMusic(buildIndex);
    }
    // Метод для начала проигрывания музыки
    public void PlayMusic(int clipIndex)
    {
        if (clipIndex < 0 || clipIndex >= musicClips.Length)
        {
            Debug.LogWarning("Invalid clip index.");
            return;
        }

        // Если музыка уже играет, плавно её затухаем
        if (audioSource.isPlaying)
        {
            StartCoroutine(FadeOutAndPlayNewMusic(clipIndex));
        }
        else
        {
            // Если музыка не играет, просто начинаем воспроизведение нового клипа
            audioSource.clip = musicClips[clipIndex];
            audioSource.Play();
        }
    }

    // Корутина для плавного затухания текущей музыки и начала новой
    public System.Collections.IEnumerator FadeOutAndPlayNewMusic(int clipIndex)
    {
        // Затухаем текущий трек
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop();  // Останавливаем текущую музыку

        // Меняем клип на новый
        audioSource.clip = musicClips[clipIndex];
        audioSource.Play();

        // Плавно увеличиваем громкость
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, startVolume, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = startVolume;
    }

    // Метод для остановки музыки с плавным затуханием
    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            StartCoroutine(FadeOutAndStopMusic());
        }
    }

    private System.Collections.IEnumerator FadeOutAndStopMusic()
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop();
    }
}
