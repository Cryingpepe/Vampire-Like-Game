using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject[] titles;

    public void Lose()
    {
        titles[0].SetActive(true); // Show the "You Lose" title
    }

    public void Win()
    {
        titles[1].SetActive(true); // Show the "You Win" title
    }
}
