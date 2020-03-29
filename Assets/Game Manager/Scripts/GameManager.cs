using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GameSystem
{
    public GameData GameData;
    private bool isFirstTap = true;

    void Start()
    {
        GameData.ResetLevel();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (!GameData.IsRunning && isFirstTap)
            {
                GameData.IsRunning = true;
                isFirstTap = false;
            }
        }
    }


    public void LoadLevel()
    {
        GameData.ResetLevel();
        isFirstTap = true;
    }


}
