using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.transitionCanvasGroup = gameObject.GetComponent<CanvasGroup>();

        // ���������� ������������� ����� � 0
        if (UIManager.Instance.transitionCanvasGroup != null)
        {
            UIManager.Instance.transitionCanvasGroup.alpha = 1;
            UIManager.Instance.transitionCanvasGroup.interactable = false; // ��������� ��������������, ����� ������ �������� UI ���� �������
            UIManager.Instance.transitionCanvasGroup.blocksRaycasts = false; // ��������� ���������� ������
        }

        UIManager.Instance.StartCoroutine(UIManager.Instance.WaitForSceneAndFadeOut(0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
