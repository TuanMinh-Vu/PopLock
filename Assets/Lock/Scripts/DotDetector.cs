using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDetector : GameSystem
{
    GameObject currentDot;
    GameObject lastEnteredDot;
    public float LoseThreshold = .5f;
    public GameEvent OnDotMissed;
    public GameEvent OnDotScored;
    public GameEvent OnWinEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentDot = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        lastEnteredDot = currentDot;
        currentDot = null;
        Debug.Log("Last dot set");
    }

    private void Update()
    {
        if (gameManager.GameData.IsRunning)
        {
            //Find distance b/w last dot and current pos. And if it's higher than some threshold then raise DotMissed Event
            if (lastEnteredDot && GetDistanceFromLastDot() > LoseThreshold)
            {
                OnDotMissed.Raise();
            }
        }


    }

    float GetDistanceFromLastDot()
    {
        Debug.Log((transform.position - lastEnteredDot.transform.position).magnitude);
        return (transform.position - lastEnteredDot.transform.position).magnitude;
    }

    public void OnTouch()
    {
        if (gameManager.GameData.IsRunning)
        {
            if (currentDot != null)
            {

                Destroy(currentDot);
                gameManager.GameData.DotsRemaining--;

                if (gameManager.GameData.DotsRemaining <= 0)
                {
                    gameManager.GameData.DotsRemaining = 0;
                    gameManager.GameData.CurrentLevel++;
                    OnWinEvent.Raise();
                }
                else
                {
                    OnDotScored.Raise();
                }
            }
            else
            {
                OnDotMissed.Raise();
            }
        }
    }

}
