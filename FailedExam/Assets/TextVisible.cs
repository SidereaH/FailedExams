using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class TextVisible : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI text;
    GameObject hintpar;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
       // hintpar = transform.parent.transform.parent.gameObject;
    }

    // Update is called once per frame

    IEnumerator toVisible()
    {
        for (float f = 1f; f >= -0.05; f -= 0.05f)
        {
            Color color = text.color;
            color.a = f;
            text.color = color;
            yield return new WaitForSeconds(0.05f);
        }
        
        //hintpar.SetActive(false);
    }
    public void StartVisible()
    {

        StartCoroutine("toVisible");


    }
}
