using Eflatun.SceneReference;
using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeField]
    public List<LevelSettings> Levels { get; private set; } = new List<LevelSettings>();

    public LevelSettings GetCurrentLevelSettings() => Levels[_currentLevel];

    public GameState CurrentState { get; private set; }

    public static event Action<GameState> OnGameStateChanged;
    public static event Action OnLevelLoaded;

    [SerializeField]
    [Min(0)]
    private int _currentLevel = 0;

    [SerializeField]
    private SceneReference _gameUiScene;

    private int catsGooned = 0;
    private int catsEdged = 0;

    private float volume = 1;

    private void Start()
    {
        UpdateGameState(GameState.MainMenu);
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
                LoadLevel(++_currentLevel);
                break;
            case GameState.GameOver:
                LoadLevel(_currentLevel);
                break;
            default:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void ChangeCurrentLevel(int levelIndex)
    {
        _currentLevel = levelIndex;
        LoadLevel(_currentLevel);
    }

    private void LoadLevel(int levelIndex)
    {
        catsGooned = 0;
        
        if(levelIndex >= Levels.Count)
        {
            SceneManager.LoadScene("EndCutScene");
        }

        var levelToLoadSettings = Levels[levelIndex];

        LoadScene(levelToLoadSettings.LevelScene);
        LoadUI();

        UpdateGameState(GameState.Play);
        OnLevelLoaded?.Invoke();
    }

    private void LoadScene(SceneReference sceneAsset)
    {
        SceneManager.LoadScene(sceneAsset.Name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void LoadUI()
    {
        //if (SceneManager.GetSceneByName(_gameUiScene.name).isLoaded)
        //{
        //    return;
        //}

        if (Application.isPlaying)
        {
            SceneManager.LoadSceneAsync(_gameUiScene.Name, LoadSceneMode.Additive);
        }
        #if UNITY_EDITOR
        else
        {
            EditorSceneManager.OpenScene(_gameUiScene.Name, OpenSceneMode.Additive);
        }
        #endif
    }

    public void CatGoon(){
        catsGooned++;
        UiTextScript.Instance.UpdateCatsOutOfMaxx(catsGooned);

        CheckForEnd();
    }

    public void CatEdged(){
        catsEdged++;
        UiTextScript.Instance.UpdateCatsEdged(catsEdged);

        CheckForEnd();
    }

    private void CheckForEnd(){
        if(catsGooned >= Levels[_currentLevel].CatAmount){
            UpdateGameState(GameState.LevelComplete);
        } else if(catsEdged >= Levels[_currentLevel].CatAmount){
            UpdateGameState(GameState.GameOver);
        } else if(catsEdged + catsGooned == Levels[_currentLevel].CatAmount && catsEdged < Levels[_currentLevel].CatsRequiredToPass){
            UpdateGameState(GameState.GameOver);
        } else if(catsEdged + catsGooned == Levels[_currentLevel].CatAmount && catsEdged >= Levels[_currentLevel].CatsRequiredToPass){
            UpdateGameState(GameState.LevelComplete);
        } else if(catsGooned >= Levels[_currentLevel].CatsRequiredToPass){
            UiTextScript.Instance.CatIsEnough();
        }else if( Levels[_currentLevel].CatsRequiredToPass > Levels[_currentLevel].CatAmount - catsEdged){
            UiTextScript.Instance.CatLost();
        }
    }

    public void RestartLevel(){
        catsGooned = 0;
        LoadLevel(_currentLevel);
    }

    public void SetVolume(float value)
    {
        Debug.Log("Volume set to: " + value);
        volume = value;
    }

    public float GetVolume()
    {
        return volume;
    }
}
