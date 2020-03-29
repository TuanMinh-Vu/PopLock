using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotSpawner : GameSystem
{
    [SerializeField] AnchoredMotor Motor;
    [SerializeField] GameObject DotPrefab;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        RemoveDuplicates();

        var angle = Random.Range(gameManager.GameData.MinSpawnAngle, gameManager.GameData.MaxSpawnAngle);
        var go = Instantiate(DotPrefab, Motor.transform.position, Quaternion.identity, transform);
        go.transform.RotateAround(transform.position, Vector3.forward, -angle * (int)Motor.Direction);
    }

   

    private void RemoveDuplicates()
    {
        var dots = GameObject.FindGameObjectsWithTag("Dot");
        foreach (var dot in dots)
        {
            Destroy(dot);
        }
    }
}
