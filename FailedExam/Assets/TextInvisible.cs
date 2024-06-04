using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextInvisible : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI text;
    void Start()
    {

        text = GetComponent<TextMeshProUGUI>();
        Color color = text.color;
        color.a = 0f;
        text.color = color;
    }


    IEnumerator fromInvisible()
    {
        for (float f = 0.05f; f <= 1.05; f += 0.05f)
        {
            Color color = text.color;
            color.a = f;
            text.color = color;
            yield return new WaitForSeconds(0.05f);
        }

    }
    public void StartInvisible()
    {
        StartCoroutine("fromInvisible");

    }
}
