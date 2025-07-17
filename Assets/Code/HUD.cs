using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType
    {
        Exp, Level, Kill, Time, Health
    }
    public InfoType infoType; // Type of information to display

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        switch (infoType)
        {
            case InfoType.Exp:
                float currentExp = GameManager.instance.exp;
                float nextExp = GameManager.instance.nextExp[GameManager.instance.level];
                mySlider.value = currentExp / nextExp; // Update the slider value based on current and next experience
                break;

            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", GameManager.instance.level); // Display the current level
                break;

            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.instance.kill); // Display the number of kills
                break;

            case InfoType.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int minutes = Mathf.FloorToInt(remainTime / 60);
                int seconds = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds); // Display the remaining time in MM:SS format
                break;

            case InfoType.Health:
                float currentHealth = GameManager.instance.health;
                float maxHealth = GameManager.instance.maxHealth;
                mySlider.value = currentHealth / maxHealth; // Update the slider value based on current and maximum health
                break;
        }
    }
}
