using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlinkText : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    IEnumerator Blink()
    {
        while(true)
        {
            switch(text.color.a)
            {
                case 0:
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
                    yield return new WaitForSeconds(0.5f);

                    break;

                case 1:
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }
    }

    void StartBlinking()
    {
      StopCoroutine(Blink());
      StartCoroutine(Blink());
    }

    void StopBlinking()
    {
        StopCoroutine("Blink");
    }

    private void OnEnable()
    {
        StartBlinking();
    }

    private void OnDisable()
    {
        StopBlinking();
    }
}
