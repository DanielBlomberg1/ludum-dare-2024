using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    [SerializeField] private GameObject LevelSelectPanel;

    public void Play()
    {
        SelectLevel(0);
    }
    public void LevelSelect()
    {
        LevelSelectPanel.SetActive(true);
    }
    public void CloseLevelSelect()
    {
        LevelSelectPanel.SetActive(false);
    }
    public void SelectLevel(int level)
    {
        GameManager.Instance.ChangeCurrentLevel(level);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
