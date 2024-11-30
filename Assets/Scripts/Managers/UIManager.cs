using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public HumanPanel womanPanel;
    public HumanPanel manPanel;
    public Button startButton;
    private PulsingObject pulsingStartButton;

    public GameObject answerButtonPrefab;

    // Новый объект для перехода (покрывающий экран)
    public Image transitionImage;
    private CanvasGroup transitionCanvasGroup;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject("UIManager");
                    _instance = obj.AddComponent<UIManager>();
                }
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);  // Для UIManager, чтобы он сохранялся между сценами
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Получаем компонент PulsingObject для кнопки
        pulsingStartButton = startButton.GetComponent<PulsingObject>();

        // Получаем CanvasGroup на объекте перехода
        transitionCanvasGroup = transitionImage.GetComponent<CanvasGroup>();

        // Изначально устанавливаем альфу в 0
        if (transitionCanvasGroup != null)
        {
            transitionCanvasGroup.alpha = 0;
            transitionCanvasGroup.interactable = false; // Отключаем взаимодействие, чтобы другие элементы UI были активны
            transitionCanvasGroup.blocksRaycasts = false; // Отключаем блокировку кликов
        }

        startButton.onClick.AddListener(OnStartButtonClicked);

        // Делаем transitionImage тоже DontDestroyOnLoad
        DontDestroyOnLoad(transitionImage.gameObject);
    }

    void Update()
    {
        // Проверяем, назначены ли оба персонажа
        if (womanPanel.assignedHuman != null && manPanel.assignedHuman != null)
        {
            // Делаем кнопку доступной для нажатия и активируем пульсацию
            startButton.interactable = true;
            if (pulsingStartButton != null)
            {
                pulsingStartButton.isActive = true;  // Включаем пульсацию
            }
        }
        else
        {
            // Если хотя бы один персонаж не назначен, делаем кнопку недоступной и останавливаем пульсацию
            startButton.interactable = false;
            if (pulsingStartButton != null)
            {
                pulsingStartButton.isActive = false;  // Останавливаем пульсацию
            }
        }
    }

    public void UpdateStatsPanel(List<Stat> humanityStats)
    {
        GameObject simulationCanvas = GameObject.Find("SimulationCanvas");
        GameObject statsPanel = simulationCanvas.transform.Find("StatsPanel").gameObject;

        for (int i = 0; i < humanityStats.Count; i++)
        {
            foreach (Transform child in statsPanel.transform)
            {
                TextMeshProUGUI textComponent = child.GetComponent<TextMeshProUGUI>();

                if (textComponent != null && child.name.Contains(humanityStats[i].Name))
                {
                    // Округляем число вниз и отображаем его без плавающей точки
                    int roundedValue = Mathf.FloorToInt(humanityStats[i].Value);
                    textComponent.text = $"{humanityStats[i].Abbreviation}: {roundedValue}";
                    break;
                }
            }
        }
    }

    // Function to display the quest and wait for user interaction
    public IEnumerator DisplayQuest(Quest quest)
    {
        GameObject questCanvas = GameObject.Find("QuestCanvas");
        GameObject questPanel = questCanvas.transform.Find("QuestPanel").gameObject;
        GameObject buttonsLayout = questPanel.transform.Find("ButtonsLayout").gameObject;

        // Display the quest's title and description
        questPanel.SetActive(true); // Show the quest panel
        questPanel.transform.Find("QuestText").GetComponent<TextMeshProUGUI>().text = quest.Description;

        // Clear previous answer buttons (if any)
        ClearAnswerButtons(buttonsLayout);

        // Instantiate a button for each answer
        for (int i = 0; i < quest.Answers.Count; i++)
        {
            // Instantiate the button from the prefab
            GameObject answerButton = Instantiate(answerButtonPrefab, buttonsLayout.transform);
            TextMeshProUGUI buttonText = answerButton.GetComponentInChildren<TextMeshProUGUI>(); // Find the text component inside the button prefab

            // Set the button text to the answer's text
            buttonText.text = quest.Answers[i].Text;

            // Add the OnClick listener to trigger the selected answer's action
            Button button = answerButton.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                // Invoke the selected answer's action
                quest.Answers[i].OnChosen?.Invoke();

                // Hide the quest canvas after the answer is selected
                questCanvas.SetActive(false);
            });
        }

        // Wait for the user to select an answer (button click)
        yield return new WaitUntil(() => !questCanvas.activeSelf); // Wait until the quest canvas is hidden

        // After the quest is answered, you can proceed with other actions
        yield return new WaitForSeconds(1f);  // Optionally wait before moving on
    }

    // Function to clear the answer buttons
    private void ClearAnswerButtons(GameObject gO)
    {
        // Clear previous answer buttons (remove all children in the container)
        foreach (Transform child in gO.transform)
        {
            Destroy(child.gameObject);
        }
    }


// Function to display the message and wait for button click
public IEnumerator ShowMessage(string message, string textForButton = "OK", float delay = 0)
    {
        GameObject messageCanvas = GameObject.Find("MessageCanvas");

        GameObject messagePanel = null;
        GameObject confirmButton = null;

        TextMeshProUGUI messageText = null;

        if (messageCanvas != null)
        {
            // Get references to the message panel and confirm button
            messagePanel = messageCanvas.transform.Find("MessagePanel").gameObject;
            messageText = messagePanel.GetComponentInChildren<TextMeshProUGUI>();

            confirmButton = messagePanel.transform.Find("ConfirmButton").gameObject;
        }
        else
        {
            Debug.LogError("MessageCanvas not found in the scene.");
            yield break;
        }

        // Wait for the initial delay
        yield return new WaitForSeconds(delay);

        // Show the message panel
        messagePanel.SetActive(true);
        messageText.text = message; // Set the message text

        // Set the text for the button
        confirmButton.transform.Find("ConfirmText").GetComponent<TextMeshProUGUI>().text = textForButton;

        // Wait for the user to click the confirm button
        bool buttonClicked = false;

        // Add a listener for the confirm button
        confirmButton.GetComponent<Button>().onClick.AddListener(() => {
            buttonClicked = true;
        });

        // Wait until the button is clicked
        while (!buttonClicked)
        {
            yield return null; // Wait one frame and check again
        }

        // After button click, hide the message panel
        HideMessage();
    }

    // Метод для отображения сообщения, теперь принимает объект Era
    public IEnumerator ShowStartEraMessage(Era era)
    {
        string descriptionText = "";
        string textForConfirmButton = "";

        // Определяем текст для каждой эпохи
        if (era.eraName.Contains("Primitive"))
        {
            descriptionText = "First people created new village";
            textForConfirmButton = "Oh... Here we ho again...";
        }
        else if (era.eraName.Contains("Ancient"))
        {
            descriptionText = "The Rise of Ancient Civilization";
            textForConfirmButton = "Great empires are forming. Will they focus on war or peace?";
        }
        else if (era.eraName.Contains("Middle"))
        {
            descriptionText = "The Middle Ages";
            textForConfirmButton = "Knights, castles, and kingdoms are emerging. What path will humanity choose?";
        }
        else if (era.eraName.Contains("Renaissance"))
        {
            descriptionText = "The Renaissance";
            textForConfirmButton = "A rebirth of knowledge, culture, and art. How will society evolve?";
        }
        else if (era.eraName.Contains("Modern"))
        {
            descriptionText = "The Renaissance";
            textForConfirmButton = "A rebirth of knowledge, culture, and art. How will society evolve?";
        }
        else if (era.eraName.Contains("Our"))
        {
            descriptionText = "The Renaissance";
            textForConfirmButton = "A rebirth of knowledge, culture, and art. How will society evolve?";
        }
        else if (era.eraName.Contains("Future"))
        {
            descriptionText = "The Renaissance";
            textForConfirmButton = "A rebirth of knowledge, culture, and art. How will society evolve?";
        }

        // Вызываем ShowMessage для отображения текста
        yield return StartCoroutine(ShowMessage(descriptionText, textForConfirmButton, 3f));
    }

    // Функция для скрытия табло через определённое время
    public void HideMessage()
    {
        GameObject messageCanvas = GameObject.Find("MessageCanvas");

        GameObject messagePanel = null;

        if (messageCanvas != null)
        {
            // Получаем ссылку на панель и текстовое поле внутри SimulationCanvas
            messagePanel = messageCanvas.transform.Find("MessagePanel").gameObject;
        }
        else
        {
            Debug.LogError("MessageCanvas не найден в сцене.");
        }

        messagePanel.SetActive(false); // Скрываем панель
    }

    public void OnStartButtonClicked()
    {
        // Запускаем анимацию для плавного увеличения альфы с использованием CanvasGroup
        transitionCanvasGroup.DOFade(1, 1f).OnComplete(() =>
        {
            // После завершения анимации загрузим следующую сцену
            LoadNextScene();
        });
    }

    private void LoadNextScene()
    {
        // Загрузка следующей сцены
        SceneManager.LoadScene("SimulationScene"); // Замените на имя вашей сцены

        // Начинаем анимацию перехода в следующей сцене
        StartCoroutine(WaitForSceneAndFadeOut());
    }

    private IEnumerator WaitForSceneAndFadeOut()
    {
        // Ждем пока сцена загрузится
        yield return new WaitForSeconds(1f);  // Это зависит от длительности анимации, подберите нужное время

        // Начинаем уменьшать альфу с 1 до 0
        transitionCanvasGroup.DOFade(0, 1f);
    }
}
