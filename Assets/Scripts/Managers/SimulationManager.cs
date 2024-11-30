using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SimulationManager : MonoBehaviour
{
    public Human man;
    public Human woman;

    public GameObject statsPanel;
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;

    public Era currentEra;

    private float averageStrength;
    private float averageEndurance;
    private float averageAgility;
    private float averageLogic;
    private float averageCreativity;
    private float averageLearnability;
    private float averageEmotionalStability;
    private float averageSocialSkills;
    private float averageMotivation;

    List<Stat> humanityStats = new List<Stat>();
    private List<Quest> availableQuests = new List<Quest>();

    public List<Era> eras = new List<Era>();
    public int currentEraIndex = 0;


    private static SimulationManager _instance;
    public static SimulationManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SimulationManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("SimulationManager");
                    _instance = obj.AddComponent<SimulationManager>();
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
            DontDestroyOnLoad(this.gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        man = HumanManager.Instance.chosenMan;
        woman = HumanManager.Instance.chosenWoman;
        CalculateAverageStats();
        UIManager.Instance.UpdateStatsPanel(humanityStats);
        currentEra = eras[currentEraIndex];

        // Start quest cycle
        StartCoroutine(StartEra(currentEra));
    }

    // Start the quest cycle
    public IEnumerator StartEra(Era era)
    {
        // Display the initial message
        yield return StartCoroutine(UIManager.Instance.ShowStartEraMessage(era));

        // Get available quests for the era
        availableQuests = QuestManager.Instance.GetQuestsForEra(era); // Replace with the current era dynamically

        // Start the quest cycle
        yield return StartCoroutine(StartQuestCycle());
    }

    // Cycle through quests
    public IEnumerator StartQuestCycle()
    {
        // Select 3 random quests
        List<Quest> selectedQuests = GetRandomQuests(3);

        // Show first quest
        yield return StartCoroutine(UIManager.Instance.DisplayQuest(selectedQuests[0]));

        // Show second quest
        yield return StartCoroutine(UIManager.Instance.DisplayQuest(selectedQuests[1]));

        // Show third quest
        yield return StartCoroutine(UIManager.Instance.DisplayQuest(selectedQuests[2]));

        // After all quests, check the conditions for the era
        CheckPrimitiveSocietyEvents(); // Replace with your era-specific condition check
    }

    // Get random quests
    public List<Quest> GetRandomQuests(int count)
    {
        List<Quest> selectedQuests = new List<Quest>();
        List<Quest> available = new List<Quest>(availableQuests);

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, available.Count);
            selectedQuests.Add(available[randomIndex]);
            available.RemoveAt(randomIndex);
        }

        return selectedQuests;
    }

    // Calculate the average stats of both man and woman
    void CalculateAverageStats()
    {
        List<Stat> manStats = man.Stats;
        List<Stat> womanStats = woman.Stats;

        for (int i = 0; i < manStats.Count; i++)
        {
            float averageValue = (manStats[i].Value + womanStats[i].Value) / 2;
            string abbreviation = manStats[i].Abbreviation;
            humanityStats.Add(new Stat(manStats[i].Name, abbreviation, averageValue));
        }
    }

    public void ApplyResult(string result)
    {


    }

    // Check the events for the "Primitive Society" era
    void CheckPrimitiveSocietyEvents()
    {
        if (averageStrength > 4 || averageLogic > 5)
        {
            Debug.Log("Invention of tools has occurred");
        }
        else
        {
            Debug.Log("Invention of tools did not occur");
        }

        // Add other conditions based on the stats as needed
    }
}
