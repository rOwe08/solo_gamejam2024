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
                    new QuestAnswer("Prioritize strength", "STR +1"),
                    new QuestAnswer("Focus on creativity", "CRE +1" ),
                    new QuestAnswer("Focus on survival", "END +1" )
                }
            ));

            quests.Add(new Quest(
                "Discovery of Fire", "PrimitiveSocietyEra",
                "The discovery of fire will change humanity's future. What should it be used for?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Warmth and cooking", "COM +1"),
                    new QuestAnswer("Weaponry and defense", "DEF +1"),
                    new QuestAnswer("Rituals and community bonding", "SOC +1")
                }
            ));

            quests.Add(new Quest(
                "The First Shelter", "PrimitiveSocietyEra",
                "Humans are learning to build shelters. What is the best way to design them?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Build compact and sturdy shelters", "SUR +1"),
                    new QuestAnswer("Build large communal shelters", "SOC +1"),
                    new QuestAnswer("Build temporary shelters for mobility", "EXP +1")
                }
            ));

            quests.Add(new Quest(
                "Hunting vs. Farming", "PrimitiveSocietyEra",
                "Should humanity prioritize hunting or farming as their primary food source?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Focus on hunting for meat", "HUN +1"),
                    new QuestAnswer("Focus on farming for stable food", "AGRI +1"),
                    new QuestAnswer("Focus on a balanced approach", "RES +1")
                }
            ));

            quests.Add(new Quest(
                "The First Language", "PrimitiveSocietyEra",
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
            quests.Add(new Quest(
                "The Birth of Writing", "AncientWorldEra",
                "A system of writing is emerging. How should it be used?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("For trade and commerce", "ECO +1"),
                    new QuestAnswer("For religious texts", "REL +1"),
                    new QuestAnswer("For historical record-keeping", "KNO +1")
                }
            ));

            quests.Add(new Quest(
                "The First War", "AncientWorldEra",
                "The first war has broken out. Should we focus on offense or defense?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Focus on offense to conquer", "MIL +1"),
                    new QuestAnswer("Focus on defense to protect", "DEF +1"),
                    new QuestAnswer("Focus on diplomacy to avoid war", "REL +1")
                }
            ));

            quests.Add(new Quest(
                "The Rise of Monarchy", "AncientWorldEra",
                "A monarchy is being established. How should the leader be chosen?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Through divine right", "REL +1"),
                    new QuestAnswer("Through election by the people", "DEM +1"),
                    new QuestAnswer("Through hereditary monarchy", "NOB +1")
                }
            ));

            quests.Add(new Quest(
                "The Great Pyramid", "AncientWorldEra",
                "Should we build a massive monument for the pharaoh or use resources elsewhere?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Build the pyramid to honor the pharaoh", "REL +1"),
                    new QuestAnswer("Use resources for public works", "HAP +1"),
                    new QuestAnswer("Focus on military development", "MIL +1")
                }
            ));

            quests.Add(new Quest(
                "The First Code of Laws", "AncientWorldEra",
                "A code of laws is being proposed. What should it focus on?",
                new List<QuestAnswer>
                {
                    new QuestAnswer("Focus on property rights", "ECO +1"),
                    new QuestAnswer("Focus on religious morality", "REL +1"),
                    new QuestAnswer("Focus on equality for all", "SOC +1")
                }
            ));
        }
        // Antiquity
        else if (era.eraName.Contains("Antiquity"))
        {
            // Antiquity Era
            quests.Add(new Quest(
                "The Rise of Democracy", "AntiquityEra",
                "A new form of government is emerging. How should the power be distributed?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Give power to the people through voting", "DEM +1"),
            new QuestAnswer("Concentrate power in the hands of a few elites", "NOB +1")
                }
            ));

            quests.Add(new Quest(
                "The First Empire", "AntiquityEra",
                "A powerful leader is uniting the tribes. What should be their primary goal?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Expand through conquest", "MIL +1"),
            new QuestAnswer("Consolidate power and focus on internal stability", "ECO +1"),
            new QuestAnswer("Promote diplomacy and alliances", "REL +1")
                }
            ));

            quests.Add(new Quest(
                "The Invention of Iron", "AntiquityEra",
                "A new technology has emerged: Iron. How should it be used?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Use it for weapons and defense", "MIL +1"),
            new QuestAnswer("Use it for tools and farming", "ECO +1"),
            new QuestAnswer("Use it for building structures", "SOC +1")
                }
            ));

            quests.Add(new Quest(
                "The Development of Philosophy", "AntiquityEra",
                "Philosophers are questioning the nature of the world. What should be the focus?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on ethics and morality", "INT +1"),
            new QuestAnswer("Focus on the study of the natural world", "KNO +1")
                }
            ));

            quests.Add(new Quest(
                "The Spread of Trade", "AntiquityEra",
                "Trade routes are expanding across the world. What should be prioritized?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Increase trade with neighboring civilizations", "ECO +1"),
            new QuestAnswer("Establish new trade routes with distant lands", "EXP +1"),
            new QuestAnswer("Create a centralized marketplace for all goods", "SOC +1")
                }
            ));

            quests.Add(new Quest(
                "The Decline of City-States", "AntiquityEra",
                "The city-state model is beginning to decline. What should be done?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Form larger kingdoms or empires", "MIL +1"),
            new QuestAnswer("Focus on creating strong local communities", "SOC +1")
                }
            ));

            quests.Add(new Quest(
                "The Beginning of Science", "AntiquityEra",
                "A new era of scientific inquiry is beginning. What should be the primary focus?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on astronomy and the study of the stars", "KNO +1"),
            new QuestAnswer("Focus on medicine and improving healthcare", "ECO +1"),
            new QuestAnswer("Focus on mathematics and geometry", "INT +1")
                }
            ));

            quests.Add(new Quest(
                "The First Legal Code", "AntiquityEra",
                "A set of laws is being proposed. What should it emphasize?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Ensure fairness and equality for all", "SOC +1"),
            new QuestAnswer("Protect the wealth and power of the elite", "ECO +1")
                }
            ));
        }
        else if (era.eraName.Contains("Middle"))
        {
            // Middle Ages Era
            quests.Add(new Quest(
                "The Rise of Feudalism", "MiddleAgesEra",
                "Feudalism is spreading throughout the land. How should the system be structured?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Give power to a single monarch", "MIL +1"),
            new QuestAnswer("Divide power among local lords", "SOC +1"),
            new QuestAnswer("Establish a centralized bureaucracy", "ECO +1")
                }
            ));

            quests.Add(new Quest(
                "The Crusades", "MiddleAgesEra",
                "A religious war is being proposed. Should we participate?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Join the crusades for religious glory", "REL +1"),
            new QuestAnswer("Stay neutral and focus on internal growth", "ECO +1"),
            new QuestAnswer("Defend against the invaders", "MIL +1")
                }
            ));

            quests.Add(new Quest(
                "The Black Death", "MiddleAgesEra",
                "A plague is sweeping across the land. How should we respond?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Quarantine and isolate the infected", "ECO +1"),
            new QuestAnswer("Focus on finding a cure", "SOC +1"),
            new QuestAnswer("Relocate to new regions", "EXP +1")
                }
            ));

            quests.Add(new Quest(
                "The Rise of the Catholic Church", "MiddleAgesEra",
                "The Catholic Church is gaining immense power. What should its role be?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Maintain the power of the church and its influence", "REL +1"),
            new QuestAnswer("Separate religion from state affairs", "DEM +1"),
            new QuestAnswer("Use the church to unify the population", "SOC +1")
                }
            ));

            quests.Add(new Quest(
                "The Development of Castles", "MiddleAgesEra",
                "Castles are being built to defend against invaders. What should their primary purpose be?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on defense and military strategy", "MIL +1"),
            new QuestAnswer("Use them as symbols of power and authority", "NOB +1"),
            new QuestAnswer("Create centers of culture and learning within them", "KNO +1")
                }
            ));

            quests.Add(new Quest(
                "The Magna Carta", "MiddleAgesEra",
                "A new legal document is being proposed to limit the king's power. Should we support it?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Support it to ensure justice and rights", "SOC +1"),
            new QuestAnswer("Reject it to maintain the monarch's power", "MIL +1")
                }
            ));

            quests.Add(new Quest(
                "The Rise of Guilds", "MiddleAgesEra",
                "Craftsmen and traders are forming guilds. What should their role be?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Encourage the formation of guilds to protect workers", "ECO +1"),
            new QuestAnswer("Ban the guilds to ensure control by the state", "MIL +1"),
            new QuestAnswer("Promote the guilds as a means to control trade", "SOC +1")
                }
            ));

            quests.Add(new Quest(
                "The Invention of the Printing Press", "MiddleAgesEra",
                "A new printing press has been invented. What should we do with it?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Use it to spread religious texts", "REL +1"),
            new QuestAnswer("Use it to spread knowledge and education", "KNO +1"),
            new QuestAnswer("Control its use to maintain political power", "MIL +1")
                }
            ));
        }
        else if (era.eraName.Contains("Renaissance"))
        {
            // Renaissance Era
            quests.Add(new Quest(
                "The Age of Exploration", "RenaissanceEra",
                "The world is being mapped out and new lands are being discovered. What should our focus be?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on colonizing new lands", "EXP +1"),
            new QuestAnswer("Focus on trade routes and wealth", "ECO +1"),
            new QuestAnswer("Focus on cultural exchange with the new world", "SOC +1")
                }
            ));

            quests.Add(new Quest(
                "The Printing Revolution", "RenaissanceEra",
                "Printing technology has advanced. What should be printed?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Print religious texts to spread faith", "REL +1"),
            new QuestAnswer("Print scientific works to spread knowledge", "KNO +1"),
            new QuestAnswer("Print literature and art to spread culture", "SOC +1")
                }
            ));

            quests.Add(new Quest(
                "The Rise of Humanism", "RenaissanceEra",
                "Humanism is gaining traction. How should we incorporate it into our society?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Promote the importance of individual achievements", "SOC +1"),
            new QuestAnswer("Emphasize the study of classical texts and philosophy", "KNO +1"),
            new QuestAnswer("Focus on the development of the arts and sciences", "INT +1")
                }
            ));

            quests.Add(new Quest(
                "The Birth of Modern Art", "RenaissanceEra",
                "Art is flourishing. What should be the focus of our artists?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on realistic portrayals of the human form", "ART +1"),
            new QuestAnswer("Focus on religious themes to inspire faith", "REL +1"),
            new QuestAnswer("Focus on abstract and symbolic representations", "KNO +1")
                }
            ));

            quests.Add(new Quest(
                "The Scientific Revolution", "RenaissanceEra",
                "New discoveries in science are challenging old beliefs. How should we proceed?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Support scientific discoveries to advance knowledge", "INT +1"),
            new QuestAnswer("Maintain traditional beliefs to preserve order", "SOC +1"),
            new QuestAnswer("Promote the use of science in practical inventions", "ECO +1")
                }
            ));

            quests.Add(new Quest(
                "The Invention of the Telescope", "RenaissanceEra",
                "A new telescope has been invented, expanding our view of the universe. How should we use it?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Use it to explore the stars and expand our knowledge", "KNO +1"),
            new QuestAnswer("Use it to improve navigation and exploration", "EXP +1"),
            new QuestAnswer("Use it for military surveillance and defense", "MIL +1")
                }
            ));

            quests.Add(new Quest(
                "The Rise of Banking", "RenaissanceEra",
                "Banking is becoming more centralized. How should we handle this new economic system?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Encourage the development of banks to boost trade", "ECO +1"),
            new QuestAnswer("Limit the power of banks to prevent corruption", "SOC +1"),
            new QuestAnswer("Use banks to fund exploration and expansion", "EXP +1")
                }
            ));

            quests.Add(new Quest(
                "The Protestant Reformation", "RenaissanceEra",
                "A religious movement is challenging the authority of the Church. Should we support it?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Support the Reformation to empower individual faith", "REL +1"),
            new QuestAnswer("Oppose the Reformation to maintain the Church's power", "SOC +1"),
            new QuestAnswer("Remain neutral and focus on internal development", "ECO +1")
                }
            ));

            quests.Add(new Quest(
                "The Fall of Constantinople", "RenaissanceEra",
                "The great city of Constantinople has fallen. How should we respond?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on rebuilding the city and maintaining order", "SOC +1"),
            new QuestAnswer("Use the opportunity to expand our own empire", "MIL +1"),
            new QuestAnswer("Seek alliances to counter the new power in the region", "DEF +1")
                }
            ));
        }
        else if (era.eraName.Contains("Modern"))
        {
            // Modern Era
            quests.Add(new Quest(
                "The Industrial Revolution", "ModernEra",
                "New machines are being invented. How should we use this newfound technology?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on factory production for economic growth", "ECO +1"),
            new QuestAnswer("Focus on improving living standards for workers", "SOC +1"),
            new QuestAnswer("Focus on military applications to gain power", "MIL +1")
                }
            ));

            quests.Add(new Quest(
                "The Rise of Nationalism", "ModernEra",
                "Nationalism is growing across the globe. How should we approach it?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Promote unity and national pride", "SOC +1"),
            new QuestAnswer("Support independence movements for minorities", "POL +1"),
            new QuestAnswer("Oppose nationalism to maintain stability", "DEF +1")
                }
            ));

            quests.Add(new Quest(
                "The Advent of Electricity", "ModernEra",
                "Electricity is becoming widespread. How should we use it?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on powering industries for mass production", "ECO +1"),
            new QuestAnswer("Focus on improving everyday life with electric lighting", "SOC +1"),
            new QuestAnswer("Focus on developing electrical weapons for defense", "MIL +1")
                }
            ));

            quests.Add(new Quest(
                "The Development of Mass Media", "ModernEra",
                "Newspapers and radio are spreading information rapidly. What should be our focus?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Use it to spread political ideologies", "POL +1"),
            new QuestAnswer("Use it to promote cultural values and art", "SOC +1"),
            new QuestAnswer("Use it to promote technological advancements", "ECO +1")
                }
            ));

            quests.Add(new Quest(
                "The First World War", "ModernEra",
                "A great war has erupted. Should we focus on offense or defense?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on offense to conquer territories", "MIL +1"),
            new QuestAnswer("Focus on defense to protect our borders", "DEF +1"),
            new QuestAnswer("Focus on diplomacy to avoid the conflict", "REL +1")
                }
            ));

            quests.Add(new Quest(
                "The Rise of Democracy", "ModernEra",
                "Democracy is spreading globally. How should we approach it?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Support the spread of democratic values", "DEM +1"),
            new QuestAnswer("Maintain strong control to preserve order", "POL +1"),
            new QuestAnswer("Focus on reforms to balance democracy and stability", "SOC +1")
                }
            ));

            quests.Add(new Quest(
                "The Space Race", "ModernEra",
                "The race to space is on. How should we participate?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Invest in space exploration for scientific progress", "KNO +1"),
            new QuestAnswer("Focus on military use of space technologies", "MIL +1"),
            new QuestAnswer("Focus on space tourism to boost economy", "ECO +1")
                }
            ));

            quests.Add(new Quest(
                "The Internet Revolution", "ModernEra",
                "The internet is changing the world. How should we harness its power?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on global communication and information sharing", "SOC +1"),
            new QuestAnswer("Focus on using the internet to boost economic growth", "ECO +1"),
            new QuestAnswer("Focus on using it for surveillance and control", "DEF +1")
                }
            ));

            quests.Add(new Quest(
                "The Green Revolution", "ModernEra",
                "New agricultural techniques are emerging. How should we use them?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Use it to increase food production for a growing population", "AGRI +1"),
            new QuestAnswer("Focus on sustainable agriculture to preserve the environment", "ENV +1"),
            new QuestAnswer("Use it to control food distribution and stabilize the economy", "ECO +1")
                }
            ));

            quests.Add(new Quest(
                "The Nuclear Age", "ModernEra",
                "Nuclear technology has been harnessed. How should it be used?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Develop nuclear weapons for defense", "MIL +1"),
            new QuestAnswer("Use nuclear energy to power industries and homes", "ECO +1"),
            new QuestAnswer("Focus on peaceful nuclear technology for scientific research", "KNO +1")
                }
            ));
        }
        else if (era.eraName.Contains("Our"))
        {
            // Our Time Era
            quests.Add(new Quest(
                "Global Climate Change", "OurTimeEra",
                "Climate change is becoming a global issue. How should we respond?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Implement green technologies to reduce emissions", "ENV +1"),
            new QuestAnswer("Focus on adaptation and infrastructure to withstand climate change", "SOC +1"),
            new QuestAnswer("Take aggressive actions to halt industrial growth", "ECO +1")
                }
            ));

            quests.Add(new Quest(
                "Artificial Intelligence", "OurTimeEra",
                "Artificial Intelligence is advancing rapidly. How should we integrate it into society?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on AI for economic growth and efficiency", "ECO +1"),
            new QuestAnswer("Implement AI to improve healthcare and quality of life", "SOC +1"),
            new QuestAnswer("Regulate AI development to avoid ethical concerns", "POL +1")
                }
            ));

            quests.Add(new Quest(
                "Space Colonization", "OurTimeEra",
                "Mankind is looking to colonize other planets. How should we approach it?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Invest heavily in space exploration to secure our future", "KNO +1"),
            new QuestAnswer("Focus on colonizing Mars and other planets to expand humanity", "MIL +1"),
            new QuestAnswer("Concentrate on improving Earth's sustainability instead", "ENV +1")
                }
            ));

            quests.Add(new Quest(
                "Universal Basic Income", "OurTimeEra",
                "The idea of universal basic income is being debated. What should we do?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Implement universal basic income to reduce inequality", "SOC +1"),
            new QuestAnswer("Focus on improving job opportunities instead of UBI", "ECO +1"),
            new QuestAnswer("Introduce UBI temporarily to test its impact", "POL +1")
                }
            ));

            quests.Add(new Quest(
                "Globalization vs Nationalism", "OurTimeEra",
                "The world is becoming more interconnected. Should we embrace globalization or focus on nationalism?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Embrace globalization to strengthen international ties", "POL +1"),
            new QuestAnswer("Support nationalism to protect cultural and economic identity", "SOC +1"),
            new QuestAnswer("Focus on a balanced approach that encourages both", "ECO +1")
                }
            ));

            quests.Add(new Quest(
                "Healthcare for All", "OurTimeEra",
                "Healthcare access is a critical issue. How should we ensure it for everyone?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Implement universal healthcare systems globally", "SOC +1"),
            new QuestAnswer("Focus on privatizing healthcare to increase efficiency", "ECO +1"),
            new QuestAnswer("Provide basic healthcare services while incentivizing private solutions", "POL +1")
                }
            ));

            quests.Add(new Quest(
                "Sustainable Energy", "OurTimeEra",
                "The need for sustainable energy sources has never been greater. What should be our focus?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Invest in renewable energy sources like solar and wind", "ENV +1"),
            new QuestAnswer("Focus on nuclear energy as a clean and powerful option", "ECO +1"),
            new QuestAnswer("Promote energy conservation and efficiency measures", "SOC +1")
                }
            ));

            quests.Add(new Quest(
                "Digital Privacy", "OurTimeEra",
                "Digital privacy is being threatened in many parts of the world. How should we protect individuals?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Implement stronger laws and regulations for data protection", "POL +1"),
            new QuestAnswer("Invest in technology to protect privacy through encryption", "ECO +1"),
            new QuestAnswer("Focus on educating the public on privacy and cybersecurity", "SOC +1")
                }
            ));

            quests.Add(new Quest(
                "Technological Unemployment", "OurTimeEra",
                "Automation and AI are threatening jobs across the globe. What should we do?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Implement policies for job retraining and upskilling", "SOC +1"),
            new QuestAnswer("Support businesses in transitioning to automation while protecting workers", "ECO +1"),
            new QuestAnswer("Focus on a global safety net for those displaced by automation", "POL +1")
                }
            ));

            quests.Add(new Quest(
                "The Future of Education", "OurTimeEra",
                "Education is evolving rapidly. How should we prepare for the future?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Implement online learning and virtual classrooms worldwide", "SOC +1"),
            new QuestAnswer("Focus on STEM education to drive future technological growth", "ECO +1"),
            new QuestAnswer("Promote alternative education models that foster creativity and critical thinking", "KNO +1")
                }
            ));
        }


        return quests;
    }

}
