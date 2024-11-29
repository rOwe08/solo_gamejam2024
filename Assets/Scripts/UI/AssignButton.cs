using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignButton : MonoBehaviour
{
    public GameObject assignPanel;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            assignPanel.SetActive(!assignPanel.activeSelf);
            assignPanel.GetComponent<AssignPanel>().PreparePanel();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
