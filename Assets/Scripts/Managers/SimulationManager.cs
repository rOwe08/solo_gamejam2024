using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Playables;
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

    List<Stat> humanityStats = new List<Stat>();
    List<Stat> abstractStats = new List<Stat>();

    private List<Quest> availableQuests = new List<Quest>();

    public List<Era> eras = new List<Era>();
    public int currentEraIndex = 0;

    public int amountOfCompletedEvents = 0;

    void Start()
    {
        man = HumanManager.Instance.chosenMan;
        woman = HumanManager.Instance.chosenWoman;
        CalculateAverageStats();
        UIManager.Instance.UpdateStatsPanel(humanityStats, "StatsPanel");
        currentEra = eras[currentEraIndex];

        abstractStats = new List<Stat>
        {
            new Stat("Culture", "CUL", 0),      // Правильная аббревиатура для Культуры
            new Stat("Science", "SCI", 0),      // Правильная аббревиатура для Науки
            new Stat("Religion", "REL", 0),     // Правильная аббревиатура для Религии
        };

        AssignEventsToEras();

        UIManager.Instance.UpdateStatsPanel(abstractStats, "AbstractStatsPanel");

        // Start quest cycle
        StartCoroutine(StartEra(currentEra));
    }

    // Start the quest cycle
    public IEnumerator StartEra(Era era)
    {
        UIManager.Instance.ChangeSimulationBackground(era.successSprite);

        UpdateEventsPanel(era);
        
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

    private void UpdateEventsPanel(Era era)
    {
        CheckForHappenedEvents(era);
        UIManager.Instance.UpdateEventsPanel(era);
    }

    private void CheckForHappenedEvents(Era era)
    {
        int numHappenedEvents = 0;

        if(era.events != null)
        {
            // Перебираем все события в текущей эре
            foreach (EraEvent eraEvent in era.events)
            {
                bool allRequirementsMet = true;

                // Перебираем все требования текущего события
                foreach (Stat requirement in eraEvent.eventReqs)
                {
                    // Проверяем, существует ли требуемая статистика в текущих статах
                    Stat currentStat = GetCurrentStat(requirement.Abbreviation);

                    // Если текущая статистика меньше требуемой, то условие не выполнено
                    if (currentStat == null || currentStat.Value < requirement.Value)
                    {
                        allRequirementsMet = false;
                        numHappenedEvents--;
                        break;
                    }
                }

                // Если все требования выполнены, событие произошло
                eraEvent.happened = allRequirementsMet;
                numHappenedEvents++;
            }

            amountOfCompletedEvents = numHappenedEvents;
        }
    }

    // Метод для получения текущей статистики по аббревиатуре
    private Stat GetCurrentStat(string abbreviation)
    {
        Stat statToReturn = null;

        // Проверка текущих статистик человечества (man и woman)
        foreach (Stat stat in humanityStats) // humanityStats — это список текущих статей человечества
        {
            if (stat.Abbreviation == abbreviation)
            {
                statToReturn = stat;
                break;
            }
        }

        if (statToReturn == null)
        {
            foreach(Stat stat in abstractStats)
            {
                if(stat.Abbreviation == abbreviation)
                {
                    statToReturn = stat;
                    break;
                }
            }
        }
       
        return statToReturn;
    }

    // Cycle through quests
    public IEnumerator StartQuestCycle()
    {
        if (!currentEra.eraName.Contains("Future"))
        {
            // Select 3 random quests
            List<Quest> selectedQuests = GetRandomQuests(5);

            // Show first quest
            yield return StartCoroutine(UIManager.Instance.DisplayQuest(selectedQuests[0]));
            UpdateEventsPanel(currentEra);

            // Show second quest
            yield return StartCoroutine(UIManager.Instance.DisplayQuest(selectedQuests[1]));
            UpdateEventsPanel(currentEra);

            // Show third quest
            yield return StartCoroutine(UIManager.Instance.DisplayQuest(selectedQuests[2]));
            UpdateEventsPanel(currentEra);

            // Show third quest
            yield return StartCoroutine(UIManager.Instance.DisplayQuest(selectedQuests[3]));
            UpdateEventsPanel(currentEra);

            // Show third quest
            yield return StartCoroutine(UIManager.Instance.DisplayQuest(selectedQuests[4]));
            UpdateEventsPanel(currentEra);

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
            float manValue = manStats[i].Value;
            float womanValue = womanStats[i].Value;

            // Применяем соответствующие коэффициенты для каждого стата
            switch (manStats[i].Name)
            {
                case "Strength":
                manValue *= 1.5f;
                womanValue *= 0.7f;
                break;
                case "Endurance":
                manValue *= 1.3f;
                womanValue *= 0.9f;
                break;
                case "Agility":
                manValue *= 1.2f;
                womanValue *= 1.4f;
                break;
                case "Logic":
                manValue *= 1.2f;
                womanValue *= 1.0f;
                break;
                case "Creativity":
                manValue *= 0.8f;
                womanValue *= 1.3f;
                break;
                case "Learnability":
                manValue *= 1.0f;
                womanValue *= 1.2f;
                break;
                case "Emotional Stability":
                manValue *= 0.7f;
                womanValue *= 1.5f;
                break;
                case "Social Skills":
                manValue *= 0.6f;
                womanValue *= 1.5f;
                break;
                case "Motivation":
                manValue *= 1.1f;
                womanValue *= 1.2f;
                break;
            }

            // Рассчитываем среднее значение после применения коэффициентов
            float averageValue = (manValue + womanValue) / 2;
            string abbreviation = manStats[i].Abbreviation;

            // Добавляем среднее значение в humanityStats
            humanityStats.Add(new Stat(manStats[i].Name, abbreviation, averageValue));
        }
    }

    public void ApplyResult(List<string> result)
    {

        foreach(string s in result)
        {
            // Разделяем результат, чтобы получить статы и их значения
            string[] resultParts = s.Split(' ');

            // Если результат состоит из нескольких частей, то обработаем каждую часть
            if (resultParts.Length == 2)
            {
                string statAbbreviation = resultParts[0]; // Аббревиатура статы (например, "CUL")
                float statChange = float.Parse(resultParts[1]); // Изменение статы (например, "+1" или "-1")

                // Находим соответствующую стату в humanityStats
                Stat statToUpdate = humanityStats.Find(stat => stat.Abbreviation == statAbbreviation);

                if (statToUpdate == null)
                {
                    statToUpdate = abstractStats.Find(stat => stat.Abbreviation == statAbbreviation);

                    if (statToUpdate == null)
                    {
                        Debug.Log($"{statAbbreviation}DOESNT EXIST!");
                    }
                    else
                    {
                        // Применяем изменение
                        statToUpdate.Value += statChange;

                        // Обновляем UI с новым значением статы
                        UIManager.Instance.UpdateStatsPanel(abstractStats, "AbstractStatsPanel");
                    }
                }
                else
                {
                    // Применяем изменение
                    statToUpdate.Value += statChange;

                    // Обновляем UI с новым значением статы
                    UIManager.Instance.UpdateStatsPanel(humanityStats, "StatsPanel");
                }
            }
        }
        
    }

    public void AssignEventsToEras()
    {
        // Primitive Society to Ancient World
        eras[0].events = new List<EraEvent>
        {
            new EraEvent("Discovery of Fire", new List<Stat> { new Stat("Logic", "LOG", 2) }),
            new EraEvent("Invention of Stone Tools", new List<Stat> { new Stat("Strength", "STR", 3) }),
            new EraEvent("Development of Language", new List<Stat> {
                new Stat("Creativity", "SOC", 1),
                new Stat("Learnability", "LRN", 2)}),
            new EraEvent("Formation of Tribal Societies", new List<Stat> { new Stat("Social Skills", "SOC", 2) }),
            new EraEvent("Domestication of Animals", new List<Stat> { new Stat("Endurance", "END", 2) })
        };
        // Ancient World to Antiquity
        eras[1].events = new List<EraEvent>
        {
            new EraEvent("Invention of the Wheel", new List<Stat> { new Stat("Strength", "STR", 3) }),
            new EraEvent("Foundation of Early Cities", new List<Stat> { new Stat("Social Skills", "SOC", 3) }),
            new EraEvent("Agricultural Revolution", new List<Stat> { new Stat("Endurance", "END", 2) }),
            new EraEvent("Development of Written Language", new List<Stat> { new Stat("Creativity", "CRE", 2) }),
            new EraEvent("Introduction of Trade", new List<Stat> { new Stat("Social Skills", "SOC", 3) })
        };

        // Antiquity to Middle Ages
        eras[2].events = new List<EraEvent>
        {
            new EraEvent("The Rise of Empires", new List<Stat> { new Stat("Strength", "STR", 3), new Stat("Social Skills", "SOC", 3) }),
            new EraEvent("Invention of the Printing Press", new List<Stat> { new Stat("Creativity", "CRE", 3) }),
            new EraEvent("Expansion of Trade Routes", new List<Stat> { new Stat("Social Skills", "SOC", 2) }),
            new EraEvent("The Spread of Religion", new List<Stat> { new Stat("Religion", "REL", 3) }),
            new EraEvent("Medieval Warfare", new List<Stat> { new Stat("Strength", "STR", 4) })
        };

        // Middle Ages to Renaissance
        eras[3].events = new List<EraEvent>
        {
            new EraEvent("The Renaissance", new List<Stat> { new Stat("Creativity", "CRE", 4), new Stat("Science", "SCI", 3) }),
            new EraEvent("The Printing Revolution", new List<Stat> { new Stat("Creativity", "CRE", 3) }),
            new EraEvent("Scientific Discoveries", new List<Stat> { new Stat("Science", "SCI", 4) }),
            new EraEvent("Reformation", new List<Stat> { new Stat("Religion", "REL", 2) }),
            new EraEvent("Rise of Humanism", new List<Stat> { new Stat("Creativity", "CRE", 3) })
        };

        // Renaissance to Modern Era
        eras[4].events = new List<EraEvent>
        {
            new EraEvent("Industrial Revolution", new List<Stat> { new Stat("Strength", "STR", 4), new Stat("Science", "SCI", 3) }),
            new EraEvent("Invention of the Steam Engine", new List<Stat> { new Stat("Science", "SCI", 4) }),
            new EraEvent("Development of Capitalism", new List<Stat> { new Stat("Motivation", "MOT", 2) }),
            new EraEvent("Rise of Global Empires", new List<Stat> { new Stat("Strength", "STR", 3) }),
            new EraEvent("The Enlightenment", new List<Stat> { new Stat("Logic", "LOG", 3) })
        };

        // Modern Era to Our Time
        eras[5].events = new List<EraEvent>
        {
            new EraEvent("World Wars", new List<Stat> { new Stat("Strength", "STR", 4), new Stat("Social Skills", "SOC", 3) }),
            new EraEvent("Space Exploration", new List<Stat> { new Stat("Science", "SCI", 5) }),
            new EraEvent("Technological Advancements", new List<Stat> { new Stat("Science", "SCI", 4) }),
            new EraEvent("Digital Revolution", new List<Stat> { new Stat("Logic", "LOG", 4) }),
            new EraEvent("Rise of Globalization", new List<Stat> { new Stat("Social Skills", "SOC", 4) })
        };

        // Our Time to Future
        eras[6].events = new List<EraEvent>
        {
            new EraEvent("Climate Change Awareness", new List<Stat> { new Stat("Social Skills", "SOC", 3), new Stat("Science", "SCI", 3) }),
            new EraEvent("Artificial Intelligence", new List<Stat> { new Stat("Logic", "LOG", 5) }),
            new EraEvent("Space Colonization", new List<Stat> { new Stat("Science", "SCI", 4) }),
            new EraEvent("Universal Basic Income", new List<Stat> { new Stat("Social Skills", "SOC", 3) }),
            new EraEvent("Technological Unemployment", new List<Stat> { new Stat("Social Skills", "SOC", 2), new Stat("Logic", "LOG", 2) })
        };
    }
    IEnumerator WinGame()
    {
        yield return new WaitForSeconds(1f);

        Stat statReward = DetermineReward(true); // true — это победа
        string rewardType = "";

        if (statReward.Abbreviation == "STR" || statReward.Abbreviation == "END" || statReward.Abbreviation == "AGI")
        {
            rewardType = "Physical Parameters";
            GameManager.Instance.physicsPoints++;
        }
        else if(statReward.Abbreviation == "LOG" || statReward.Abbreviation == "CRE" || statReward.Abbreviation == "LRN")
        {
            rewardType = "Intellectual Parameters";
            GameManager.Instance.intellectPoints++;
        }
        else if (statReward.Abbreviation == "EMO" || statReward.Abbreviation == "SOC" || statReward.Abbreviation == "MOT")
        {
            rewardType = "Mental Parameters";
            GameManager.Instance.mentalPoints++;
        }
        string rewardText = $"{rewardType} increased as a reward!";

        StartCoroutine(UIManager.Instance.ShowMessage("Pupa and Lupa managed to rebuild human civilization and lead it to success!", "Back", rewardText));

        Debug.Log("YOU WIN!");
    }

    IEnumerator LoseGame()
    {
        UIManager.Instance.ChangeSimulationBackground(currentEra.failureSprite);

        yield return new WaitForSeconds(1f);

        Stat statReward = DetermineReward(false); // false — это поражение

        string rewardType = "";

        if (statReward.Abbreviation == "STR" || statReward.Abbreviation == "END" || statReward.Abbreviation == "AGI")
        {
            rewardType = "Physical Parameters";
            GameManager.Instance.physicsPoints++;
        }
        else if (statReward.Abbreviation == "LOG" || statReward.Abbreviation == "CRE" || statReward.Abbreviation == "LRN")
        {
            rewardType = "Intellectual Parameters";
            GameManager.Instance.intellectPoints++;
        }
        else if (statReward.Abbreviation == "EMO" || statReward.Abbreviation == "SOC" || statReward.Abbreviation == "MOT")
        {
            rewardType = "Mental Parameters";
            GameManager.Instance.mentalPoints++;
        }
        string rewardText = $"{rewardType} increased as a consolation prize!";

        StartCoroutine(UIManager.Instance.ShowMessage("Pupa and Lupa were disappointed, even though they are robots and have no feelings, that it didn't work out this time. But they are not giving up.", "Back", rewardText));

        Debug.Log("YOU LOSE!");
    }

    private Stat DetermineReward(bool isVictory)
    {
        if (isVictory)
        {
            return DetermineMaxStat();  // Возвращаем максимальный стат при победе
        }
        else
        {
            return DetermineMinStat();  // Возвращаем минимальный стат при поражении
        }
    }

    private Stat DetermineMinStat()
    {
        Stat minStat = humanityStats[0]; // Инициализируем первым статом

        // Поиск стата с минимальным значением среди человечества
        foreach (Stat stat in humanityStats)
        {
            if (stat.Value < minStat.Value)
            {
                minStat = stat;
            }
        }

        return minStat;
    }

    private Stat DetermineMaxStat()
    {
        Stat maxStat = humanityStats[0]; // Инициализируем первым статом

        // Поиск стата с максимальным значением среди человечества
        foreach (Stat stat in humanityStats)
        {
            if (stat.Value > maxStat.Value)
            {
                maxStat = stat;
            }
        }

        return maxStat;
    }


    bool CheckForNextEraConditions()
    {

        foreach (Stat stat in humanityStats)
        {
            if (stat.Value < 0)
            {
                return false;
            }
        }

        foreach (Stat stat in abstractStats)
        {
            if (stat.Value < 0)
            {
                return false;
            }
        }

        if (amountOfCompletedEvents >= UIManager.Instance.requiredNumOfEvents)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
