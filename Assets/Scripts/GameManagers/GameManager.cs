using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //events for communicating with the singleton
    public static event Action TitleInput;
    public static event Action PauseToggled;
    public UnityEvent PlayerDied;
    public static void TitleInputInvoke() {  TitleInput.Invoke(); }   
    public static void PauseToggledInvoke() {  PauseToggled.Invoke(); }

    [SerializeField] string TitleSceneName = "Title";
    [SerializeField] string MainMenuSceneName = "MainMenu";
    [SerializeField] string PauseSceneName = "PauseMenu";
    [SerializeField] string GameOverScreenName = "GameOver";

    bool isPaused = false;
    [SerializeField] float deathScreenDelay = 1f;

    Coroutine gameovercoroutine = null;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
        TitleInput += TitleStart;
        PauseToggled += TogglePause;
        
    }

    private void Start()
    {
        PlayerDied.AddListener(OnPlayerDeath);
    }

    public void TitleStart() {
        SceneManager.LoadScene(MainMenuSceneName);
        
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        switch (isPaused)
        {
            case true: 
                initiatePause();
                return;
            case false:
                endPause();
                return;
        }
    }

    public void initiatePause()
    {
        Debug.Log("Pause");
        SceneManager.LoadScene(PauseSceneName, LoadSceneMode.Additive);
    }

    public void endPause()
    {
        Debug.Log("Unpause");
        SceneManager.UnloadSceneAsync(PauseSceneName);
    }

    public void setScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void Quit()
    {
#if UNITY_EDITOR
        // Quit the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quit the game
        Application.Quit();
#endif
    }

    public void OnPlayerDeath()
    {
        Debug.Log("Deathscreen");
        gameovercoroutine = StartCoroutine("DeathScreenDelay");
    }

    IEnumerator DeathScreenDelay()
    {
        yield return new WaitForSeconds(deathScreenDelay);

        SceneManager.LoadScene(GameOverScreenName);
    }
}
