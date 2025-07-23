using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rectTransform;
    Item[] items;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next(); // Show the next set of items
        rectTransform.localScale = Vector3.one;
        GameManager.instance.Stop();
        AudioManager.instance.PlaySFX(AudioManager.SFX.LevelUp); // Play the level-up sound effect
        AudioManager.instance.EffectBGM(true); // Enable the effect BGM
    }

    public void Hide()
    {
        rectTransform.localScale = Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.instance.PlaySFX(AudioManager.SFX.Select); // Play the select sound effect
        AudioManager.instance.EffectBGM(false); // Disable the effect BGM
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    void Next()
    {
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false); // Deactivate all items
        }

        int[] random = new int[3];
        while (true)
        {
            random[0] = Random.Range(0, items.Length);
            random[1] = Random.Range(0, items.Length);
            random[2] = Random.Range(0, items.Length);
            if (random[0] != random[1] && random[0] != random[2] && random[1] != random[2])
                break; // Ensure all three indices are unique
        }

        for (int index = 0; index < random.Length; index++)
        {
            Item randomItem = items[random[index]];

            if (randomItem.level == randomItem.data.levelDamage.Length)
            {
                items[4].gameObject.SetActive(true); 
            }
            else
            { 
                randomItem.gameObject.SetActive(true); // Activate the randomly selected items
            }
        }
    }
}
