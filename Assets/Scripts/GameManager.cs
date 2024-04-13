using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    [field: SerializeField]
    public SceneAsset GameUIScene { get; private set; }

    [field: SerializeField]
    public List<LevelSettings> Levels { get; private set; } = new List<LevelSettings>();

    public GameState CurrentState { get; private set; }

    public static event Action<GameState> OnGameStateChanged;

    [SerializeField]
    private int _currentLevel = 0;

    [SerializeField]
    private SceneAsset _gameUiScene;

    private int catsGooned = 0;

    private void Start()
    {
        UpdateGameState(GameState.Play);
        LoadLevel(_currentLevel);
    }

    public void UpdateGameState(GameState newState)
    {
        CurrentState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.Play:
                Time.timeScale = 1f;
                break;
            case GameState.Pause:
                Time.timeScale = 0f;
                break;
            case GameState.LevelComplete:
                LoadLevel(_currentLevel++);
                break;
            case GameState.GameOver:
                LoadLevel(_currentLevel);
                break;
            default:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void LoadLevel(int levelIndex)
    {
        var levelToLoadSettings = Levels[levelIndex];

        LoadScene(levelToLoadSettings.LevelScene);
        LoadUI();

        UpdateGameState(GameState.Play);
    }

    private void LoadScene(SceneAsset sceneAsset)
    {
        SceneManager.LoadScene(sceneAsset.name);
    }

    private void LoadUI()
    {
        if (SceneManager.GetSceneByName(_gameUiScene.name).isLoaded)
        {
            return;
        }

        if (Application.isPlaying)
        {
            SceneManager.LoadSceneAsync(_gameUiScene.name, LoadSceneMode.Additive);
        }
        #if UNITY_EDITOR
        else
        {
            EditorSceneManager.OpenScene(_gameUiScene.name, OpenSceneMode.Additive);
        }
        #endif
    }

    public void CatGoon(){
        catsGooned++;
        if(catsGooned >= Levels[_currentLevel].CatAmount){
            UpdateGameState(GameState.LevelComplete);
        }
    }
}
