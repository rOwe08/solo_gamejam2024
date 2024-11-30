using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(UIManager.Instance.OnStartButtonClicked);
        UIManager.Instance.startButton = gameObject.GetComponent<Button>();
    }
}
