using UnityEngine;

public class RemainingDotsTextUI : GameSystem
{
    private TMPro.TextMeshPro text;

    private void Start()
    {
        text = GetComponent<TMPro.TextMeshPro>();
        text.text = gameManager.GameData.DotsRemaining.ToString();
    }

    private void Update()
    {
        text.text = gameManager.GameData.DotsRemaining.ToString();
    }
}
