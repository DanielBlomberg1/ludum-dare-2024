using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    public GameState CurrentState { get; private set; }

    public static event Action<GameState> OnGameStateChanged;

    private void Start()
    {
        UpdateGameState(GameState.Play);
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
                NextLevel();
                break;
            case GameState.GameOver:
                ReloadLevel();
                break;
            default:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void ReloadLevel()
    {
        int currentLevelBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelBuildIndex);
    }

    public void NextLevel()
    {
        int nextLevelBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextLevelBuildIndex);
    }
}
