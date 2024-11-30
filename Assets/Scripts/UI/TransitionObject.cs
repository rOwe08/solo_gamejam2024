using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.transitionCanvasGroup = gameObject.GetComponent<CanvasGroup>();

        // Изначально устанавливаем альфу в 0
        if (UIManager.Instance.transitionCanvasGroup != null)
        {
            UIManager.Instance.transitionCanvasGroup.alpha = 1;
            UIManager.Instance.transitionCanvasGroup.interactable = false; // Отключаем взаимодействие, чтобы другие элементы UI были активны
            UIManager.Instance.transitionCanvasGroup.blocksRaycasts = false; // Отключаем блокировку кликов
        }

        UIManager.Instance.StartCoroutine(UIManager.Instance.WaitForSceneAndFadeOut(0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
