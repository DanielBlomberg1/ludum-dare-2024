using UnityEngine;

[CreateAssetMenu(fileName = "New Summon Item Settings", menuName = "Scriptable Objects/Items/New Summon Item Settings")]
public class SummonItemSettings : ScriptableObject
{
    [field: SerializeField]
    public SummonItem SummonItemType { get; private set; }

    [field: SerializeField]
    public GameObject ItemWorldPrefab { get; private set; }

    [field: SerializeField]
    public KeyCode KeyBind { get; private set; }
}
