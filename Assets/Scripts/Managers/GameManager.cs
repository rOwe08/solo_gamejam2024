using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public int physicsPoints = 5;
    public int intellectPoints = 5;
    public int mentalPoints = 5;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    _instance = obj.AddComponent<GameManager>();
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
        else if (_instance != this)
        {
            Destroy(gameObject); 
        }
    }

    public void ApplyResult(string result)
    {
        GameObject simulationManager = GameObject.Find("SimulationManager");

        simulationManager.GetComponent<SimulationManager>().ApplyResult(result); 
    }

    public void LoadNextScene(string sceneName)
    {
        MusicManager.Instance.StopMusic();
        UIManager.Instance.transitionCanvasGroup.DOFade(1, 2f).OnComplete(() =>
        {
            // После завершения анимации загрузим следующую сцену
            GameManager.Instance.LoadScene(sceneName);
            MusicManager.Instance.PlayMusic(SceneManager.GetSceneByName(sceneName).buildIndex);
        });
    }

    public void LoadScene(string sceneName)
    {
        // Загрузка следующей сцены
        SceneManager.LoadScene(sceneName); // Замените на имя вашей сцены

        // Начинаем анимацию перехода в следующей сцене
        UIManager.Instance.StartCoroutine(UIManager.Instance.WaitForSceneAndFadeOut());
    }

    void Update()
    {

    }
}
