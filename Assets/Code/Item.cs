using UnityEngine;
using UnityEngine.UI;


public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    public Gear gear;

    Image icon;
    Text textLevel;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    void LateUpdate()
    {
        textLevel.text = "Lv." + (level + 1).ToString();
    }

    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.MeleeWeapon:
            case ItemData.ItemType.RangedWeapon:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.levelDamage[level];
                    nextCount += data.levelCount[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }

                level++;

                break;

            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoes:
                if (level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.levelDamage[level];
                    gear.LevelUp(nextRate);
                }

                level++;

                break;

            case ItemData.ItemType.Heal:
                GameManager.instance.health = GameManager.instance.maxHealth;

                break;
        }

        if (level == data.levelDamage.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
