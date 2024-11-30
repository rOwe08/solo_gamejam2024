using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

public class UIManager : MonoBehaviour
{
    private UIManager _instance;
    public HumanPanel womanPanel;
    public HumanPanel manPanel;
    public Button startButton;
    private PulsingObject pulsingStartButton;

    // Новый объект для перехода (покрывающий экран)
    public Image transitionImage;
    private CanvasGroup transitionCanvasGroup;

    public UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();

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
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);  // Для UIManager, чтобы он сохранялся между сценами
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Получаем компонент PulsingObject для кнопки
        pulsingStartButton = startButton.GetComponent<PulsingObject>();

        // Получаем CanvasGroup на объекте перехода
        transitionCanvasGroup = transitionImage.GetComponent<CanvasGroup>();

        // Изначально устанавливаем альфу в 0
        if (transitionCanvasGroup != null)
        {
            transitionCanvasGroup.alpha = 0;
            transitionCanvasGroup.interactable = false; // Отключаем взаимодействие, чтобы другие элементы UI были активны
            transitionCanvasGroup.blocksRaycasts = false; // Отключаем блокировку кликов
        }

        startButton.onClick.AddListener(OnStartButtonClicked);

        // Делаем transitionImage тоже DontDestroyOnLoad
        DontDestroyOnLoad(transitionImage.gameObject);
    }

    void Update()
    {
        // Проверяем, назначены ли оба персонажа
        if (womanPanel.assignedHuman != null && manPanel.assignedHuman != null)
        {
            // Делаем кнопку доступной для нажатия и активируем пульсацию
            startButton.interactable = true;
            if (pulsingStartButton != null)
            {
                pulsingStartButton.isActive = true;  // Включаем пульсацию
            }
        }
        else
        {
            // Если хотя бы один персонаж не назначен, делаем кнопку недоступной и останавливаем пульсацию
            startButton.interactable = false;
            if (pulsingStartButton != null)
            {
                pulsingStartButton.isActive = false;  // Останавливаем пульсацию
            }
        }
    }

    public void OnStartButtonClicked()
    {
        // Запускаем анимацию для плавного увеличения альфы с использованием CanvasGroup
        transitionCanvasGroup.DOFade(1, 1f).OnComplete(() =>
        {
            // После завершения анимации загрузим следующую сцену
            LoadNextScene();
        });
    }

    private void LoadNextScene()
    {
        // Загрузка следующей сцены
        SceneManager.LoadScene("SimulationScene"); // Замените на имя вашей сцены

        // Начинаем анимацию перехода в следующей сцене
        StartCoroutine(WaitForSceneAndFadeOut());
    }

    private IEnumerator WaitForSceneAndFadeOut()
    {
        // Ждем пока сцена загрузится
        yield return new WaitForSeconds(1f);  // Это зависит от длительности анимации, подберите нужное время

        // Начинаем уменьшать альфу с 1 до 0
        transitionCanvasGroup.DOFade(0, 1f);
    }
}
