using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestAnswer
{
    public string Description;
    public List<string> Result;

    public Action OnChosen;

    // Constructor to initialize the quest answer
    public QuestAnswer(string description, List<string> result, Action onChosen = null)
    {
        Description = description;
        Result = result;

        // If onChosen is null, assign the default action
        if (onChosen == null)
        {
            OnChosen = () => GameManager.Instance.ApplyResult(result);
        }
        else
        {
            OnChosen = onChosen;
        }
    }
}

[System.Serializable]
public class Quest
{
    public string Name;
    public string Era;
    public string Description;
    public List<QuestAnswer> Answers;

    // Constructor to initialize the quest
    public Quest(string name, string era, string description, List<QuestAnswer> answers)
    {
        Name = name;
        Era = era;
        Description = description;
        Answers = answers;
    }
}
