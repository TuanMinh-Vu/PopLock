using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour
{

    [SerializeField] Color StartColor;
    [SerializeField] Color LoseColor;
    private Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
        camera.backgroundColor = StartColor;
    }

    public void ChangeToLoseColor()
    {
        camera.backgroundColor = LoseColor;
    }

    public void ChangeToStartColor()
    {
        camera.backgroundColor = StartColor;
    }

}
