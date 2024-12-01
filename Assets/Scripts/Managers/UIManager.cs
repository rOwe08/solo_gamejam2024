using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public HumanPanel womanPanel;
    public HumanPanel manPanel;
    public Button startButton;
    private PulsingObject pulsingStartButton;
    public CreatePanel createPanel;

    public GameObject answerButtonPrefab;
    public GameObject eventPrefab;

    private GameObject questCanvas;
    private GameObject questPanel;
    private GameObject buttonsLayout;
    public GameObject eventsPanel;

    // Новый объект для перехода (покрывающий экран)
    public Image transitionImage;
    public CanvasGroup transitionCanvasGroup;

    public int requiredNumOfEvents = 3;
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
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GeneratingScene")
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
    }

    public void UpdateEventsPanel(Era era)
    {
        Transform parentLayout = eventsPanel.GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform;

        // Удаляем все дочерние объекты под родителем
        foreach (Transform child in parentLayout)
        {
            Destroy(child.gameObject);
        }

        int numHappenedEvents = 0;

        if(era.events != null)
        {
            // Далее идёт ваш код для обновления панели событий
            foreach (EraEvent eraEvent in era.events)
            {
                GameObject panel = Instantiate(eventPrefab, parentLayout);

                // Изменяем цвет панели
                Image panelImage = panel.GetComponent<Image>();
                if (panelImage != null)
                {
                    if (eraEvent.happened)
                    {
                        panelImage.color = new Color32(85, 171, 154, 255);  // #55AB9A в RGB
                        numHappenedEvents++;
                    }
                    else
                    {
                        panelImage.color = new Color32(171, 85, 93, 255);  // #AB555D в RGB
                    }
                }

                panel.transform.Find("EventTitle").GetComponent<TextMeshProUGUI>().text = eraEvent.eventName;
                panel.transform.Find("EventReqsText").GetComponent<TextMeshProUGUI>().text = "Need ";

                // Обработка требований события
                for (int i = 0; i < eraEvent.eventReqs.Count; i++)
                {
                    panel.transform.Find("EventReqsText").GetComponent<TextMeshProUGUI>().text +=
                        $">{eraEvent.eventReqs[i].Abbreviation}:{eraEvent.eventReqs[i].Value - 1}";
                    if (i != eraEvent.eventReqs.Count - 1)
                    {
                        panel.transform.Find("EventReqsText").GetComponent<TextMeshProUGUI>().text += ", ";
                    }
                }
            }

            eventsPanel.transform.Find("CompletedEventsText").GetComponent<TextMeshProUGUI>().text = $"Completed events {numHappenedEvents}/{requiredNumOfEvents} needed";
        }
    }



    public void UpdateStatsPanel(List<Stat> stats, string panelName)
    {
        GameObject simulationCanvas = GameObject.Find("SimulationCanvas");
        GameObject statsPanel = simulationCanvas.transform.Find(panelName).gameObject;

        for (int i = 0; i < stats.Count; i++)
        {
            foreach (Transform child in statsPanel.transform)
            {
                TextMeshProUGUI textComponent = child.GetComponent<TextMeshProUGUI>();

                if (textComponent != null && child.name.Contains(stats[i].Name))
                {
                    // Округляем число вниз и отображаем его без плавающей точки
                    int roundedValue = Mathf.FloorToInt(stats[i].Value);
                    textComponent.text = $"{stats[i].Abbreviation}: {roundedValue}";
                    break;
                }
            }
        }
    }

    // Function to display the quest and wait for user interaction
    public IEnumerator DisplayQuest(Quest quest)
    {
        if (questCanvas == null)
        {
            questCanvas = GameObject.Find("QuestCanvas");
        }
        if (questPanel == null)
        {
            questPanel = questCanvas.transform.Find("QuestPanel").gameObject;
        }
        if (buttonsLayout == null)
        {
            buttonsLayout = questPanel.transform.Find("ButtonsLayout").gameObject;
        }

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
            TextMeshProUGUI buttonText = answerButton.transform.Find("ButtonText").GetComponent<TextMeshProUGUI>(); // Find the text component inside the button prefab
            TextMeshProUGUI resultText = answerButton.transform.Find("ResultText").GetComponent<TextMeshProUGUI>(); // Find the text component inside the button prefab

            // Set the button text to the answer's text
            buttonText.text = quest.Answers[i].Description;
            resultText.text = quest.Answers[i].Result;

            if (resultText.text.Contains("+"))
            {
                resultText.color = Color.green;
            }
            else
            {
                resultText.color = Color.red;
            }

            // Use a local variable to capture the current value of 'i'
            int answerIndex = i;

            // Add the OnClick listener to trigger the selected answer's action
            Button button = answerButton.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                // Invoke the selected answer's action
                quest.Answers[answerIndex].OnChosen?.Invoke();

                questPanel.SetActive(false);
            });
        }

        // Wait for the user to select an answer (button click)
        yield return new WaitUntil(() => !questPanel.activeSelf); // Wait until the quest canvas is hidden
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
    public IEnumerator ShowMessage(string message, string textForButton = "OK", string award = null, float delay = 0)
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

            if (award != null) 
            {
                messagePanel.transform.Find("RewardText").gameObject.SetActive(true);
                messagePanel.transform.Find("RewardText").GetComponent<TextMeshProUGUI>().text =  $"Reward: {award}";
            }
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

        // After button click, check if the button text contains "Back"
        if (textForButton.Contains("Back"))
        {
            // Load the previous scene or desired scene
            // Replace "PreviousScene" with the actual scene name or use SceneManager.LoadScene("YourSceneName");
            GameManager.Instance.LoadNextScene("GeneratingScene"); // Change this to your desired scene name
        }
        else
        {
            // If not "Back", just hide the message
            HideMessage();
        }
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
        else if (era.eraName.Contains("Antiquity"))
        {
            descriptionText = "Antiquity era";
            textForConfirmButton = "Great empires are forming. Will they focus on war or peace?";
        }
        else if (era.eraName.Contains("Middle"))
        {
            descriptionText = "The Middle Ages Era";
            textForConfirmButton = "Knights, castles, and kingdoms are emerging. What path will humanity choose?";
        }
        else if (era.eraName.Contains("Renaissance"))
        {
            descriptionText = "The Renaissance Era";
            textForConfirmButton = "A rebirth of knowledge, culture, and art. How will society evolve?";
        }
        else if (era.eraName.Contains("Modern"))
        {
            descriptionText = "Modern Era";
            textForConfirmButton = "A rebirth of knowledge, culture, and art. How will society evolve?";
        }
        else if (era.eraName.Contains("Our"))
        {
            descriptionText = "Our time Era";
            textForConfirmButton = "A rebirth of knowledge, culture, and art. How will society evolve?";
        }
        else if (era.eraName.Contains("Future"))
        {
            descriptionText = "Future Era";
            textForConfirmButton = "A rebirth of knowledge, culture, and art. How will society evolve?";
        }

        // Вызываем ShowMessage для отображения текста
        yield return StartCoroutine(ShowMessage(descriptionText, textForConfirmButton, null, 3f));
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
        GameManager.Instance.LoadNextScene("SimulationScene");
    }

    public void ChangeSimulationBackground(Sprite sprite)
    {
        GameObject simulationCanvas = GameObject.Find("SimulationCanvas");

        Image backgroundImage = simulationCanvas.transform.Find("Background").GetComponent<Image>();
        backgroundImage.sprite = sprite;
    }

    public IEnumerator WaitForSceneAndFadeOut(float delay = 1f)
    {
        // Ждем пока сцена загрузится
        yield return new WaitForSeconds(delay);  // Это зависит от длительности анимации, подберите нужное время

        // Начинаем уменьшать альфу с 1 до 0
        transitionCanvasGroup.DOFade(0, 1f);
    }

    public int GetAvailablePoints()
    {
        return createPanel.availablePoints;
    }

    public void UpdateAvailablePoints()
    {
        if (createPanel != null)
        {
            createPanel.UpdateAvailablePoints();
        }
    }
}
