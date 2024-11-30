using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
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
        UIManager.Instance.ChangeSimulationBackground(era.successSprite);

        // Display the initial message
        yield return StartCoroutine(UIManager.Instance.ShowStartEraMessage(era));

        if (era.eraName.Contains("Future"))
        {
            StartCoroutine(WinGame());
        }
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

        bool ToNextEra = CheckForNextEraConditions();

        if (ToNextEra)
        {
            currentEraIndex++;
            currentEra = eras[currentEraIndex];

            StartCoroutine(StartEra(currentEra));
        }
        else
        {
            StartCoroutine(LoseGame());
        }
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

    IEnumerator WinGame()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("YOU WIN!");
    }

    IEnumerator LoseGame()
    {
        UIManager.Instance.ChangeSimulationBackground(currentEra.failureSprite);

        yield return new WaitForSeconds(1f);

        StartCoroutine(UIManager.Instance.ShowMessage("You lose:(", "Back:("));

        Debug.Log("YOU LOSE!");
    }

    // Check the events for the "Primitive Society" era
    bool CheckForNextEraConditions()
    {
        return false;
    }
}
