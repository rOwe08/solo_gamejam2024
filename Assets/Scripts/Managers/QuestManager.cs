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
        if (era.eraName == "PrimitiveSocietyEra")
        {
            quests.Add(new Quest(
                "Invention of Tools", "Primitive Society",
                "The humans are discovering tools for the first time. What should they prioritize?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Prioritize strength", "STR +1", () => { /* Increase strength logic */ }),
                    new QuestAnswer("Focus on creativity", "CRE +1", () => { /* Increase creativity */ }),
                    new QuestAnswer("Focus on survival", "END +1", () => { /* Increase endurance */ })
                }
            ));

            quests.Add(new Quest(
                "Discovery of Fire", "Primitive Society",
                "The discovery of fire will change humanity's future. What should it be used for?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Warmth and cooking", "COM +1", () => { /* Increase comfort */ }),
                    new QuestAnswer("Weaponry and defense", "DEF +1", () => { /* Increase defense */ }),
                    new QuestAnswer("Rituals and community bonding", "SOC +1", () => { /* Increase social cohesion */ })
                }
            ));

            quests.Add(new Quest(
                "The First Shelter", "Primitive Society",
                "Humans are learning to build shelters. What is the best way to design them?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Build compact and sturdy shelters", "SUR +1", () => { /* Increase survival */ }),
                    new QuestAnswer("Build large communal shelters", "SOC +1", () => { /* Increase social unity */ }),
                    new QuestAnswer("Build temporary shelters for mobility", "EXP +1", () => { /* Increase exploration */ })
                }
            ));

            quests.Add(new Quest(
                "Hunting vs. Farming", "Primitive Society",
                "Should humanity prioritize hunting or farming as their primary food source?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Focus on hunting for meat", "HUN +1", () => { /* Increase hunting skill */ }),
                    new QuestAnswer("Focus on farming for stable food", "AGRI +1", () => { /* Increase agriculture */ }),
                    new QuestAnswer("Focus on a balanced approach", "RES +1", () => { /* Increase resource management */ })
                }
            ));

            quests.Add(new Quest(
                "The First Language", "Primitive Society",
                "Humans are starting to communicate. What form of language should they adopt?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Simple gestures and sounds", "COM +1", () => { /* Increase communication efficiency */ }),
                    new QuestAnswer("Develop complex spoken language", "INT +1", () => { /* Increase intellectual growth */ }),
                    new QuestAnswer("Develop written language", "REC +1", () => { /* Increase record keeping */ })
                }
            ));
        }

        // Ancient Civilization
        else if (era.eraName == "Ancient Civilization")
        {
            quests.Add(new Quest(
                "The Birth of Writing", "Ancient Civilization",
                "A system of writing is emerging. How should it be used?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("For trade and commerce", "ECO +1", () => { /* Increase economy */ }),
                    new QuestAnswer("For religious texts", "REL +1", () => { /* Increase spiritual influence */ }),
                    new QuestAnswer("For historical record-keeping", "KNO +1", () => { /* Increase knowledge */ })
                }
            ));

            quests.Add(new Quest(
                "The First War", "Ancient Civilization",
                "The first war has broken out. Should we focus on offense or defense?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Focus on offense to conquer", "MIL +1", () => { /* Increase military power */ }),
                    new QuestAnswer("Focus on defense to protect", "DEF +1", () => { /* Increase fortifications */ }),
                    new QuestAnswer("Focus on diplomacy to avoid war", "REL +1", () => { /* Increase relations */ })
                }
            ));

            quests.Add(new Quest(
                "The Rise of Monarchy", "Ancient Civilization",
                "A monarchy is being established. How should the leader be chosen?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Through divine right", "REL +1", () => { /* Increase religion influence */ }),
                    new QuestAnswer("Through election by the people", "DEM +1", () => { /* Increase democracy */ }),
                    new QuestAnswer("Through hereditary monarchy", "NOB +1", () => { /* Increase nobility power */ })
                }
            ));

            quests.Add(new Quest(
                "The Great Pyramid", "Ancient Civilization",
                "Should we build a massive monument for the pharaoh or use resources elsewhere?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Build the pyramid to honor the pharaoh", "REL +1", () => { /* Increase religious influence */ }),
                    new QuestAnswer("Use resources for public works", "HAP +1", () => { /* Increase public happiness */ }),
                    new QuestAnswer("Focus on military development", "MIL +1", () => { /* Increase military power */ })
                }
            ));

            quests.Add(new Quest(
                "The First Code of Laws", "Ancient Civilization",
                "A code of laws is being proposed. What should it focus on?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Focus on property rights", "ECO +1", () => { /* Increase economy */ }),
                    new QuestAnswer("Focus on religious morality", "REL +1", () => { /* Increase spiritual strength */ }),
                    new QuestAnswer("Focus on equality for all", "SOC +1", () => { /* Increase social stability */ })
                }
            ));
        }

        // Medieval Era
        else if (era.eraName == "MedievalEra")
        {
            quests.Add(new Quest(
                "The Feudal System", "Medieval Era",
                "Feudalism is spreading. Should we embrace it fully or limit its power?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Embrace feudalism for stability", "CON +1", () => { /* Increase control */ }),
                    new QuestAnswer("Limit feudal power for fairness", "EQL +1", () => { /* Increase equality */ }),
                    new QuestAnswer("Focus on a different system entirely", "INN +1", () => { /* Increase innovation */ })
                }
            ));

            quests.Add(new Quest(
                "The Crusades", "Medieval Era",
                "A call for a crusade has been made. Should we join or stay neutral?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Join the crusade for religious glory", "REL +1", () => { /* Increase religious influence */ }),
                    new QuestAnswer("Stay neutral to avoid conflict", "PEA +1", () => { /* Increase peace */ }),
                    new QuestAnswer("Oppose the crusade for justice", "MOR +1", () => { /* Increase moral authority */ })
                }
            ));

            quests.Add(new Quest(
                "Black Plague", "Medieval Era",
                "A plague has struck. What measures should we take?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Quarantine affected areas", "HEA +1", () => { /* Increase health security */ }),
                    new QuestAnswer("Focus on finding a cure", "RES +1", () => { /* Increase scientific research */ }),
                    new QuestAnswer("Pray for divine intervention", "FAI +1", () => { /* Increase faith */ })
                }
            ));

            quests.Add(new Quest(
                "Renaissance Art", "Medieval Era",
                "The Renaissance is coming. Should we support the arts or focus on military development?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Support art and culture", "CUL +1", () => { /* Increase culture */ }),
                    new QuestAnswer("Focus on military power", "DEF +1", () => { /* Increase defense */ }),
                    new QuestAnswer("Support a balance between the two", "BAL +1", () => { /* Increase balance */ })
                }
            ));

            quests.Add(new Quest(
                "The Printing Press", "Medieval Era",
                "The printing press is invented. Should we support its widespread use?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Support the spread of knowledge", "EDU +1", () => { /* Increase education */ }),
                    new QuestAnswer("Control information for control", "CON +1", () => { /* Increase control */ }),
                    new QuestAnswer("Limit its use to the elite", "ARI +1", () => { /* Increase aristocracy power */ })
                }
            ));
        }

        // Add other era-specific quests here...

        return quests;
    }

}
