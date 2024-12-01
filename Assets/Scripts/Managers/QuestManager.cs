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
                "Invention of Tools", "PrimitiveSocietyEra",
                "The humans are discovering tools for the first time. What should they prioritize?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Prioritize strength", new List<string> { "STR +1" }),
            new QuestAnswer("Focus on creativity", new List<string> { "CRE +1" })
                }
            ));

            quests.Add(new Quest(
                "Discovery of Fire", "PrimitiveSocietyEra",
                "The discovery of fire will change humanity's future. What should it be used for?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Warmth and cooking", new List<string> { "END +1" }),
            new QuestAnswer("Weapons and defense", new List<string> { "STR +1" }),
            new QuestAnswer("Rituals and community bonding", new List<string> { "REL +1" })
                }
            ));

            quests.Add(new Quest(
                "The First Shelter", "PrimitiveSocietyEra",
                "Humans are learning to build shelters. What is the best way to design them?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Build compact and sturdy shelters", new List<string> { "END +1" }),
            new QuestAnswer("Build temporary shelters for mobility", new List<string> { "AGI +1" })
                }
            ));

            quests.Add(new Quest(
                "A Drought Strikes", "PrimitiveSocietyEra",
                "A severe drought is threatening the community's food supply. What should be done?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on hunting, even if it's dangerous", new List<string> { "AGI -1" }),
            new QuestAnswer("Conserve water, but risk dehydration", new List<string> { "END -1" })
                }
            ));

            quests.Add(new Quest(
                "Hunting vs. Farming", "PrimitiveSocietyEra",
                "Should humanity prioritize hunting or farming as their primary food source?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on hunting for meat", new List<string> { "STR +1" }),
            new QuestAnswer("Focus on farming for stable food", new List<string> { "END +1" })
                }
            ));

            quests.Add(new Quest(
                "Tribal Conflicts", "PrimitiveSocietyEra",
                "Tensions are rising between neighboring tribes. How should humankind respond?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Avoid conflicts", new List<string> { "STR -1" }),
            new QuestAnswer("Prepare for war", new List<string> { "AGI -1" })
                }
            ));

            quests.Add(new Quest(
                "The First Language", "PrimitiveSocietyEra",
                "Humans are starting to communicate. What form of language should they adopt?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Simple gestures and sounds", new List<string> { "SOC +1" }),
            new QuestAnswer("Develop complex spoken language", new List<string> { "LRN +1" })
                }
            ));
        }
        else if (era.eraName.Contains("Ancient"))
        {
            quests.Add(new Quest(
                "The Birth of Writing", "AncientWorldEra",
                "A system of writing is emerging. How should it be used?",
                new List<QuestAnswer>
                {
            new QuestAnswer("For trade and commerce", new List<string> { "CUL +1" }),
            new QuestAnswer("For religious texts", new List<string> { "REL +1" }),
            new QuestAnswer("For historical record-keeping", new List<string> { "SCI +1" })
                }
            ));

            quests.Add(new Quest(
                "The Rise of Monarchy", "AncientWorldEra",
                "A monarchy is being established. How should the leader be chosen?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Through divine right", new List<string> { "REL +1" }),
            new QuestAnswer("Through election by the people", new List<string> { "SOC +1" })
                }
            ));

            quests.Add(new Quest(
                "A Devastating Plague", "AncientWorldEra",
                "A plague has spread across the land. How should people respond?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on prayers and rituals, neglecting other needs", new List<string> { "REL -1" }),
            new QuestAnswer("Quarantine the sick, risking social unrest", new List<string> { "SOC -1" })
                }
            ));

            quests.Add(new Quest(
                "The Great Pyramid", "AncientWorldEra",
                "Should we build a massive monument for the leaders or use resources elsewhere?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Build the pyramid to honor the leader", new List<string> { "CUL +1" }),
            new QuestAnswer("Use resources for public works", new List<string> { "MOT +1" })
                }
            ));

            quests.Add(new Quest(
                "The First Code of Laws", "AncientWorldEra",
                "A code of laws is being proposed. What should it focus on?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on religious morality", new List<string> { "REL +1" }),
            new QuestAnswer("Focus on equality for all", new List<string> { "SOC +1" })
                }
            ));
        }
        else if (era.eraName.Contains("Antiquity"))
        {
            quests.Add(new Quest(
                "The Rise of Democracy", "AntiquityEra",
                "A new form of government is emerging. How should the power be distributed?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Give power to the people through voting", new List<string> { "SOC +1" }), // ���������� ������ ����� ����������
            new QuestAnswer("Concentrate power in the hands of a few elites", new List<string> { "CUL +1" }) // �������� ����� ������� �����
                }
            ));

            quests.Add(new Quest(
                "The First Empire", "AntiquityEra",
                "A powerful leader is uniting the tribes. What should be their primary goal?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Expand through conquest", new List<string> { "STR +1" }), // ���� ����� ����������
            new QuestAnswer("Consolidate power and focus on internal stability", new List<string> { "END +1" }), // ������������ ����� ����������� ������������
            new QuestAnswer("Promote diplomacy and alliances", new List<string> { "SOC +1" }) // ���������� ������ ����� ����������
                }
            ));

            quests.Add(new Quest(
                "The Invention of Iron", "AntiquityEra",
                "A new technology has emerged: Iron. How should it be used?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Use it for weapons and defense", new List<string> { "STR +1" }), // ���� ����� ������
            new QuestAnswer("Use it for tools and farming", new List<string> { "CRE +1" }), // ������������ ����� �������� ������������
            new QuestAnswer("Use it for scientific research", new List<string> { "SCI +1" }),
            new QuestAnswer("Use it for building structures", new List<string> { "CUL +1" }) // �������� ����� �����������
                }
            ));

            quests.Add(new Quest(
                "The Development of Philosophy", "AntiquityEra",
                "Philosophers are questioning the nature of the world. What should be the focus?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on ethics and morality", new List<string> { "LOG +1" }), // ������ ����� ��������� �������
            new QuestAnswer("Focus on the study of the natural world", new List<string> { "SCI +1" }) // ����� ����� ��������������
                }
            ));

            quests.Add(new Quest(
                "Overextension of the Empire", "AntiquityEra",
                "The empire is expanding too fast. What are the consequences?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Weak leadership causes internal strife", new List<string> { "MOT -1" }), // ������ ���������
            new QuestAnswer("Increased demands cause fatigue in citizens", new List<string> { "END -1" }) // ������ ������������
                }
            ));

            quests.Add(new Quest(
                "The Spread of Trade", "AntiquityEra",
                "Trade routes are expanding across the world. What should be prioritized?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Increase trade with neighboring civilizations", new List<string> { "SOC +1" }), // ���������� ������ ����� ��������
            new QuestAnswer("Establish new trade routes with distant lands", new List<string> { "LRN +1" }) // ����������� ����� ����� ��������
                }
            ));

            quests.Add(new Quest(
                "The Decline of City-States", "AntiquityEra",
                "The city-state model is beginning to decline. What should be done?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Form larger kingdoms or empires", new List<string> { "STR +1" }), // ���� ����� �����������
            new QuestAnswer("Focus on creating strong local communities", new List<string> { "SOC +1" }) // ���������� ������ ����� ��������� ����������
                }
            ));

            quests.Add(new Quest(
                "The Beginning of Science", "AntiquityEra",
                "A new era of scientific inquiry is beginning. What should be the primary focus?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on astronomy and the study of the stars", new List<string> { "SCI +1" }), // ����� ����� ����������
            new QuestAnswer("Focus on medicine and improving healthcare", new List<string> { "LOG +1" }), // ������ ����� ����������� ������������
            new QuestAnswer("Focus on mathematics and geometry", new List<string> { "LRN +1" }) // ����������� ����� ����������
                }
            ));

            quests.Add(new Quest(
                "The First Legal Code", "AntiquityEra",
                "A set of laws is being proposed. What should it emphasize?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Ensure fairness and equality for all", new List<string> { "SOC +1" }), // ���������� ������ ����� ��������������
            new QuestAnswer("Focus on protecting religious rights", new List<string> { "REL +1" }) // ������� ����� ������
                }
            ));
        }
        else if (era.eraName.Contains("Middle"))
        {
            quests.Add(new Quest(
                "The Rise of Feudalism", "MiddleAgesEra",
                "Feudalism is spreading throughout the land. How should the system be structured?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Give power to a single monarch", new List<string> { "STR +1" }), // ���� ����� ���������������� ������
            new QuestAnswer("Divide power among local lords", new List<string> { "SOC +1" }) // ���������� ������ ����� ������������� ������
                }
            ));

            quests.Add(new Quest(
                "The Black Death", "MiddleAgesEra",
                "A plague is sweeping across the land. How should people respond?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Quarantine and isolate the infected", new List<string> { "SOC +1" }), // ���������� ������ ����� �������� �������
            new QuestAnswer("Focus on finding a cure", new List<string> { "SCI +1" }) // ����� ����� ����� ���������
                }
            ));

            quests.Add(new Quest(
                "The Rise of the Catholic Church", "MiddleAgesEra",
                "The Catholic Church is gaining immense power. What should its role be?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Maintain the power of the church and its influence", new List<string> { "REL +1" }), // ������� ����� ���������� ������� ������
            new QuestAnswer("Separate religion from state affairs", new List<string> { "CUL +1" }), // �������� ����� ��������� ������� �� �����������
            new QuestAnswer("Use the church to unify the population", new List<string> { "SOC +1" }) // ���������� ������ ����� ����������� ������
                }
            ));

            quests.Add(new Quest(
                "The Development of Castles", "MiddleAgesEra",
                "Castles are being built to defend against invaders. What should their primary purpose be?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on defense and military strategy", new List<string> { "STR +1" }), // ���� ����� �������
            new QuestAnswer("Use them as symbols of power and authority", new List<string> { "CUL +1" }), // �������� ����� ������ ������
            new QuestAnswer("Create centers of culture and learning within them", new List<string> { "CRE +1" }) // ������������ ����� �������� ���������� �������
                }
            ));

            quests.Add(new Quest(
                "The Magna Carta", "MiddleAgesEra",
                "A new legal document is being proposed to limit the king's power. Should humankind support it?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Support it to ensure justice and rights", new List<string> { "SOC +1" }), // ���������� ������ ����� ��������������
            new QuestAnswer("Reject it to maintain the monarch's power", new List<string> { "STR +1" }) // ���� ����� ���������� ������ �������
                }
            ));

            quests.Add(new Quest(
                "The Rise of Guilds", "MiddleAgesEra",
                "Craftsmen and traders are forming guilds. What should their role be?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Encourage the formation of guilds to protect workers", new List<string> { "SOC +1" }), // ���������� ������ ����� ������ �������
            new QuestAnswer("Ban the guilds to ensure control by the state", new List<string> { "STR +1" }) // ���� ����� ������ �������
                }
            ));


            quests.Add(new Quest(
                "The Rise of Guilds", "MiddleAgesEra",
                "Craftsmen and traders are forming guilds. What should their role be?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Encourage the formation of guilds to protect workers", new List<string> { "SOC +1" }), // ���������� ������ ����� ������ �������
        new QuestAnswer("Ban the guilds to ensure control by the state", new List<string> { "STR +1" }) // ���� ����� ������ �������
                }
            ));

            quests.Add(new Quest(
                "The Invention of the Printing Press", "MiddleAgesEra",
                "A new printing press has been invented. What should people do with it?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Use it to spread religious texts", new List<string> { "REL +1" }), // ������� ����� ��������������� �������
        new QuestAnswer("Use it to spread knowledge and education", new List<string> { "SCI +1" }), // ����� ����� �����������
        new QuestAnswer("Control its use to maintain political power", new List<string> { "SOC -1" }) // ������������� ������ �� ���������� ������ ����� �������� ����������
                }
            ));
        }
        else if (era.eraName.Contains("Middle"))
        {
            quests.Add(new Quest(
            "The Age of Exploration", "RenaissanceEra",
            "The world is being mapped out and new lands are being discovered. What should humankind's focus be?",
            new List<QuestAnswer>
            {
        new QuestAnswer("Focus on colonizing new lands", new List<string> { "STR +1" }), // ����
        new QuestAnswer("Focus on cultural exchange with the new world", new List<string> { "CUL +1" }) // ��������
            }
        ));

            quests.Add(new Quest(
                "The Printing Revolution", "RenaissanceEra",
                "Printing technology has advanced. What should be printed?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Print religious texts to spread faith", new List<string> { "REL +1" }), // �������
        new QuestAnswer("Print scientific works to spread knowledge", new List<string> { "SCI +1" }) // �����
                }
            ));

            quests.Add(new Quest(
                "The Birth of Modern Art", "RenaissanceEra",
                "Art is flourishing. What should be the focus of the artists?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Focus on realistic portrayals of the human form", new List<string> { "CRE +1" }), // ������������
        new QuestAnswer("Focus on abstract and symbolic representations", new List<string> { "LOG +1" }) // ������
                }
            ));

            quests.Add(new Quest(
                "The Scientific Revolution", "RenaissanceEra",
                "New discoveries in science are challenging old beliefs. How should people proceed?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Support scientific discoveries to advance knowledge", new List<string> { "SCI +1" }), // �����
        new QuestAnswer("Maintain traditional beliefs to preserve order", new List<string> { "SOC -1" }), // ���������� ������ (���������)
        new QuestAnswer("Promote the use of science in practical inventions", new List<string> { "CRE +1" }) // ������������
                }
            ));

            quests.Add(new Quest(
                "The Invention of the Telescope", "RenaissanceEra",
                "A new telescope has been invented, expanding people's view of the universe. How should they use it?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Use it to explore the stars and expand our knowledge", new List<string> { "SCI +1" }), // �����
        new QuestAnswer("Use it for military surveillance and defense", new List<string> { "END -1" }) // ������������ (���������)
                }
            ));

            quests.Add(new Quest(
                "The Rise of Banking", "RenaissanceEra",
                "Banking is becoming more centralized. How should people handle this new economic system?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Encourage the development of banks to boost trade", new List<string> { "SOC +1" }), // ���������� ������
        new QuestAnswer("Limit the power of banks to prevent corruption", new List<string> { "LOG +1" }), // ������
        new QuestAnswer("Use banks to fund expansion and growth", new List<string> { "MOT +1" }) // ���������
                }
            ));

            quests.Add(new Quest(
                "The Protestant Reformation", "RenaissanceEra",
                "A religious movement is challenging the authority of the Church. Should people support it?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Support the Reformation to empower individual faith", new List<string> { "REL +1" }), // �������
        new QuestAnswer("Oppose the Reformation to maintain the Church's power", new List<string> { "SOC -1" }), // ���������� ������ (���������)
        new QuestAnswer("Remain neutral and focus on internal development", new List<string> { "LOG +1" }) // ������
                }
            ));

            quests.Add(new Quest(
                "The Impact of Disease", "RenaissanceEra",
                "A deadly disease is spreading across the region. How should humankind respond?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Quarantine affected areas and focus on containment", new List<string> { "SOC -1" }), // ���������� ������ (���������)
        new QuestAnswer("Focus on finding a cure through scientific research", new List<string> { "SCI +1" }), // �����
        new QuestAnswer("Use the disease as an opportunity to weaken rivals", new List<string> { "STR -1" }) // ���� (���������)
                }
            ));
        }
        else if (era.eraName.Contains("Middle"))
        {
            quests.Add(new Quest(
            "The Industrial Revolution", "ModernEra",
            "New machines are being invented. How should people use this newfound technology?",
            new List<QuestAnswer>
            {
        new QuestAnswer("Focus on factory production for economic growth", new List<string> { "SOC +1" }), // ���������� ������
        new QuestAnswer("Focus on improving living standards for workers", new List<string> { "EMO +1" }), // ������������� ������������
        new QuestAnswer("Focus on developing military technologies for power", new List<string> { "STR -1" }) // ����
            }
        ));

            quests.Add(new Quest(
                "The Rise of Nationalism", "ModernEra",
                "Nationalism is growing across the globe. How should people approach it?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Promote unity and national pride", new List<string> { "SOC +1" }), // ���������� ������
        new QuestAnswer("Support independence movements for minorities", new List<string> { "LOG +1" }), // ������
        new QuestAnswer("Oppose nationalism to maintain stability", new List<string> { "END +1" }) // ������������
                }
            ));

            quests.Add(new Quest(
                "The Advent of Electricity", "ModernEra",
                "Electricity is becoming widespread. How should humankind use it?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Focus on powering industries for mass production", new List<string> { "LRN +1" }), // �����������
        new QuestAnswer("Focus on improving everyday life with electric lighting", new List<string> { "SOC +1" }), // ���������� ������
        new QuestAnswer("Focus on developing electrical weapons for defense", new List<string> { "STR +1" }) // ����
                }
            ));

            quests.Add(new Quest(
                "The Development of Mass Media", "ModernEra",
                "Newspapers and radio are spreading information rapidly. What should be people's focus?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Use it to spread political ideologies", new List<string> { "LOG +1" }), // ������
        new QuestAnswer("Use it to promote cultural values and art", new List<string> { "CUL +1" }), // ��������
        new QuestAnswer("Use it to manipulate public opinion for control", new List<string> { "SOC -1" }) // ���������� ������
                }
            ));

            // Rise of Democracy
            quests.Add(new Quest(
                "The Rise of Democracy", "ModernEra",
                "Democracy is spreading globally. How should people approach it?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Support the spread of democratic values", new List<string> { "SOC +1" }), // ���������� ������
                    new QuestAnswer("Maintain strong control to preserve order", new List<string> { "LOG +1" }), // ������
                    new QuestAnswer("Oppose democracy to preserve stability", new List<string> { "STR +1" }) // ������������� ������������
                }
            ));

            // Space Race
            quests.Add(new Quest(
                "The Space Race", "ModernEra",
                "The race to space is on. How should people participate?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Invest in space exploration for scientific progress", new List<string> { "SCI +1" }), // �����
        new QuestAnswer("Focus on military use of space technologies", new List<string> { "STR +1" }), // ����
        new QuestAnswer("Focus on space tourism to boost economy", new List<string> { "LRN -1" }) // �����������
                }
            ));

            // Internet Revolution
            quests.Add(new Quest(
                "The Internet Revolution", "ModernEra",
                "The internet is changing the world. How should people harness its power?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Focus on global communication and information sharing", new List<string> { "SOC +1" }), // ���������� ������
        new QuestAnswer("Focus on using the internet to boost economic growth", new List<string> { "LRN +1" }), // �����������
        new QuestAnswer("Use it for surveillance and mass control", new List<string> { "LOG -1" }) // ������
                }
            ));

            // Green Revolution
            quests.Add(new Quest(
                "The Green Revolution", "ModernEra",
                "New agricultural techniques are emerging. How should people use them?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Use it to increase food production for a growing population", new List<string> { "CRE +1" }), // ������������
        new QuestAnswer("Focus on sustainable agriculture to preserve the environment", new List<string> { "CUL +1" }), // ��������
        new QuestAnswer("Use it to control food distribution and stabilize the economy", new List<string> { "LOG -1" }) // ������
                }
            ));

            // Nuclear Age
            quests.Add(new Quest(
                "The Nuclear Age", "ModernEra",
                "Nuclear technology has been harnessed. How should it be used?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Develop nuclear weapons for defense", new List<string> { "STR +1" }), // ����
        new QuestAnswer("Use nuclear energy to power industries and homes", new List<string> { "LRN +1" }), // �����������
        new QuestAnswer("Focus on peaceful nuclear technology for scientific research", new List<string> { "SCI +1" }) // �����
                }
            ));
        }
        else if (era.eraName.Contains("Our"))
        {
            // Global Climate Change
            quests.Add(new Quest(
                "Global Climate Change", "OurTimeEra",
                "Climate change is becoming a global issue. How should humankind respond?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Implement green technologies to reduce emissions", new List<string> { "CUL +1" }), // ��������
            new QuestAnswer("Focus on adaptation and infrastructure to withstand climate change", new List<string> { "SOC +1" }), // ���������� ������
            new QuestAnswer("Prioritize economic growth over climate concerns", new List<string> { "SCI -1" }) // �����
                }
            ));

            // Artificial Intelligence
            quests.Add(new Quest(
                "Artificial Intelligence", "OurTimeEra",
                "Artificial Intelligence is advancing rapidly. How should people integrate it into society?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on AI for economic growth and efficiency", new List<string> { "LOG +1" }), // ������
            new QuestAnswer("Create memes and +18 content", new List<string> { "SCI -5" }),
            new QuestAnswer("Ban AI development to prevent societal disruption", new List<string> { "END -1" }) // ������������
                }
            ));

            // Space Colonization
            quests.Add(new Quest(
                "Space Colonization", "OurTimeEra",
                "Humankind is looking to colonize other planets. How should people approach it?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Invest heavily in space exploration to secure our future", new List<string> { "SCI +1" }), // �����
            new QuestAnswer("Focus on colonizing Mars and other planets to expand humanity", new List<string> { "STR +1" }), // ����
            new QuestAnswer("Improve sustainability on Earth before venturing into space", new List<string> { "LOG +1" }) // ��������
                }
            ));

            // Universal Basic Income
            quests.Add(new Quest(
                "Universal Basic Income", "OurTimeEra",
                "The idea of universal basic income is being debated. What should humankind do?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Implement universal basic income to reduce inequality", new List<string> { "SOC +1" }) // ���������� ������
                }
            ));

            // Globalization vs Nationalism
            quests.Add(new Quest(
                "Globalization vs Nationalism", "OurTimeEra",
                "The world is becoming more interconnected. Should people embrace globalization or focus on nationalism?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Support nationalism to protect cultural and economic identity", new List<string> { "CUL +1" }), // ��������
            new QuestAnswer("Focus on a balanced approach that encourages both", new List<string> { "LOG +1" }) // ������
                }
            ));

            // Healthcare for All
            quests.Add(new Quest(
                "Healthcare for All", "OurTimeEra",
                "Healthcare access is a critical issue. How should humankind ensure it for everyone?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Implement universal healthcare systems globally", new List<string> { "SOC +1" }), // ���������� ������
            new QuestAnswer("Focus on privatizing healthcare to increase efficiency", new List<string> { "LOG +1" }) // ������
                }
            ));

            // Sustainable Energy
            quests.Add(new Quest(
                "Sustainable Energy", "OurTimeEra",
                "The need for sustainable energy sources has never been greater. What should people's our focus?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Invest in renewable energy sources like solar and wind", new List<string> { "CUL +1" }), // ��������
            new QuestAnswer("Focus on nuclear energy as a clean and powerful option", new List<string> { "SCI +1" }), // �����
            new QuestAnswer("Promote energy conservation and efficiency measures", new List<string> { "SOC +1" }) // ���������� ������
                }
            ));

            // Digital Privacy
            quests.Add(new Quest(
                "Digital Privacy", "OurTimeEra",
                "Digital privacy is being threatened in many parts of the world. How should people protect individuals?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Invest in technology to protect privacy through encryption", new List<string> { "LOG +1" }), // ������
            new QuestAnswer("Focus on educating the public on privacy and cybersecurity", new List<string> { "SOC +1" }) // ���������� ������
                }
            ));

            // Technological Unemployment
            quests.Add(new Quest(
                "Technological Unemployment", "OurTimeEra",
                "Automation and AI are threatening jobs across the globe. What should humankind do?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Implement policies for job retraining and upskilling", new List<string> { "SOC +1" }), // ���������� ������
            new QuestAnswer("Support businesses in transitioning to automation while protecting workers", new List<string> { "LOG +1" }) // ������
                }
            ));

            // The Future of Education
            quests.Add(new Quest(
                "The Future of Education", "OurTimeEra",
                "Education is evolving rapidly. How should people prepare for the future?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Implement online learning and virtual classrooms worldwide", new List<string> { "SOC +1" }), // ���������� ������
            new QuestAnswer("Focus on STEM education to drive future technological growth", new List<string> { "SCI +1" }), // �����
            new QuestAnswer("Promote alternative education models that foster creativity and critical thinking", new List<string> { "CRE +1" }) // ������������
                }
            ));
        }

            return quests;
    }

}
