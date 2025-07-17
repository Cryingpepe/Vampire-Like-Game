using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType
    {
        MeleeWeapon,
        RangedWeapon,
        Glove,
        Shoes,
        Heal
    }

    [Header("Main Information")]

    public ItemType itemType;
    public int itemId;
    public string itemName;
    [TextArea]
    public string itemDescription;
    public Sprite itemIcon;

    [Header("Level Data")]

    public float baseDamage;
    public int baseCount;
    public float[] levelDamage;
    public int[] levelCount;

    [Header("Weapon Data")]

    public GameObject projectile;

    public Sprite hand;
}
