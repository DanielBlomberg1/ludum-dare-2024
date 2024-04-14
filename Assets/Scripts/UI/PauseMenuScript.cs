using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    
    [SerializeField] private GameObject pauseMenu;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            pauseMenu.SetActive(true);
            GameManager.Instance.UpdateGameState(GameState.Pause);
        }
    }

    public void OnExitPressed(){
        pauseMenu.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.Play);
    }
}
