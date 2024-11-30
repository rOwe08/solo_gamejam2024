using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public Human man;
    public Human woman;

    public GameObject statsPanel;

    // Общие показатели человечества
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

    void Start()
    {
        man = HumanManager.Instance.chosenMan;
        woman = HumanManager.Instance.chosenWoman;

        CalculateAverageStats();
        UIManager.Instance.UpdateStatsPanel(humanityStats);

        StartCoroutine(UIManager.Instance.ShowMessage("First people created new village", "Here we go again...", 3f));

        //CheckPrimitiveSocietyEvents();
        //CheckAncientWorldEvents();
        //CheckAntiquityEvents();
        //CheckMiddleAgesEvents();
        //CheckRenaissanceEvents();
        //CheckIndustrialEraEvents();
        //CheckModernEraEvents();

        // Пример побочного события
        //TriggerRandomEvent();
    }

    void CalculateAverageStats()
    {
        List<Stat> manStats = man.Stats;
        List<Stat> womanStats = woman.Stats;

        for (int i = 0; i < manStats.Count; i++)
        {
            float averageValue = (manStats[i].Value + womanStats[i].Value) / 2;
            string abbreviation = manStats[i].Abbreviation; // Используем аббревиатуру из manStats
            humanityStats.Add(new Stat(manStats[i].Name, abbreviation, averageValue));
        }
    }

    // Primitive Society
    void CheckPrimitiveSocietyEvents()
    {
        if (averageStrength > 4 || averageLogic > 5)
        {
            Debug.Log("Изобретение орудий труда произошло");
        }
        else
        {
            Debug.Log("Изобретение орудий труда не произошло");
        }

        if (averageCreativity > 4 || averageLearnability > 5)
        {
            Debug.Log("Открытие огня произошло");
        }
        else
        {
            Debug.Log("Открытие огня не произошло");
        }

        if (averageSocialSkills > 4 && averageEmotionalStability > 4)
        {
            Debug.Log("Развитие речевой коммуникации произошло");
        }
        else
        {
            Debug.Log("Развитие речевой коммуникации не произошло");
        }

        if (averageEndurance > 5 && averageMotivation > 4)
        {
            Debug.Log("Появление первых племен произошло");
        }
        else
        {
            Debug.Log("Появление первых племен не произошло");
        }

        if (averageStrength > 5 && averageAgility > 4)
        {
            Debug.Log("Одомашнивание животных произошло");
        }
        else
        {
            Debug.Log("Одомашнивание животных не произошло");
        }
    }

    // Ancient World
    void CheckAncientWorldEvents()
    {
        if (averageEndurance > 5 && averageCreativity > 5)
        {
            Debug.Log("Развитие земледелия произошло");
        }
        else
        {
            Debug.Log("Развитие земледелия не произошло");
        }

        if (averageLearnability > 6)
        {
            Debug.Log("Возникновение письменности произошло");
        }
        else
        {
            Debug.Log("Возникновение письменности не произошло");
        }

        if (averageLogic > 5 && averageMotivation > 5)
        {
            Debug.Log("Строительство первых городов произошло");
        }
        else
        {
            Debug.Log("Строительство первых городов не произошло");
        }

        if (averageSocialSkills > 5 && averageLogic > 5)
        {
            Debug.Log("Появление государства и законов произошло");
        }
        else
        {
            Debug.Log("Появление государства и законов не произошло");
        }

        if (averageEmotionalStability > 4 && averageSocialSkills > 6)
        {
            Debug.Log("Возникновение религий произошло");
        }
        else
        {
            Debug.Log("Возникновение религий не произошло");
        }
    }

    // Antiquity
    void CheckAntiquityEvents()
    {
        if (averageStrength > 6 && averageEndurance > 5)
        {
            Debug.Log("Расширение империй произошло");
        }
        else
        {
            Debug.Log("Расширение империй не произошло");
        }

        if (averageLogic > 6)
        {
            Debug.Log("Развитие философии произошло");
        }
        else
        {
            Debug.Log("Развитие философии не произошло");
        }

        if (averageCreativity > 6)
        {
            Debug.Log("Расцвет искусства и архитектуры произошел");
        }
        else
        {
            Debug.Log("Расцвет искусства и архитектуры не произошло");
        }

        if (averageMotivation > 6 && averageSocialSkills > 6)
        {
            Debug.Log("Развитие международной торговли произошло");
        }
        else
        {
            Debug.Log("Развитие международной торговли не произошло");
        }

        if (averageEndurance > 5 && averageLearnability > 5)
        {
            Debug.Log("Появление организованной армии произошло");
        }
        else
        {
            Debug.Log("Появление организованной армии не произошло");
        }
    }

    // Middle Ages
    void CheckMiddleAgesEvents()
    {
        if (averageLogic > 5)
        {
            Debug.Log("Создание университетов произошло");
        }
        else
        {
            Debug.Log("Создание университетов не произошло");
        }

        if (averageMotivation > 5)
        {
            Debug.Log("Крестовые походы произошли");
        }
        else
        {
            Debug.Log("Крестовые походы не произошли");
        }

        if (averageSocialSkills > 6)
        {
            Debug.Log("Развитие феодальной системы произошло");
        }
        else
        {
            Debug.Log("Развитие феодальной системы не произошло");
        }

        if (averageEndurance > 5)
        {
            Debug.Log("Черная смерть произошла");
        }
        else
        {
            Debug.Log("Черная смерть не произошла");
        }

        if (averageCreativity > 5)
        {
            Debug.Log("Развитие искусства витражей произошло");
        }
        else
        {
            Debug.Log("Развитие искусства витражей не произошло");
        }
    }

    // Renaissance
    void CheckRenaissanceEvents()
    {
        if (averageCreativity > 7)
        {
            Debug.Log("Расцвет искусства Возрождения произошел");
        }
        else
        {
            Debug.Log("Расцвет искусства Возрождения не произошел");
        }

        if (averageLearnability > 6)
        {
            Debug.Log("Научные открытия произошли");
        }
        else
        {
            Debug.Log("Научные открытия не произошли");
        }

        if (averageSocialSkills > 6)
        {
            Debug.Log("Эпоха Великих географических открытий произошла");
        }
        else
        {
            Debug.Log("Эпоха Великих географических открытий не произошла");
        }

        if (averageMotivation > 6)
        {
            Debug.Log("Развитие книгопечатания произошло");
        }
        else
        {
            Debug.Log("Развитие книгопечатания не произошло");
        }

        if (averageLogic > 6)
        {
            Debug.Log("Реформация произошла");
        }
        else
        {
            Debug.Log("Реформация не произошла");
        }
    }

    // Industrial Era
    void CheckIndustrialEraEvents()
    {
        if (averageLearnability > 6)
        {
            Debug.Log("Индустриальная революция произошла");
        }
        else
        {
            Debug.Log("Индустриальная революция не произошла");
        }

        if (averageMotivation > 7)
        {
            Debug.Log("Изобретение паровой машины произошло");
        }
        else
        {
            Debug.Log("Изобретение паровой машины не произошло");
        }

        if (averageLogic > 6)
        {
            Debug.Log("Появление фабрик и заводов произошло");
        }
        else
        {
            Debug.Log("Появление фабрик и заводов не произошло");
        }

        if (averageSocialSkills > 6)
        {
            Debug.Log("Борьба за рабочие права произошла");
        }
        else
        {
            Debug.Log("Борьба за рабочие права не произошла");
        }

        if (averageEndurance > 6)
        {
            Debug.Log("Развитие железных дорог произошло");
        }
        else
        {
            Debug.Log("Развитие железных дорог не произошло");
        }
    }

    // Modern Era
    void CheckModernEraEvents()
    {
        if (averageLearnability > 8)
        {
            Debug.Log("Цифровая революция произошла");
        }
        else
        {
            Debug.Log("Цифровая революция не произошла");
        }

        if (averageSocialSkills > 7)
        {
            Debug.Log("Глобализация произошла");
        }
        else
        {
            Debug.Log("Глобализация не произошла");
        }

        if (averageCreativity > 7)
        {
            Debug.Log("Революция в искусстве и культуре произошла");
        }
        else
        {
            Debug.Log("Революция в искусстве и культуре не произошла");
        }

        if (averageLogic > 8)
        {
            Debug.Log("Развитие искусственного интеллекта произошло");
        }
        else
        {
            Debug.Log("Развитие искусственного интеллекта не произошло");
        }

        if (averageEndurance > 6)
        {
            Debug.Log("Устойчивое развитие и экология произошли");
        }
        else
        {
            Debug.Log("Устойчивое развитие и экология не произошли");
        }
    }

    // Побочное событие
    void TriggerRandomEvent()
    {
        int randomEvent = Random.Range(0, 2); // Выбор события случайным образом (например, +1 к логике или выносливости)

        if (randomEvent == 0)
        {
            averageLogic += 1;
            Debug.Log("Побочное событие: логика +1");
        }
        else
        {
            averageEndurance += 1;
            Debug.Log("Побочное событие: выносливость +1");
        }
    }
}
