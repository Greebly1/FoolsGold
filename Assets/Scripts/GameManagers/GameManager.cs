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
    [SerializeField] KeyCode pauseKey = KeyCode.P;
    public static void TitleInputInvoke() {  TitleInput.Invoke(); }
    public static void PauseToggledInvoke() { PauseToggled.Invoke(); }

    [SerializeField] LevelFlow Scenes;

    #region scene name getters
    string getSceneName (level levelEnum, string Default)
    {
        if (Scenes == null) { return Default; }
        string output;
        Scenes.SceneDictionary.TryGetValue(levelEnum, out output);
        if (output != null) { return output; }
        return Default;
    }
    string TitleSceneName {
        get {
            return getSceneName(level.Title, Default: "Title");
        }
    }
    string MainMenuSceneName
    {
        get
        {
            return getSceneName(level.MainMenu, Default: "MainMenu");
        }
    }
    string PauseSceneName
    {
        get
        {
            return getSceneName(level.PauseMenu, Default: "PauseMenu");
        }
    }
    string GameOverSceneName
    {
        get
        {
            return getSceneName(level.GameOver, Default: "GameOver");
        }
    }
    #endregion

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

    private void Update()
    {
        if (Input.GetKeyUp(pauseKey))
        {
            TogglePause();
        }
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
        Time.timeScale = 0;
        
        SceneManager.LoadScene(PauseSceneName, LoadSceneMode.Additive);
    }

    public void endPause()
    {
        Time.timeScale = 1;
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

        SceneManager.LoadScene(GameOverSceneName);
    }
}
