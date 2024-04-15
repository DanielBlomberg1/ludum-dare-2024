using UnityEngine;

public class ButtonsScript : MonoBehaviour
{
    [SerializeField] private GameObject LevelSelectPanel;

    public void Play()
    {
        SelectLevel(1);
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
        GameManager.Instance.ChangeCurrentLevel(level - 1);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void ExitToMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }
}
