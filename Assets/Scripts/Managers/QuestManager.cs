using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private static QuestManager _instance;
    public static QuestManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<QuestManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("QuestManager");
                    _instance = obj.AddComponent<QuestManager>();
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

    // Get quests for the specified era
    public List<Quest> GetQuestsForEra(Era era)
    {
        List<Quest> quests = new List<Quest>();

        // Primitive Society
        if (era.eraName.Contains("Primitive"))
        {
            quests.Add(new Quest(
                "Invention of Tools", "Primitive Society",
                "The humans are discovering tools for the first time. What should they prioritize?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Prioritize strength", "STR +1"),
                    new QuestAnswer("Focus on creativity", "CRE +1" ),
                    new QuestAnswer("Focus on survival", "END +1" )
                }
            ));

            quests.Add(new Quest(
                "Discovery of Fire", "Primitive Society",
                "The discovery of fire will change humanity's future. What should it be used for?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Warmth and cooking", "COM +1"),
                    new QuestAnswer("Weaponry and defense", "DEF +1"),
                    new QuestAnswer("Rituals and community bonding", "SOC +1")
                }
            ));

            quests.Add(new Quest(
                "The First Shelter", "Primitive Society",
                "Humans are learning to build shelters. What is the best way to design them?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Build compact and sturdy shelters", "SUR +1"),
                    new QuestAnswer("Build large communal shelters", "SOC +1"),
                    new QuestAnswer("Build temporary shelters for mobility", "EXP +1")
                }
            ));

            quests.Add(new Quest(
                "Hunting vs. Farming", "Primitive Society",
                "Should humanity prioritize hunting or farming as their primary food source?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Focus on hunting for meat", "HUN +1"),
                    new QuestAnswer("Focus on farming for stable food", "AGRI +1"),
                    new QuestAnswer("Focus on a balanced approach", "RES +1")
                }
            ));

            quests.Add(new Quest(
                "The First Language", "Primitive Society",
                "Humans are starting to communicate. What form of language should they adopt?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Simple gestures and sounds", "COM +1"),
                    new QuestAnswer("Develop complex spoken language", "INT +1"),
                    new QuestAnswer("Develop written language", "REC +1")
                }
            ));

        }
        else if (era.eraName.Contains("Ancient"))
        {
            // Ancient Civilization
            quests.Add(new Quest(
                "The Birth of Writing", "Ancient Civilization",
                "A system of writing is emerging. How should it be used?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("For trade and commerce", "ECO +1"),
                    new QuestAnswer("For religious texts", "REL +1"),
                    new QuestAnswer("For historical record-keeping", "KNO +1")
                }
            ));

            quests.Add(new Quest(
                "The First War", "Ancient Civilization",
                "The first war has broken out. Should we focus on offense or defense?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Focus on offense to conquer", "MIL +1"),
                    new QuestAnswer("Focus on defense to protect", "DEF +1"),
                    new QuestAnswer("Focus on diplomacy to avoid war", "REL +1")
                }
            ));

            quests.Add(new Quest(
                "The Rise of Monarchy", "Ancient Civilization",
                "A monarchy is being established. How should the leader be chosen?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Through divine right", "REL +1"),
                    new QuestAnswer("Through election by the people", "DEM +1"),
                    new QuestAnswer("Through hereditary monarchy", "NOB +1")
                }
            ));

            quests.Add(new Quest(
                "The Great Pyramid", "Ancient Civilization",
                "Should we build a massive monument for the pharaoh or use resources elsewhere?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Build the pyramid to honor the pharaoh", "REL +1"),
                    new QuestAnswer("Use resources for public works", "HAP +1"),
                    new QuestAnswer("Focus on military development", "MIL +1")
                }
            ));

            quests.Add(new Quest(
                "The First Code of Laws", "Ancient Civilization",
                "A code of laws is being proposed. What should it focus on?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Focus on property rights", "ECO +1"),
                    new QuestAnswer("Focus on religious morality", "REL +1"),
                    new QuestAnswer("Focus on equality for all", "SOC +1")
                }
            ));
        }

        // Medieval Era
        else if (era.eraName.Contains("Antiquity"))
        {

        }

        // Add other era-specific quests here...

        return quests;
    }

}
