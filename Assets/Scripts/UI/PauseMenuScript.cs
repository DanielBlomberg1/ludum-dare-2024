using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject volumeSlider;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if (pauseMenu.activeInHierarchy)
            {
                Close();
            }
            else
            {
                pauseMenu.SetActive(true);
                GameManager.Instance.UpdateGameState(GameState.Pause);
                
                volumeSlider.GetComponent<Scrollbar>().value = GameManager.Instance.GetVolume();
            }

        }else if(Input.GetKeyDown(KeyCode.R)){
            RestartLevel();
        }
    }

    public void OnExitPressed(){
        pauseMenu.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
        GameManager.Instance.LoadMainMenu();
    }

    public void RestartLevel(){
        pauseMenu.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.Play);
        GameManager.Instance.RestartLevel();
    }
    public void OnVolumeSliderChanged(float value)
    {
        GameManager.Instance.SetVolume(value);
    }

    public void Close()
    {
        GameManager.Instance.UpdateGameState(GameState.Play);
        pauseMenu.SetActive(false);
    }
}
