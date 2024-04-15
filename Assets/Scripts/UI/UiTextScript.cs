using UnityEngine;
using TMPro;

public class UiTextScript : Singleton<UiTextScript>
{
    [SerializeField] private TMP_Text requiredText;
    [SerializeField] private TMP_Text catsOutOfMaxx;
    [SerializeField] private TMP_Text catsEdged;

    private int maxCats;

    private void Start() {
        catsOutOfMaxx.color = Color.white;
        maxCats = GameManager.Instance.GetCurrentLevelSettings().CatAmount;
        UpdateCatsOutOfMaxx(0);
        UpdateRequiredText(GameManager.Instance.GetCurrentLevelSettings().CatsRequiredToPass);
    }

    public void UpdateRequiredText(int required)
    {
        requiredText.text = required.ToString();
    }

    public void UpdateCatsOutOfMaxx(int cats)
    {
        catsOutOfMaxx.text = $"{cats}/{maxCats}";
    }

    public void UpdateCatsEdged(int cats)
    {    
        catsEdged.text = cats.ToString();
    }

    public void CatLost(){
        catsOutOfMaxx.color = Color.red;
    }
    public void CatIsEnough(){
        catsOutOfMaxx.color = Color.green;
    }
}
