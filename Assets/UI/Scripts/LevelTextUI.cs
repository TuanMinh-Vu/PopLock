using UnityEngine;

public class LevelTextUI : GameSystem
{
    TMPro.TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        text.text = "Level: " + gameManager.GameData.CurrentLevel.ToString();
    }

    void Update()
    {
        text.text = "Level: " + gameManager.GameData.CurrentLevel.ToString();
    }
}
