using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsPanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.eventsPanel = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
