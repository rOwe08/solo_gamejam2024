using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    private UIManager _instance;

    public UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                FindAnyObjectByType(typeof(UIManager));

                if (_instance == null)
                {
                    GameObject obj = new GameObject("UIManager");
                    _instance = obj.AddComponent<UIManager>();
                }
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        
    }
}
