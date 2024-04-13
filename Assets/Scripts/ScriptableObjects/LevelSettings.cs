using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Level Settings", menuName = "Scriptable Objects/Levels/New Level Settings")]
public class LevelSettings : ScriptableObject
{
    [field: SerializeField]
    public SceneAsset LevelScene { get; private set; }


    [field: Header("Level start props")]

    [field: SerializeField]
    public int BridgeAmount { get; private set; } = 0;

    [field: SerializeField]
    public int DrillAmount { get; private set; } = 0;

    [field: SerializeField]
    public int PickaxeAmount { get; private set; } = 0;

    [field: SerializeField]
    public int SpringAmount { get; private set; } = 0;

    [field: SerializeField]
    public int WaterAmount { get; private set; } = 0;



    [field: Header("Other")]

    [field: SerializeField]
    public int CatAmount { get; private set; } = 0;
}
