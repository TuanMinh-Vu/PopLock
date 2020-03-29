using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchoredMotor : GameSystem
{
    public Direction Direction = Direction.Clockwise;
    [SerializeField] GameEvent OnPaddleReset;
    private Vector3 initialPos;

    private Transform anchor;

    private void Start()
    {
        anchor = GameObject.FindGameObjectWithTag("Anchor").transform;
        initialPos = GetComponent<Transform>().localPosition;
    }

    private void Update()
    {
        if (gameManager.GameData.IsRunning)
        {
            transform.RotateAround(anchor.position, Vector3.forward, gameManager.GameData.CurrentMotorSpeed * Time.deltaTime * -(int)Direction);
        }
    }

    public void ChangeDirection()
    {
        if(gameManager.GameData.IsRunning)
        {
            switch (Direction)
            {
                case Direction.Clockwise:
                    Direction = Direction.CounterClockwise;
                    break;
                case Direction.CounterClockwise:
                    Direction = Direction.Clockwise;
                    break;
            }
        }
     
    }

    public void ResetPosition()
    {
        transform.localPosition = new Vector3(0, initialPos.y, 0);
        transform.localRotation = Quaternion.identity;

        OnPaddleReset.Raise();
    }
}

public enum Direction
{
    Clockwise = 1,
    CounterClockwise = -1
}