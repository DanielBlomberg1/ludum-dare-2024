using UnityEngine;

[CreateAssetMenu(fileName = "New Summon Item Settings", menuName = "Scriptable Objects/Items/New Summon Item Settings")]
public class SummonItemSettings : ScriptableObject
{
    [field: SerializeField]
    public SummonItem SummonItemLabel { get; private set; }

    [field:  SerializeField]
    public Sprite PreviewSprite { get; private set; }

    [field: SerializeField]
    public GameObject ItemWorldPrefab { get; private set; }

    [field: SerializeField]
    public KeyCode KeyBind { get; private set; }

    [field: SerializeField]
    public string KeybindText { get; private set; }
}
