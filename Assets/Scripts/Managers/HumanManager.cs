using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : MonoBehaviour
{

    public List<Human> listHumans;

    public HumanPanel manPanel;
    public HumanPanel womanPanel;

    private static HumanManager _instance;
    public static HumanManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<HumanManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("HumanManager");
                    _instance = obj.AddComponent<HumanManager>();
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
        else
        {
            Destroy(gameObject);
        }

        listHumans = new List<Human>();
    }

    public void AssignHuman(Human human)
    {
        // Determine if the human is a man or a woman and assign to the correct panel
        if (human is Man)
        {
            if (manPanel.assignedHuman != null) // If there is already a human assigned, unassign it
            {
                manPanel.UnassignHuman();
            }

            // Assign the new human to the man panel
            manPanel.AssignHuman(human);
        }
        else if (human is Woman)
        {
            if (womanPanel.assignedHuman != null) // If there is already a human assigned, unassign it
            {
                womanPanel.UnassignHuman();
            }

            // Assign the new human to the woman panel
            womanPanel.AssignHuman(human);
        }

        Debug.Log("Human assigned. Total humans in list: " + listHumans.Count);
    }

    public void AddHuman(Human human)
    {
        listHumans.Add(human);
    }
}
