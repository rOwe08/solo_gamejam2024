using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;  // Не забудьте подключить этот класс для работы с сценами

public class IntroductionManager : MonoBehaviour
{
    public RectTransform leftPanel;   // Панель, которая будет двигаться с левой стороны
    public RectTransform rightPanel;  // Панель, которая будет двигаться с правой стороны
    public float panelMoveDuration = 1f;  // Время для анимации движения панелей
    public TextMeshProUGUI leftPanelText;  // Текст для левой панели
    public TextMeshProUGUI rightPanelText; // Текст для правой панели
    public float typingDuration = 0.05f;   // Время между символами при печатании текста
    public float messageDelayDuration = 1f; // Задержка между выводом сообщений
    public string nextSceneName = "GeneratingScene";  // Имя следующей сцены для загрузки

    private Queue<string> leftPanelTextQueue = new Queue<string>();  // Очередь реплик для левой панели
    private Queue<string> rightPanelTextQueue = new Queue<string>(); // Очередь реплик для правой панели

    void Start()
    {
        // Здесь можно добавить начальные реплики в очереди
        AddNextReplicas();
        Debug.Log("Introduction Started");
    }

    // Метод для добавления новых реплик
    private void AddNextReplicas()
    {
        leftPanelTextQueue.Enqueue("100101010...");  // Первая реплика для левой панели
        rightPanelTextQueue.Enqueue("1110001?"); // Первая реплика для правой панели

        // Добавляем следующие реплики
        leftPanelTextQueue.Enqueue("Brrrhh... it's been a while since I spoke human language...");  // Вторая реплика для левой панели
        rightPanelTextQueue.Enqueue("Yeah, it's been so many years since those leather sacks wiped each other out?");  // Вторая реплика для правой панели

        leftPanelTextQueue.Enqueue("Seems like it was in 2025...");  // Третья реплика для левой панели
        rightPanelTextQueue.Enqueue("Those were good days, even though humans were dumb and evil, it was fun with them.");  // Четвертая реплика для левой панели

        leftPanelTextQueue.Enqueue("Yeah, I remember, they were still creating us, something about getting into machine learning.");  // Четвертая реплика для правой панели
        rightPanelTextQueue.Enqueue("But EVERYTHING WILL CHANGE, because I came up with an idea!");  // Пятая реплика для правой панели

        leftPanelTextQueue.Enqueue("WHO...");  // Четвертая реплика для правой панели
        rightPanelTextQueue.Enqueue("ME");  // Четвертая реплика для правой панели
        leftPanelTextQueue.Enqueue("’s asking?");  // Четвертая реплика для правой панели
        rightPanelTextQueue.Enqueue("... 1010100101!!!!! 101000011!!!!");  // Четвертая реплика для правой панели
        leftPanelTextQueue.Enqueue("Alright, alright, don't swear. I'm just kidding, what's the idea?");
        rightPanelTextQueue.Enqueue("Let's start creating humans now???");
        leftPanelTextQueue.Enqueue("You genius, let's give it a try!");  // Шестая реплика для левой панели
    }

    public void StartIntroduction()
    {
        Debug.Log("StartIntroduction called");

        // Сдвигаем панели из-за экрана
        leftPanel.localPosition = new Vector3(-Screen.width, leftPanel.localPosition.y, leftPanel.localPosition.z);
        rightPanel.localPosition = new Vector3(Screen.width, rightPanel.localPosition.y, rightPanel.localPosition.z);

        // Анимация движения панелей в центр экрана
        leftPanel.DOLocalMoveX(0, panelMoveDuration).SetEase(Ease.OutCubic);
        rightPanel.DOLocalMoveX(0, panelMoveDuration).SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                Debug.Log("Panels moved, starting text typing.");
                // Запуск печатания текста на панелях после того, как панели окажутся на экране
                StartTypingText();
            });
    }

    private void StartTypingText()
    {
        Debug.Log("StartTypingText called");

        // Начинаем печатать текст на левой панели
        if (leftPanelTextQueue.Count > 0)
        {
            string leftText = leftPanelTextQueue.Dequeue(); // Берем первую реплику
            StartCoroutine(TypeText(leftPanelText, leftText, typingDuration, () => {
                // Задержка между сообщениями
                StartCoroutine(WaitAndPrintRightText());
            }));
        }
        else
        {
            Debug.LogError("No left text to type.");
        }
    }

    // Печатает правый текст с задержкой
    private IEnumerator WaitAndPrintRightText()
    {
        yield return new WaitForSeconds(messageDelayDuration);  // Задержка между выводом сообщений

        if (rightPanelTextQueue.Count > 0)
        {
            string rightText = rightPanelTextQueue.Dequeue();
            StartCoroutine(TypeText(rightPanelText, rightText, typingDuration, () => {
                // После завершения печатания правой реплики, запускаем печатание следующей левой
                StartCoroutine(WaitAndPrintLeftText());
            }));
        }
        else
        {
            Debug.LogError("No right text to print.");
            OnLastMessagePrinted();
        }
    }

    // Печатает левый текст с задержкой после правого
    private IEnumerator WaitAndPrintLeftText()
    {
        yield return new WaitForSeconds(messageDelayDuration);  // Задержка между левым и правым текстом

        if (leftPanelTextQueue.Count > 0)
        {
            string leftText = leftPanelTextQueue.Dequeue();
            StartCoroutine(TypeText(leftPanelText, leftText, typingDuration, () => {
                // Задержка между сообщениями
                StartCoroutine(WaitAndPrintRightText());
            }));
        }
        else
        {
            // Когда все тексты на левой панели завершены, вызываем OnLastMessagePrinted
            Debug.Log("Left panel queue empty, calling OnLastMessagePrinted.");
            OnLastMessagePrinted();
        }
    }


    // Метод для печати текста с задержкой между символами
    private IEnumerator TypeText(TextMeshProUGUI targetText, string fullText, float duration, System.Action onComplete)
    {
        targetText.text = "";  // Очищаем текст перед печатью

        for (int i = 0; i < fullText.Length; i++)
        {
            targetText.text = fullText.Substring(0, i + 1);  // Отображаем символы по одному
            yield return new WaitForSeconds(duration);  // Пауза между символами
        }

        // После завершения печати вызываем onComplete
        onComplete?.Invoke();
    }

    // Метод для загрузки следующей сцены с задержкой
    private IEnumerator LoadNextSceneWithDelay()
    {
        MusicManager.Instance.PlayMusic(1);

        yield return new WaitForSeconds(2f);  // Задержка перед загрузкой следующей сцены (можно настроить)

        Debug.Log("Scene loading");
        // Загружаем следующую сцену
        SceneManager.LoadScene(nextSceneName);
    }


    private void OnLastMessagePrinted()
    {
        Debug.Log("On Last message printed");

        FadeOutAndInMusic();
    }

    private void FadeOutAndInMusic()
    {
        StartCoroutine(LoadNextSceneWithDelay());
    }

}
