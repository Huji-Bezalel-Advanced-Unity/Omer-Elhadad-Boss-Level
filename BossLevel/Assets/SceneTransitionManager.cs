using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private string enterAnimation = "SceneEnter";
    [SerializeField] private string exitAnimation = "SceneExit";
    [SerializeField] private float transitionTime = 1f;

    private bool _isTransitioning = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        transitionAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void OnEnable()
    {
        PlayerHealthEvents.PlayerDeathEvent += OnGameEndedEvent;
        CrackHandler.BossDeathEvent += OnGameEndedEvent;
    }

    private void OnDisable()
    {
        PlayerHealthEvents.PlayerDeathEvent -= OnGameEndedEvent;
    }

    private void OnGameEndedEvent()
    {
        ChangeScene("Start Screen", 2f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeScene("SampleScene");
        }
    }

    public void ChangeScene(string sceneName, float preTransitionDelay = 0f)
    {
        if (_isTransitioning) return;
        _isTransitioning = true;
        StartCoroutine(Transition(sceneName, preTransitionDelay));
    }

    private IEnumerator Transition(string sceneName, float preTransitionDelay = 0f)
    {
        yield return new WaitForSecondsRealtime(preTransitionDelay);

        Time.timeScale = 0f;
        transitionAnimator.Play(enterAnimation);
        yield return new WaitForSecondsRealtime(transitionTime);
        
        SceneManager.LoadScene(sceneName); // Full reset
        yield return null;

        transitionAnimator.Play(exitAnimation);
        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(transitionTime);

        _isTransitioning = false;
    }
}