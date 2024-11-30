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
                } // В этом квесте два ответа
            ));

            quests.Add(new Quest(
                "Discovery of Fire", "PrimitiveSocietyEra",
                "The discovery of fire will change humanity's future. What should it be used for?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Warmth and cooking", "END +1"), // Выносливость для выживания
            new QuestAnswer("Weaponry and defense", "STR +1"), // Сила для защиты
            new QuestAnswer("Rituals and community bonding", "SOC +1") // Социальные навыки для общества
                }
            ));

            quests.Add(new Quest(
                "The First Shelter", "PrimitiveSocietyEra",
                "Humans are learning to build shelters. What is the best way to design them?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Build compact and sturdy shelters", "END +1"),
            new QuestAnswer("Build temporary shelters for mobility", "AGI +1") // Ловкость для мобильности
                } // В этом квесте два ответа
            ));

            // Квест с отрицательными результатами
            quests.Add(new Quest(
                "A Drought Strikes", "PrimitiveSocietyEra",
                "A severe drought is threatening the community's food supply. What should be done?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on hunting, even if it's dangerous", "AGI -1"),
            new QuestAnswer("Conserve water, but risk dehydration", "END -1"),
                } // В этом квесте оба результата отрицательные
            ));

            quests.Add(new Quest(
                "Hunting vs. Farming", "PrimitiveSocietyEra",
                "Should humanity prioritize hunting or farming as their primary food source?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on hunting for meat", "STR +1"), // Сила для охоты
            new QuestAnswer("Focus on farming for stable food", "END +1"), // Выносливость для фермерства
                } // В этом квесте два ответа
            ));

            // Квест с негативным исходом
            quests.Add(new Quest(
                "Tribal Conflicts", "PrimitiveSocietyEra",
                "Tensions are rising between neighboring tribes. How should we respond?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Avoid conflict but weaken our position", "SOC -1"),
            new QuestAnswer("Prepare for war but lose trust", "EMO -1"),
            new QuestAnswer("Attempt peace but risk betrayal", "MOT -1")
                } // Все ответы приводят к отрицательным результатам
            ));

            quests.Add(new Quest(
                "The First Language", "PrimitiveSocietyEra",
                "Humans are starting to communicate. What form of language should they adopt?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Simple gestures and sounds", "SOC +1"), // Социальные навыки для общения
            new QuestAnswer("Develop complex spoken language", "LRN +1"), // Обучаемость для развития языка
                } // В этом квесте два ответа
            ));
        }
        else if (era.eraName.Contains("Ancient"))
        {
            quests.Add(new Quest(
                "The Birth of Writing", "AncientWorldEra",
                "A system of writing is emerging. How should it be used?",
                new List<QuestAnswer>
                {
            new QuestAnswer("For trade and commerce", "CUL +1"), // Культура, связанная с развитием торговли
            new QuestAnswer("For religious texts", "REL +1"), // Религия
            new QuestAnswer("For historical record-keeping", "LRN +1") // Обучаемость через хранение знаний
                }
            ));

            quests.Add(new Quest(
                "The First War", "AncientWorldEra",
                "The first war has broken out. Should we focus on offense or defense?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on offense to conquer", "STR +1"), // Сила для наступления
            new QuestAnswer("Focus on defense to protect", "END +1"), // Выносливость для защиты
            new QuestAnswer("Focus on diplomacy to avoid war", "SOC +1") // Социальные навыки для дипломатии
                }
            ));

            quests.Add(new Quest(
                "The Rise of Monarchy", "AncientWorldEra",
                "A monarchy is being established. How should the leader be chosen?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Through divine right", "REL +1"), // Религия для обоснования власти
            new QuestAnswer("Through election by the people", "SOC +1"), // Социальные навыки для выборов
                } // В этом квесте два варианта ответа
            ));

            // Квест с отрицательными результатами
            quests.Add(new Quest(
                "A Devastating Plague", "AncientWorldEra",
                "A plague has spread across the land. How should we respond?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on prayers and rituals, neglecting other needs", "REL -1"), // Потеря веры
            new QuestAnswer("Quarantine the sick, risking social unrest", "SOC -1") // Социальные последствия
                } // В этом квесте все ответы имеют негативные последствия
            ));

            quests.Add(new Quest(
                "The Great Pyramid", "AncientWorldEra",
                "Should we build a massive monument for the pharaoh or use resources elsewhere?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Build the pyramid to honor the pharaoh", "CUL +1"), // Культура через памятники
            new QuestAnswer("Use resources for public works", "MOT +1") // Мотивация через общественные проекты
                } // В этом квесте два ответа
            ));

            quests.Add(new Quest(
                "The First Code of Laws", "AncientWorldEra",
                "A code of laws is being proposed. What should it focus on?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on religious morality", "REL +1"), // Религиозная мораль
            new QuestAnswer("Focus on equality for all", "SOC +1") // Социальные навыки для равенства
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
            new QuestAnswer("Give power to the people through voting", "SOC +1"), // Социальные навыки через демократию
            new QuestAnswer("Concentrate power in the hands of a few elites", "CUL +1") // Культура через влияние элиты
                }
            ));

            quests.Add(new Quest(
                "The First Empire", "AntiquityEra",
                "A powerful leader is uniting the tribes. What should be their primary goal?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Expand through conquest", "STR +1"), // Сила через завоевания
            new QuestAnswer("Consolidate power and focus on internal stability", "END +1"), // Выносливость через поддержание стабильности
            new QuestAnswer("Promote diplomacy and alliances", "SOC +1") // Социальные навыки через дипломатию
                }
            ));

            quests.Add(new Quest(
                "The Invention of Iron", "AntiquityEra",
                "A new technology has emerged: Iron. How should it be used?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Use it for weapons and defense", "STR +1"), // Сила через оружие
            new QuestAnswer("Use it for tools and farming", "CRE +1"), // Креативность через создание инструментов
            new QuestAnswer("Use it for building structures", "CUL +1") // Культура через архитектуру
                }
            ));

            quests.Add(new Quest(
                "The Development of Philosophy", "AntiquityEra",
                "Philosophers are questioning the nature of the world. What should be the focus?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on ethics and morality", "LOG +1"), // Логика через моральные вопросы
            new QuestAnswer("Focus on the study of the natural world", "SCI +1") // Наука через естествознание
                }
            )); // В этом квесте два варианта ответа

            quests.Add(new Quest(
                "Overextension of the Empire", "AntiquityEra",
                "The empire is expanding too fast. What are the consequences?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Weak leadership causes internal strife", "MOT -1"), // Потеря мотивации
            new QuestAnswer("Increased demands cause fatigue in citizens", "END -1") // Потеря выносливости
                } // В этом квесте оба исхода отрицательные
            ));

            quests.Add(new Quest(
                "The Spread of Trade", "AntiquityEra",
                "Trade routes are expanding across the world. What should be prioritized?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Increase trade with neighboring civilizations", "SOC +1"), // Социальные навыки через торговлю
            new QuestAnswer("Establish new trade routes with distant lands", "LRN +1") // Обучаемость через новые контакты
                } // В этом квесте два варианта ответа
            ));

            quests.Add(new Quest(
                "The Decline of City-States", "AntiquityEra",
                "The city-state model is beginning to decline. What should be done?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Form larger kingdoms or empires", "STR +1"), // Сила через объединение
            new QuestAnswer("Focus on creating strong local communities", "SOC +1") // Социальные навыки через локальные сообщества
                }
            ));

            quests.Add(new Quest(
                "The Beginning of Science", "AntiquityEra",
                "A new era of scientific inquiry is beginning. What should be the primary focus?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on astronomy and the study of the stars", "SCI +1"), // Наука через астрономию
            new QuestAnswer("Focus on medicine and improving healthcare", "LOG +1"), // Логика через медицинские исследования
            new QuestAnswer("Focus on mathematics and geometry", "LRN +1") // Обучаемость через математику
                }
            ));

            quests.Add(new Quest(
                "The First Legal Code", "AntiquityEra",
                "A set of laws is being proposed. What should it emphasize?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Ensure fairness and equality for all", "SOC +1"), // Социальные навыки через справедливость
            new QuestAnswer("Focus on protecting religious rights", "REL +1") // Религия через законы
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
            new QuestAnswer("Give power to a single monarch", "STR +1"), // Сила через централизованную власть
            new QuestAnswer("Divide power among local lords", "SOC +1") // Социальные навыки через распределение власти
                }
            )); // Уменьшено до двух вариантов

            quests.Add(new Quest(
                "The Crusades", "MiddleAgesEra",
                "A religious war is being proposed. Should we participate?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Join the crusades for religious glory", "REL +1"), // Религия через участие в крестовых походах
            new QuestAnswer("Stay neutral and focus on internal growth", "CUL +1"), // Культура через развитие общества
            new QuestAnswer("Defend against invaders", "STR +1") // Сила через защиту от врагов
                }
            ));

            quests.Add(new Quest(
                "The Black Death", "MiddleAgesEra",
                "A plague is sweeping across the land. How should we respond?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Quarantine and isolate the infected", "SOC +1"), // Социальные навыки через изоляцию больных
            new QuestAnswer("Focus on finding a cure", "SCI +1") // Наука через поиск лекарства
                }
            )); // Уменьшено до двух вариантов

            quests.Add(new Quest(
                "The Rise of the Catholic Church", "MiddleAgesEra",
                "The Catholic Church is gaining immense power. What should its role be?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Maintain the power of the church and its influence", "REL +1"), // Религия через сохранение влияния церкви
            new QuestAnswer("Separate religion from state affairs", "CUL +1"), // Культура через отделение религии от государства
            new QuestAnswer("Use the church to unify the population", "SOC +1") // Социальные навыки через объединение народа
                }
            ));

            quests.Add(new Quest(
                "The Development of Castles", "MiddleAgesEra",
                "Castles are being built to defend against invaders. What should their primary purpose be?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on defense and military strategy", "STR +1"), // Сила через оборону
            new QuestAnswer("Use them as symbols of power and authority", "CUL +1"), // Культура через символ власти
            new QuestAnswer("Create centers of culture and learning within them", "CRE +1") // Креативность через развитие культурных центров
                }
            ));

            quests.Add(new Quest(
                "The Magna Carta", "MiddleAgesEra",
                "A new legal document is being proposed to limit the king's power. Should we support it?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Support it to ensure justice and rights", "SOC +1"), // Социальные навыки через справедливость
            new QuestAnswer("Reject it to maintain the monarch's power", "STR +1") // Сила через сохранение власти монарха
                }
            )); // Уменьшено до двух вариантов

            quests.Add(new Quest(
                "The Rise of Guilds", "MiddleAgesEra",
                "Craftsmen and traders are forming guilds. What should their role be?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Encourage the formation of guilds to protect workers", "SOC +1"), // Социальные навыки через защиту рабочих
            new QuestAnswer("Ban the guilds to ensure control by the state", "STR +1") // Сила через запрет гильдий
                }
            )); // Уменьшено до двух вариантов

            quests.Add(new Quest(
                "The Invention of the Printing Press", "MiddleAgesEra",
                "A new printing press has been invented. What should we do with it?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Use it to spread religious texts", "REL +1"), // Религия через распространение текстов
            new QuestAnswer("Use it to spread knowledge and education", "SCI +1"), // Наука через образование
            new QuestAnswer("Control its use to maintain political power", "SOC -1") // Отрицательный эффект на социальные навыки через контроль информации
                }
            ));
        }
        else if (era.eraName.Contains("Renaissance"))
        {
            // The Age of Exploration
            quests.Add(new Quest(
                "The Age of Exploration", "RenaissanceEra",
                "The world is being mapped out and new lands are being discovered. What should our focus be?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on colonizing new lands", "STR +1"), // Сила
            new QuestAnswer("Focus on cultural exchange with the new world", "CUL +1") // Культура
                }
            ));

            // The Printing Revolution
            quests.Add(new Quest(
                "The Printing Revolution", "RenaissanceEra",
                "Printing technology has advanced. What should be printed?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Print religious texts to spread faith", "REL +1"), // Религия
            new QuestAnswer("Print scientific works to spread knowledge", "SCI +1") // Наука
                }
            ));

            // The Rise of Humanism
            quests.Add(new Quest(
                "The Rise of Humanism", "RenaissanceEra",
                "Humanism is gaining traction. How should we incorporate it into our society?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Promote the importance of individual achievements", "MOT +1"), // Мотивация
            new QuestAnswer("Emphasize the study of classical texts and philosophy", "LOG +1") // Логика
                }
            ));

            // The Birth of Modern Art
            quests.Add(new Quest(
                "The Birth of Modern Art", "RenaissanceEra",
                "Art is flourishing. What should be the focus of our artists?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on realistic portrayals of the human form", "CRE +1"), // Креативность
            new QuestAnswer("Focus on abstract and symbolic representations", "LOG +1") // Логика
                }
            ));

            // The Scientific Revolution
            quests.Add(new Quest(
                "The Scientific Revolution", "RenaissanceEra",
                "New discoveries in science are challenging old beliefs. How should we proceed?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Support scientific discoveries to advance knowledge", "SCI +1"), // Наука
            new QuestAnswer("Maintain traditional beliefs to preserve order", "SOC -1"), // Социальные навыки (негативно)
            new QuestAnswer("Promote the use of science in practical inventions", "CRE +1") // Креативность
                }
            ));

            // The Invention of the Telescope
            quests.Add(new Quest(
                "The Invention of the Telescope", "RenaissanceEra",
                "A new telescope has been invented, expanding our view of the universe. How should we use it?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Use it to explore the stars and expand our knowledge", "SCI +1"), // Наука
            new QuestAnswer("Use it for military surveillance and defense", "END -1") // Выносливость (негативно)
                }
            ));

            // The Rise of Banking
            quests.Add(new Quest(
                "The Rise of Banking", "RenaissanceEra",
                "Banking is becoming more centralized. How should we handle this new economic system?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Encourage the development of banks to boost trade", "SOC +1"), // Социальные навыки
            new QuestAnswer("Limit the power of banks to prevent corruption", "LOG +1"), // Логика
            new QuestAnswer("Use banks to fund expansion and growth", "MOT +1") // Мотивация
                }
            ));

            // The Protestant Reformation
            quests.Add(new Quest(
                "The Protestant Reformation", "RenaissanceEra",
                "A religious movement is challenging the authority of the Church. Should we support it?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Support the Reformation to empower individual faith", "REL +1"), // Религия
            new QuestAnswer("Oppose the Reformation to maintain the Church's power", "SOC -1"), // Социальные навыки (негативно)
            new QuestAnswer("Remain neutral and focus on internal development", "LOG +1") // Логика
                }
            ));

            // The Fall of Constantinople
            quests.Add(new Quest(
                "The Fall of Constantinople", "RenaissanceEra",
                "The great city of Constantinople has fallen. How should we respond?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Focus on rebuilding the city and maintaining order", "SOC +1"), // Социальные навыки
            new QuestAnswer("Use the opportunity to expand our own empire", "STR +1"), // Сила
            new QuestAnswer("Seek alliances to counter the new power in the region", "END -1") // Выносливость (негативно)
                }
            ));

            // The Impact of Disease
            quests.Add(new Quest(
                "The Impact of Disease", "RenaissanceEra",
                "A deadly disease is spreading across the region. How should we respond?",
                new List<QuestAnswer>
                {
            new QuestAnswer("Quarantine affected areas and focus on containment", "SOC -1"), // Социальные навыки (негативно)
            new QuestAnswer("Focus on finding a cure through scientific research", "SCI +1"), // Наука
            new QuestAnswer("Use the disease as an opportunity to weaken rivals", "STR -1") // Сила (негативно)
                }
            ));
        }
        else if (era.eraName.Contains("Modern"))
        {
            // Industrial Revolution
            quests.Add(new Quest(
                "The Industrial Revolution", "ModernEra",
                "New machines are being invented. How should we use this newfound technology?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Focus on factory production for economic growth", "SOC +1"), // Социальные навыки
        new QuestAnswer("Focus on improving living standards for workers", "EMO +1"), // Эмоциональная стабильность
        new QuestAnswer("Focus on developing military technologies for power", "STR -1") // Сила
                }
            ));

            // Rise of Nationalism
            quests.Add(new Quest(
                "The Rise of Nationalism", "ModernEra",
                "Nationalism is growing across the globe. How should we approach it?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Promote unity and national pride", "SOC +1"), // Социальные навыки
        new QuestAnswer("Support independence movements for minorities", "LOG +1"), // Логика
        new QuestAnswer("Oppose nationalism to maintain stability", "END -1") // Выносливость
                }
            ));

            // Advent of Electricity
            quests.Add(new Quest(
                "The Advent of Electricity", "ModernEra",
                "Electricity is becoming widespread. How should we use it?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Focus on powering industries for mass production", "LRN +1"), // Обучаемость
        new QuestAnswer("Focus on improving everyday life with electric lighting", "SOC +1"), // Социальные навыки
        new QuestAnswer("Focus on developing electrical weapons for defense", "STR -1") // Сила
                }
            ));

            // Development of Mass Media
            quests.Add(new Quest(
                "The Development of Mass Media", "ModernEra",
                "Newspapers and radio are spreading information rapidly. What should be our focus?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Use it to spread political ideologies", "LOG +1"), // Логика
        new QuestAnswer("Use it to promote cultural values and art", "CUL +1"), // Культура
        new QuestAnswer("Use it to manipulate public opinion for control", "SOC -1") // Социальные навыки
                }
            ));

            // First World War
            quests.Add(new Quest(
                "The First World War", "ModernEra",
                "A great war has erupted. Should we focus on offense or defense?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Focus on offense to conquer territories", "STR +1"), // Сила
        new QuestAnswer("Focus on defense to protect our borders", "END +1"), // Выносливость
        new QuestAnswer("Focus on diplomacy to avoid the conflict", "SOC -1") // Социальные навыки
                }
            ));

            // Rise of Democracy
            quests.Add(new Quest(
                "The Rise of Democracy", "ModernEra",
                "Democracy is spreading globally. How should we approach it?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Support the spread of democratic values", "SOC +1"), // Социальные навыки
        new QuestAnswer("Maintain strong control to preserve order", "LOG +1"), // Логика
        new QuestAnswer("Oppose democracy to preserve stability", "EMO -1") // Эмоциональная стабильность
                }
            ));

            // Space Race
            quests.Add(new Quest(
                "The Space Race", "ModernEra",
                "The race to space is on. How should we participate?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Invest in space exploration for scientific progress", "SCI +1"), // Наука
        new QuestAnswer("Focus on military use of space technologies", "STR +1"), // Сила
        new QuestAnswer("Focus on space tourism to boost economy", "LRN -1") // Обучаемость
                }
            ));

            // Internet Revolution
            quests.Add(new Quest(
                "The Internet Revolution", "ModernEra",
                "The internet is changing the world. How should we harness its power?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Focus on global communication and information sharing", "SOC +1"), // Социальные навыки
        new QuestAnswer("Focus on using the internet to boost economic growth", "LRN +1"), // Обучаемость
        new QuestAnswer("Use it for surveillance and mass control", "LOG -1") // Логика
                }
            ));

            // Green Revolution
            quests.Add(new Quest(
                "The Green Revolution", "ModernEra",
                "New agricultural techniques are emerging. How should we use them?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Use it to increase food production for a growing population", "CRE +1"), // Креативность
        new QuestAnswer("Focus on sustainable agriculture to preserve the environment", "CUL +1"), // Культура
        new QuestAnswer("Use it to control food distribution and stabilize the economy", "LOG -1") // Логика
                }
            ));

            // Nuclear Age
            quests.Add(new Quest(
                "The Nuclear Age", "ModernEra",
                "Nuclear technology has been harnessed. How should it be used?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Develop nuclear weapons for defense", "STR +1"), // Сила
        new QuestAnswer("Use nuclear energy to power industries and homes", "LRN +1"), // Обучаемость
        new QuestAnswer("Focus on peaceful nuclear technology for scientific research", "SCI +1") // Наука
                }
            ));
        }
        else if (era.eraName.Contains("Our"))
        {
            // Global Climate Change
            quests.Add(new Quest(
                "Global Climate Change", "OurTimeEra",
                "Climate change is becoming a global issue. How should we respond?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Implement green technologies to reduce emissions", "CUL +1"), // Культура
        new QuestAnswer("Focus on adaptation and infrastructure to withstand climate change", "SOC +1"), // Социальные навыки
        new QuestAnswer("Prioritize economic growth over climate concerns", "SCI -1") // Наука
                }
            ));

            // Artificial Intelligence
            quests.Add(new Quest(
                "Artificial Intelligence", "OurTimeEra",
                "Artificial Intelligence is advancing rapidly. How should we integrate it into society?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Focus on AI for economic growth and efficiency", "LOG +1"), // Логика
        new QuestAnswer("Ban AI development to prevent societal disruption", "END -1") // Выносливость
                }
            ));

            // Space Colonization
            quests.Add(new Quest(
                "Space Colonization", "OurTimeEra",
                "Mankind is looking to colonize other planets. How should we approach it?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Invest heavily in space exploration to secure our future", "SCI +1"), // Наука
        new QuestAnswer("Focus on colonizing Mars and other planets to expand humanity", "STR +1"), // Сила
        new QuestAnswer("Improve sustainability on Earth before venturing into space", "CUL -1") // Культура
                }
            ));

            // Universal Basic Income
            quests.Add(new Quest(
                "Universal Basic Income", "OurTimeEra",
                "The idea of universal basic income is being debated. What should we do?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Implement universal basic income to reduce inequality", "SOC +1"), // Социальные навыки
                }
            ));

            // Globalization vs Nationalism
            quests.Add(new Quest(
                "Globalization vs Nationalism", "OurTimeEra",
                "The world is becoming more interconnected. Should we embrace globalization or focus on nationalism?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Support nationalism to protect cultural and economic identity", "CUL +1"), // Культура
        new QuestAnswer("Focus on a balanced approach that encourages both", "LOG +1") // Логика
                }
            ));

            // Healthcare for All
            quests.Add(new Quest(
                "Healthcare for All", "OurTimeEra",
                "Healthcare access is a critical issue. How should we ensure it for everyone?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Implement universal healthcare systems globally", "SOC +1"), // Социальные навыки
        new QuestAnswer("Focus on privatizing healthcare to increase efficiency", "LOG +1"), // Логика
                }
            ));

            // Sustainable Energy
            quests.Add(new Quest(
                "Sustainable Energy", "OurTimeEra",
                "The need for sustainable energy sources has never been greater. What should be our focus?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Invest in renewable energy sources like solar and wind", "CUL +1"), // Культура
        new QuestAnswer("Focus on nuclear energy as a clean and powerful option", "SCI +1"), // Наука
        new QuestAnswer("Promote energy conservation and efficiency measures", "SOC +1") // Социальные навыки
                }
            ));

            // Digital Privacy
            quests.Add(new Quest(
                "Digital Privacy", "OurTimeEra",
                "Digital privacy is being threatened in many parts of the world. How should we protect individuals?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Invest in technology to protect privacy through encryption", "LOG +1"), // Логика
        new QuestAnswer("Focus on educating the public on privacy and cybersecurity", "SOC +1") // Социальные навыки
                }
            ));

            // Technological Unemployment
            quests.Add(new Quest(
                "Technological Unemployment", "OurTimeEra",
                "Automation and AI are threatening jobs across the globe. What should we do?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Implement policies for job retraining and upskilling", "SOC +1"), // Социальные навыки
        new QuestAnswer("Support businesses in transitioning to automation while protecting workers", "LOG +1"), // Логика
                }
            ));

            // The Future of Education
            quests.Add(new Quest(
                "The Future of Education", "OurTimeEra",
                "Education is evolving rapidly. How should we prepare for the future?",
                new List<QuestAnswer>
                {
        new QuestAnswer("Implement online learning and virtual classrooms worldwide", "SOC +1"), // Социальные навыки
        new QuestAnswer("Focus on STEM education to drive future technological growth", "SCI +1"), // Наука
        new QuestAnswer("Promote alternative education models that foster creativity and critical thinking", "CRE +1") // Креативность
                }
            ));

        }



        return quests;
    }

}
