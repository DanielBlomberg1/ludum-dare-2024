using Eflatun.SceneReference;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Settings", menuName = "Scriptable Objects/Levels/New Level Settings")]
public class LevelSettings : ScriptableObject
{
    [field: SerializeField]
    public SceneReference LevelScene { get; private set; }


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
    public int CatsRequiredToPass { get; private set; } = 0;

    [field: SerializeField]
    public int CatAmount { get; private set; } = 0;
    [field: SerializeField]
    public float spawnDelay { get; private set; } = 5.0f;

    [field: SerializeField]
    public float CatSpeed { get; private set; } = 1;

    [field: SerializeField]
    public bool isDark { get; private set; } = false;
}
