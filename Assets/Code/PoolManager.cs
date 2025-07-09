using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;
    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject GetObject(int index)
    {
        GameObject selected = null;

        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                selected = item;
                selected.SetActive(true);
                break;
            }
        }

        if (!selected)
        {
            selected = Instantiate(prefabs[index], transform);
            pools[index].Add(selected);
        }

        return selected;
    }
}
